Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Public Class GoogleDriveUploader

    Public Sub EnviarParaGoogleDrive(filePath As String)
        Try
            Form1.EscreverLog("Iniciando upload para o Google Drive: " & filePath)

            ' Verifica se o arquivo existe
            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException("Arquivo não encontrado: " & filePath)
            End If

            ' Obter o serviço do Google Drive
            Dim driveService As DriveService = ObterServicoGoogleDrive()

            ' Obter ou criar pasta no Google Drive
            Dim nomePasta As String = "BackupDBAME"
            Dim pastaId As String = checkGDriveFolder(nomePasta, driveService)

            ' Configurar metadados do arquivo
            Dim arquivoParaUpload As New Google.Apis.Drive.v3.Data.File() With {
            .Name = Path.GetFileName(filePath),
            .Parents = New List(Of String) From {pastaId} ' Define a pasta pai
        }

            ' Enviar arquivo para o Google Drive
            Using stream As New FileStream(filePath, FileMode.Open)
                Dim request As FilesResource.CreateMediaUpload = driveService.Files.Create(arquivoParaUpload, stream, "application/zip")
                request.Upload()

                ' Obter resposta do upload
                Dim fileUploaded = request.ResponseBody
                Form1.EscreverLog("Upload concluído. ID do arquivo: " & fileUploaded.Id)
            End Using

            'File.Delete(Path.ChangeExtension(filePath, "sql"))

        Catch ex As Exception
            Form1.EscreverLog("Erro ao enviar arquivo para o Google Drive: " & ex.Message)
            Throw
        End Try
    End Sub

    Private Function checkGDriveFolder(nomePasta As String, driveService As DriveService) As String
        Try
            ' Procurar a pasta pelo nome
            Dim request = driveService.Files.List()
            request.Q = $"mimeType = 'application/vnd.google-apps.folder' and name = '{nomePasta}' and trashed = false"
            request.Fields = "files(id, name)"
            Dim resultado = request.Execute()

            ' Se a pasta já existir, retorna o ID
            If resultado.Files IsNot Nothing AndAlso resultado.Files.Count > 0 Then
                Dim pastaExistente = resultado.Files(0)
                Form1.EscreverLog($"Pasta '{nomePasta}' já existe no Google Drive. ID: {pastaExistente.Id}")
                Return pastaExistente.Id
            End If

            ' Caso contrário, criar a pasta
            Form1.EscreverLog($"Pasta '{nomePasta}' não encontrada. Criando nova pasta...")
            Dim pastaMetadata As New Google.Apis.Drive.v3.Data.File() With {
            .Name = nomePasta,
            .MimeType = "application/vnd.google-apps.folder"
        }

            Dim pastaCriada = driveService.Files.Create(pastaMetadata).Execute()
            Form1.EscreverLog($"Pasta '{nomePasta}' criada com sucesso. ID: {pastaCriada.Id}")
            Return pastaCriada.Id
        Catch ex As Exception
            Form1.EscreverLog("Erro ao obter ou criar pasta no Google Drive: " & ex.Message)
            Throw
        End Try
    End Function

    'Public Function EnviarParaGoogleDrive(filePath As String)
    '    Try
    '        Form1.EscreverLog("Iniciando upload para o Google Drive: " & filePath)

    '        ' Verifica se o arquivo existe
    '        If Not File.Exists(filePath) Then
    '            Throw New FileNotFoundException("Arquivo não encontrado: " & filePath)
    '        End If

    '        ' Lógica de upload (substitua este exemplo com sua implementação)
    '        Dim driveService As DriveService = ObterServicoGoogleDrive() ' Método que retorna o serviço autenticado
    '        Dim arquivoParaUpload As New Google.Apis.Drive.v3.Data.File() With {
    '        .Name = Path.GetFileName(filePath)
    '    }

    '        Using stream As New FileStream(filePath, FileMode.Open)
    '            Dim request As FilesResource.CreateMediaUpload = driveService.Files.Create(arquivoParaUpload, stream, "application/zip")
    '            request.Upload()

    '            ' Obtém a resposta do upload
    '            Dim fileUploaded = request.ResponseBody
    '            Form1.EscreverLog("Upload concluído. ID do arquivo: " & fileUploaded.Id)
    '            Return fileUploaded.Id
    '        End Using
    '    Catch ex As Exception
    '        Form1.EscreverLog("Erro ao enviar arquivo para o Google Drive: " & ex.Message)
    '        Throw
    '    End Try
    'End Function

    'Public Shared Function UploadArquivo(ByVal caminhoArquivo As String, ByVal credenciaisPath As String) As String
    '    Try
    '        Dim credential As GoogleCredential
    '        Using stream = New FileStream(credenciaisPath, FileMode.Open, FileAccess.Read)
    '            credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.ScopeConstants.DriveFile)
    '        End Using

    '        Dim service = New DriveService(New BaseClientService.Initializer() With {
    '            .HttpClientInitializer = credential,
    '            .ApplicationName = "MysqlBackupSecretaria"
    '        })

    '        Dim fileMetadata = New Google.Apis.Drive.v3.Data.File() With {
    '            .Name = Path.GetFileName(caminhoArquivo)
    '        }

    '        Using fileStream = New FileStream(caminhoArquivo, FileMode.Open)
    '            Dim request = service.Files.Create(fileMetadata, fileStream, "application/zip")
    '            request.Fields = "id"
    '            request.Upload()
    '            Return request.ResponseBody.Id
    '        End Using
    '    Catch ex As Exception
    '        Form1.EscreverLog("Erro ao enviar arquivo para o Google Drive: " & ex.Message)
    '        Return String.Empty
    '    End Try
    'End Function
    Public Function ObterServicoGoogleDrive() As DriveService
        Dim credPath As String = Application.StartupPath & "\backup\cred.json" ' Caminho para o arquivo de credenciais
        Dim tokenPath As String = Application.StartupPath & "\backup\token.json" ' Onde o token será salvo

        Try
            ' Escopos necessários para o acesso
            Dim Scopes As String() = {DriveService.Scope.DriveFile}

            ' Lê as credenciais do arquivo
            Using stream = New FileStream(credPath, FileMode.Open, FileAccess.Read)
                Dim cred = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.FromStream(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                New FileDataStore(tokenPath, True)
            ).Result

                ' Retorna o serviço autenticado
                Return New DriveService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = cred,
                .ApplicationName = "BackupMySQL"
            })
            End Using
        Catch ex As Exception
            Throw New Exception("Erro ao obter o serviço Google Drive: " & ex.Message)
        End Try
    End Function

End Class
