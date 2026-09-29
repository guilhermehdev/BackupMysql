Imports System.IO
Imports System.IO.Compression

Public Class BackupMySQL

    Public Shared Function CriarBackup() As String
        Dim configuracao = BackupConfiguration.Atual
        If Not Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup")) Then
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup"))
        End If
        Dim dataAtual As String = DateTime.Now.ToString("dd-MM-yyyy_HH-mm")
        Dim caminhoBackup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "backup", String.Format("AME-{0}.sql", dataAtual))
        ' Substitui vírgulas por espaços para os bancos e remove espaços extras
        Dim bancosParaBackup As String = String.Join(" ", configuracao.DB.Split(","c).Select(Function(b) b.Trim()).Where(Function(b) Not String.IsNullOrEmpty(b)))

        Try

            Dim processo As New Process()
            processo.StartInfo.FileName = configuracao.MySQLDumpPath
            processo.StartInfo.Arguments = String.Format("--user={0} --password={1} --host=localhost --skip-lock-tables --databases {2} --result-file=""{3}""", configuracao.UsuarioDB, configuracao.SenhaDB, bancosParaBackup, caminhoBackup)
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
                BackupLogger.Escrever("Erro no processo mysqldump: " & erro)
                Return Nothing
            End If
        Catch ex As Exception
            BackupLogger.Escrever("Erro em CriarBackup: " & ex.ToString())
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
            BackupLogger.Escrever("Erro ao CompactarBackup: " & ex.ToString())
            Return String.Empty
        End Try
    End Function
End Class
