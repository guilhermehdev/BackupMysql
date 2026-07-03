Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class Form1
    Private trayIcon As NotifyIcon
    Private backupHour As TimeSpan = My.Settings.backupTime
    Dim usuario As String = My.Settings.usuarioDB
    Dim senha As String = My.Settings.senhaDB
    Dim banco As String = My.Settings.DB
    Dim caminhoMysqlDump As String = My.Settings.mySQLDumpPath
    Dim google As New GoogleDriveUploader
    Private Sub Backup()
        EscreverLog("Backup solicitado.")
        Try

            Dim backupPath As String = BackupMySQL.CriarBackup()

            google.EnviarParaGoogleDrive(backupPath)
            File.Delete(Path.ChangeExtension(backupPath, "sql"))
            File.Delete(Path.ChangeExtension(backupPath, "zip"))

            EscreverLog("Backup manual concluído e enviado para o Google Drive.")
            EscreverLog("Backup concluído com sucesso!")
        Catch ex As Exception
            EscreverLog($"Erro ao realizar o backup: {ex.Message}")
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim stsLabel = lbCADSUSsts
        trayIcon = New NotifyIcon()
        trayIcon.Icon = My.Resources.icon3
        trayIcon.Text = "Backup MySQL"
        trayIcon.Visible = True

        If PDFSERVER.Iniciar() Then
            stsLabel.Text = "CADSUS PDF ON"
            stsLabel.ForeColor = Color.LimeGreen
        Else
            stsLabel.Text = "CADSUS PDF OFF"
            stsLabel.ForeColor = Color.Red
        End If

        ' Adiciona Menu ao Tray Icon
        Dim contextMenu As New ContextMenu()
        contextMenu.MenuItems.Add("Abrir", AddressOf AbrirAplicacao)
        contextMenu.MenuItems.Add("Sair", AddressOf SairAplicacao)
        trayIcon.ContextMenu = contextMenu
        AddHandler trayIcon.DoubleClick, AddressOf AbrirAplicacao

        tbMysqlDump.Text = My.Settings.mySQLDumpPath
        tbUsuario.Text = My.Settings.usuarioDB
        tbSenha.Text = My.Settings.senhaDB
        tbBanco.Text = My.Settings.DB
        tbHorarioBackup.Text = My.Settings.backupTime.ToString("hh\:mm")

        If My.Settings.mySQLDumpPath = "" Or My.Settings.usuarioDB = "" Or My.Settings.senhaDB = "" Or My.Settings.DB = "" Then
            MsgBox("Configurações necessárias")
            Me.WindowState = FormWindowState.Normal
        Else
            Me.Hide()
            Me.ShowInTaskbar = False
            ' Inicializa o Timer
            backupTimer.Start()
            ' Log
            EscreverLog("Aplicação iniciada e rodando em segundo plano.")
        End If

    End Sub

    Private Sub VerificarBackup()
        Try
            Dim agora As TimeSpan = DateTime.Now.TimeOfDay
            Dim backupHour As TimeSpan = My.Settings.backupTime

            ' Verifica se o horário de backup está definido
            If backupHour = TimeSpan.Zero Then
                ' Se não estiver definido, não faz nada
                EscreverLog("Horário de backup não definido. Nenhuma ação será realizada.")
                Return
            End If

            ' Verifica se é a hora exata do backup
            ' Permite uma margem de 59 segundos para evitar problemas de tempo
            If agora >= backupHour AndAlso agora < backupHour.Add(TimeSpan.FromMinutes(1)) Then
                EscreverLog("Iniciando o backup automático...")
                Backup()
            End If
        Catch ex As Exception
            EscreverLog("Erro em VerificarBackup: " & ex.Message)
        End Try
    End Sub
    Private Sub AbrirAplicacao(sender As Object, e As EventArgs)
        ' Mostra o formulário (se necessário)
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub SairAplicacao(sender As Object, e As EventArgs)
        ' Encerra a aplicação
        trayIcon.Visible = False
        Application.Exit()
    End Sub

    Public Sub EscreverLog(mensagem As String)
        Dim logPath As String = Application.StartupPath & "\backup\backup_log.txt"
        Dim logDir As String = Path.GetDirectoryName(logPath)

        Try
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If
            File.AppendAllText(logPath, $"{DateTime.Now}: {mensagem}{Environment.NewLine}")
        Catch ex As Exception
            ' Log de erro silencioso
        End Try
    End Sub

    Private Sub btBuscar_Click(sender As Object, e As EventArgs) Handles btBuscar.Click
        OpenFileDialog1.Filter = "Executáveis (*.exe)|*.exe"

        If OpenFileDialog1.ShowDialog Then
            tbMysqlDump.Text = OpenFileDialog1.FileName
            My.Settings.mySQLDumpPath = OpenFileDialog1.FileName
            My.Settings.Save()
            MsgBox("Salvo!")
        End If
    End Sub

    Private Sub btFechar_Click(sender As Object, e As EventArgs) Handles btFechar.Click
        Me.Hide()
    End Sub
    Private Sub cbMostrarsenha_CheckedChanged(sender As Object, e As EventArgs)
        If cbMostrarsenha.Checked Then
            tbSenha.PasswordChar = ""
            tbSenha.UseSystemPasswordChar = False
        Else
            tbSenha.PasswordChar = "*"
            tbSenha.UseSystemPasswordChar = True
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If tbUsuario.Text <> "" And tbSenha.Text <> "" And tbBanco.Text <> "" Then
            My.Settings.usuarioDB = tbUsuario.Text
            My.Settings.senhaDB = tbSenha.Text
            My.Settings.DB = tbBanco.Text
            My.Settings.backupTime = TimeSpan.Parse(tbHorarioBackup.Text)
            My.Settings.Save()
            MsgBox("Configurações salvas! Reiniciando...")
            Application.Restart()
        Else
            MsgBox("Preencha todos os campos")
        End If
    End Sub
    Private Sub btLog_Click(sender As Object, e As EventArgs)
        Try
            ' Substitua pelo caminho real do seu arquivo
            Dim caminhoArquivo As String = Application.StartupPath & "\backup\backup_log.txt"

            ' Abre o arquivo no programa padrão do Windows (geralmente Bloco de Notas)
            Process.Start(New ProcessStartInfo(caminhoArquivo) With {.UseShellExecute = True})

        Catch ex As Exception
            MessageBox.Show("Erro ao abrir o arquivo: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Backup()
    End Sub

    Private Sub backupTimer_Tick_1(sender As Object, e As EventArgs)
        VerificarBackup()
    End Sub

End Class
