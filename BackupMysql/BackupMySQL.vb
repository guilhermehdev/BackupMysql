Imports System.IO
Imports System.IO.Compression
Imports System.Windows.Forms

Public Class BackupMySQL

    Public Shared Function CriarBackup() As String
        If Not Directory.Exists(Application.StartupPath & "\backup") Then
            Directory.CreateDirectory(Application.StartupPath & "\backup")
        End If
        Dim dataAtual As String = DateTime.Now.ToString("dd-MM-yyyy_HH-mm")
        Dim caminhoBackup = Application.StartupPath & $"\backup\AME-{dataAtual}.sql"
        ' Substitui vírgulas por espaços para os bancos e remove espaços extras
        Dim bancosParaBackup As String = String.Join(" ", My.Settings.DB.Split(","c).Select(Function(b) b.Trim()).Where(Function(b) Not String.IsNullOrEmpty(b)))

        Try

            Dim processo As New Process()
            processo.StartInfo.FileName = My.Settings.mySQLDumpPath
            processo.StartInfo.Arguments = $"--user={My.Settings.usuarioDB} --password={My.Settings.senhaDB} --host=localhost --skip-lock-tables --databases {bancosParaBackup} --result-file=""{caminhoBackup}"""
            processo.StartInfo.RedirectStandardOutput = True
            processo.StartInfo.RedirectStandardError = True
            processo.StartInfo.UseShellExecute = False
            processo.StartInfo.CreateNoWindow = True
            processo.Start()
            processo.WaitForExit()

            If processo.ExitCode = 0 Then
                Return CompactarBackup(caminhoBackup)
            Else
                Dim erro As String = processo.StandardError.ReadToEnd()
                Form1.EscreverLog("Erro no processo mysqldump: " & erro)
                Return Nothing
            End If
        Catch ex As Exception
            Form1.EscreverLog("Erro em CriarBackup: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function CompactarBackup(ByVal caminhoBackup As String) As String
        Try
            Dim caminhoZip As String = Path.ChangeExtension(caminhoBackup, ".zip")
            Using zip As FileStream = New FileStream(caminhoZip, FileMode.Create)
                Using arquivoZip As ZipArchive = New ZipArchive(zip, ZipArchiveMode.Create)
                    Dim entrada = arquivoZip.CreateEntry(Path.GetFileName(caminhoBackup))
                    Using entradaStream As Stream = entrada.Open()
                        Using arquivoOriginal As FileStream = New FileStream(caminhoBackup, FileMode.Open, FileAccess.Read)
                            arquivoOriginal.CopyTo(entradaStream)
                        End Using
                    End Using
                End Using
            End Using
            Return caminhoZip

        Catch ex As Exception
            Form1.EscreverLog("erro ao CompactarBackup" & ex.Message)
            Return String.Empty
        End Try
    End Function
End Class
