Imports System.Threading
Imports System.Windows.Forms

Public Class Program
    Private Shared appMutex As Mutex

    <STAThread>
    Public Shared Sub Main()
        Dim createdNew As Boolean
        Dim appName As String = "BackupMySQL"

        ' Verifica se já existe uma instância do aplicativo em execução
        appMutex = New Mutex(True, appName, createdNew)

        If Not createdNew Then
            ReiniciarOutraInstancia(appName)
            Return
        End If

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub

    Private Shared Sub ReiniciarOutraInstancia(appName As String)
        ' Envia um sinal para a instância existente solicitar reinício
        Try
            Dim processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)
            For Each proc In processes
                If proc.Id <> Process.GetCurrentProcess().Id Then
                    proc.Kill() ' Encerra a instância anterior
                End If
            Next

            ' Aguarda brevemente e reinicia
            Thread.Sleep(1000) ' Dá tempo para encerrar a instância antiga
            Process.Start(Application.ExecutablePath) ' Reinicia o aplicativo
        Catch ex As Exception
            MessageBox.Show("Erro ao reiniciar a aplicação: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
