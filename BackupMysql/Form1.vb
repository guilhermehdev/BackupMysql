Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms

Public Class Form1
    Private trayIcon As NotifyIcon


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

        Dim configuracao = BackupConfiguration.Atual
        tbMysqlDump.Text = configuracao.MySQLDumpPath
        tbUsuario.Text = configuracao.UsuarioDB
        tbSenha.Text = configuracao.SenhaDB
        tbBanco.Text = configuracao.DB
        tbHorarioBackup.Text = configuracao.BackupTime.ToString("hh\:mm")

        backupTimer.Stop()
        backupTimer.Enabled = False
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True
        lbBACKUPsts.Text = "AGENDAMENTO PELO SERVIÇO"
        lbBACKUPsts.ForeColor = Color.DeepSkyBlue

    End Sub

    Private Sub AbrirAplicacao(sender As Object, e As EventArgs)
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub SairAplicacao(sender As Object, e As EventArgs)
        trayIcon.Visible = False
        Application.Exit()
    End Sub

    Public Sub EscreverLog(mensagem As String)
        BackupLogger.Escrever(mensagem)
    End Sub

    Private Sub btBuscar_Click(sender As Object, e As EventArgs) Handles btBuscar.Click
        OpenFileDialog1.Filter = "Executáveis (*.exe)|*.exe"

        If OpenFileDialog1.ShowDialog Then
            tbMysqlDump.Text = OpenFileDialog1.FileName
            BackupConfiguration.Atual.MySQLDumpPath = OpenFileDialog1.FileName
            BackupConfiguration.Atual.Salvar()
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
            Dim configuracao = BackupConfiguration.Atual
            configuracao.UsuarioDB = tbUsuario.Text
            configuracao.SenhaDB = tbSenha.Text
            configuracao.DB = tbBanco.Text

            Dim textoHorario As String = tbHorarioBackup.Text.Trim()
            EscreverLog("Texto do horário recebido: [" & textoHorario & "]")

            If String.IsNullOrWhiteSpace(textoHorario) OrElse textoHorario = ":" OrElse textoHorario = "  :" Then
                configuracao.BackupTime = TimeSpan.Zero
            Else
                Dim horarioInformado As DateTime
                If DateTime.TryParseExact(textoHorario, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, horarioInformado) Then
                    configuracao.BackupTime = New TimeSpan(horarioInformado.Hour, horarioInformado.Minute, 0)
                Else
                    MessageBox.Show("Informe um horário válido no formato HH:mm, por exemplo 23:30.", "Horário inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    tbHorarioBackup.Focus()
                    Return
                End If
            End If
            configuracao.Salvar()
            EscreverLog("Configuração compartilhada salva. Horário: " & configuracao.BackupTime.ToString("hh\:mm"))
            MsgBox("Configurações salvas. O serviço aplicará o novo horário automaticamente.")
        Else
            MsgBox("Preencha todos os campos")
        End If
    End Sub

    Private Sub btLog_Click(sender As Object, e As EventArgs)
        Try
            Dim caminhoArquivo As String = Application.StartupPath & "\backup\backup_log.txt"
            Process.Start(New ProcessStartInfo(caminhoArquivo) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Erro ao abrir o arquivo: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        MessageBox.Show("O backup automático é executado pelo serviço BackupMySQL.", "BackupMySQL", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub backupTimer_Tick_1(sender As Object, e As EventArgs) Handles backupTimer.Tick
        ' O agendamento pertence exclusivamente ao serviço do Windows.
    End Sub

End Class
