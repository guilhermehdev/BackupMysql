<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.backupTimer = New System.Windows.Forms.Timer(Me.components)
        Me.tbMysqlDump = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btBuscar = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cbMostrarsenha = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbHorarioBackup = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbBanco = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbSenha = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbUsuario = New System.Windows.Forms.TextBox()
        Me.btFechar = New System.Windows.Forms.Button()
        Me.btLog = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbCADSUSsts = New System.Windows.Forms.Label()
        Me.lbBACKUPsts = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'backupTimer
        '
        Me.backupTimer.Enabled = True
        Me.backupTimer.Interval = 30000
        AddHandler Me.backupTimer.Tick, AddressOf Me.backupTimer_Tick_1
        '
        'tbMysqlDump
        '
        Me.tbMysqlDump.Location = New System.Drawing.Point(15, 36)
        Me.tbMysqlDump.Name = "tbMysqlDump"
        Me.tbMysqlDump.Size = New System.Drawing.Size(351, 20)
        Me.tbMysqlDump.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "MySQLDump"
        '
        'btBuscar
        '
        Me.btBuscar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btBuscar.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btBuscar.ForeColor = System.Drawing.Color.Black
        Me.btBuscar.Location = New System.Drawing.Point(365, 35)
        Me.btBuscar.Name = "btBuscar"
        Me.btBuscar.Size = New System.Drawing.Size(38, 22)
        Me.btBuscar.TabIndex = 2
        Me.btBuscar.Text = ". . ."
        Me.btBuscar.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btBuscar.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "mysqldump"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.cbMostrarsenha)
        Me.GroupBox1.Controls.Add(Me.btBuscar)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.tbHorarioBackup)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.tbBanco)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.tbSenha)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.tbUsuario)
        Me.GroupBox1.Controls.Add(Me.tbMysqlDump)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(410, 149)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(311, 112)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(92, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Backup"
        Me.Button2.UseVisualStyleBackColor = False
        AddHandler Me.Button2.Click, AddressOf Me.Button2_Click
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(210, 112)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Salvar config"
        Me.Button1.UseVisualStyleBackColor = True
        AddHandler Me.Button1.Click, AddressOf Me.Button1_Click
        '
        'cbMostrarsenha
        '
        Me.cbMostrarsenha.AutoSize = True
        Me.cbMostrarsenha.Location = New System.Drawing.Point(311, 77)
        Me.cbMostrarsenha.Name = "cbMostrarsenha"
        Me.cbMostrarsenha.Size = New System.Drawing.Size(74, 17)
        Me.cbMostrarsenha.TabIndex = 5
        Me.cbMostrarsenha.Text = "Ver senha"
        Me.cbMostrarsenha.UseVisualStyleBackColor = True
        AddHandler Me.cbMostrarsenha.CheckedChanged, AddressOf Me.cbMostrarsenha_CheckedChanged
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(157, 97)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Horário"
        '
        'tbHorarioBackup
        '
        Me.tbHorarioBackup.Location = New System.Drawing.Point(159, 113)
        Me.tbHorarioBackup.Mask = "00:00"
        Me.tbHorarioBackup.Name = "tbHorarioBackup"
        Me.tbHorarioBackup.Size = New System.Drawing.Size(46, 20)
        Me.tbHorarioBackup.TabIndex = 7
        Me.tbHorarioBackup.ValidatingType = GetType(Date)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Bancos"
        '
        'tbBanco
        '
        Me.tbBanco.Location = New System.Drawing.Point(15, 113)
        Me.tbBanco.Name = "tbBanco"
        Me.tbBanco.Size = New System.Drawing.Size(139, 20)
        Me.tbBanco.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(171, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Senha"
        '
        'tbSenha
        '
        Me.tbSenha.Location = New System.Drawing.Point(174, 75)
        Me.tbSenha.Name = "tbSenha"
        Me.tbSenha.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbSenha.Size = New System.Drawing.Size(132, 20)
        Me.tbSenha.TabIndex = 4
        Me.tbSenha.UseSystemPasswordChar = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Usuario"
        '
        'tbUsuario
        '
        Me.tbUsuario.Location = New System.Drawing.Point(15, 75)
        Me.tbUsuario.Name = "tbUsuario"
        Me.tbUsuario.Size = New System.Drawing.Size(153, 20)
        Me.tbUsuario.TabIndex = 3
        '
        'btFechar
        '
        Me.btFechar.BackColor = System.Drawing.Color.IndianRed
        Me.btFechar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btFechar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btFechar.ForeColor = System.Drawing.Color.White
        Me.btFechar.Location = New System.Drawing.Point(347, 181)
        Me.btFechar.Name = "btFechar"
        Me.btFechar.Size = New System.Drawing.Size(75, 23)
        Me.btFechar.TabIndex = 11
        Me.btFechar.Text = "Fechar"
        Me.btFechar.UseVisualStyleBackColor = False
        '
        'btLog
        '
        Me.btLog.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btLog.ForeColor = System.Drawing.Color.Black
        Me.btLog.Location = New System.Drawing.Point(303, 181)
        Me.btLog.Name = "btLog"
        Me.btLog.Size = New System.Drawing.Size(43, 23)
        Me.btLog.TabIndex = 10
        Me.btLog.Text = "Log"
        Me.btLog.UseVisualStyleBackColor = True
        AddHandler Me.btLog.Click, AddressOf Me.btLog_Click
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(223, 23)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Backup automático"
        '
        'lbCADSUSsts
        '
        Me.lbCADSUSsts.AutoSize = True
        Me.lbCADSUSsts.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCADSUSsts.Location = New System.Drawing.Point(12, 186)
        Me.lbCADSUSsts.Name = "lbCADSUSsts"
        Me.lbCADSUSsts.Size = New System.Drawing.Size(104, 12)
        Me.lbCADSUSsts.TabIndex = 9
        Me.lbCADSUSsts.Text = "CADSUS PDF STATUS"
        '
        'lbBACKUPsts
        '
        Me.lbBACKUPsts.AutoSize = True
        Me.lbBACKUPsts.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbBACKUPsts.Location = New System.Drawing.Point(116, 186)
        Me.lbBACKUPsts.Name = "lbBACKUPsts"
        Me.lbBACKUPsts.Size = New System.Drawing.Size(110, 12)
        Me.lbBACKUPsts.TabIndex = 10
        Me.lbBACKUPsts.Text = "BACKUP AUTO STATUS"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(434, 214)
        Me.Controls.Add(Me.lbBACKUPsts)
        Me.Controls.Add(Me.lbCADSUSsts)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btLog)
        Me.Controls.Add(Me.btFechar)
        Me.Controls.Add(Me.GroupBox1)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BackupMysql"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents backupTimer As Windows.Forms.Timer
    Friend WithEvents tbMysqlDump As Windows.Forms.TextBox
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents btBuscar As Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents tbUsuario As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents tbBanco As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents tbSenha As Windows.Forms.TextBox
    Friend WithEvents btFechar As Windows.Forms.Button
    Friend WithEvents btLog As Windows.Forms.Button
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents tbHorarioBackup As Windows.Forms.MaskedTextBox
    Friend WithEvents cbMostrarsenha As Windows.Forms.CheckBox
    Friend WithEvents Button1 As Windows.Forms.Button
    Friend WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents lbCADSUSsts As Windows.Forms.Label
    Friend WithEvents lbBACKUPsts As Windows.Forms.Label
End Class
