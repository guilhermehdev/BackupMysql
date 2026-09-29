Imports System.ServiceProcess
Imports System.Threading
Imports System

Namespace Service
    Public Class BackupWindowsService
        Inherits ServiceBase

        Private backupManager As BackupManager
        Private timer As Timer
        Private ultimoBackupAutomatico As String = String.Empty

        Public Sub New()
            ServiceName = "BackupMySQL"
            CanStop = True
            CanPauseAndContinue = False
            AutoLog = True
        End Sub

        Protected Overrides Sub OnStart(args() As String)
            BackupLogger.Escrever("Serviço BackupMySQL iniciado.")
            backupManager = New BackupManager()
            timer = New Timer(AddressOf VerificarHorario, Nothing, TimeSpan.Zero, TimeSpan.FromSeconds(30))
        End Sub

        Protected Overrides Sub OnStop()
            If timer IsNot Nothing Then
                timer.Dispose()
                timer = Nothing
            End If

            BackupLogger.Escrever("Serviço BackupMySQL parado.")
        End Sub

        Private Sub VerificarHorario(state As Object)
            Try
                Dim configuracao As BackupConfiguration = BackupConfiguration.Recarregar()
                Dim horario As TimeSpan = configuracao.BackupTime
                If horario = TimeSpan.Zero Then Return

                Dim agora As DateTime = DateTime.Now
                If agora.TimeOfDay < horario OrElse agora.TimeOfDay >= horario.Add(TimeSpan.FromMinutes(1)) Then Return

                Dim chave As String = agora.ToString("yyyy-MM-dd_HH-mm")
                If ultimoBackupAutomatico = chave Then Return

                ultimoBackupAutomatico = chave
                BackupLogger.Escrever("Iniciando backup automático pelo serviço.")
                backupManager.ExecutarBackup()
            Catch ex As Exception
                BackupLogger.Escrever("Erro no agendamento do serviço: " & ex.ToString())
            End Try
        End Sub
    End Class
End Namespace
