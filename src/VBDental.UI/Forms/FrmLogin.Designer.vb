Partial Class FrmLogin
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private lblTitulo As System.Windows.Forms.Label
    Private lblUsuario As System.Windows.Forms.Label
    Private txtUsuario As System.Windows.Forms.TextBox
    Private lblPassword As System.Windows.Forms.Label
    Private txtPassword As System.Windows.Forms.TextBox
    Private btnIngresar As System.Windows.Forms.Button
    Private lblMensaje As System.Windows.Forms.Label
    Private errorProvider1 As System.Windows.Forms.ErrorProvider

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnIngresar = New System.Windows.Forms.Button()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.errorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(40, 30)
        Me.lblTitulo.Text = "VB-Dental · Reservas Médicas"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Location = New System.Drawing.Point(40, 100)
        Me.lblUsuario.Text = "Usuario:"
        '
        'txtUsuario
        '
        Me.txtUsuario.Location = New System.Drawing.Point(140, 97)
        Me.txtUsuario.Size = New System.Drawing.Size(220, 23)
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(40, 140)
        Me.lblPassword.Text = "Contraseña:"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(140, 137)
        Me.txtPassword.Size = New System.Drawing.Size(220, 23)
        Me.txtPassword.PasswordChar = "●"c
        '
        'btnIngresar
        '
        Me.btnIngresar.Location = New System.Drawing.Point(140, 180)
        Me.btnIngresar.Size = New System.Drawing.Size(120, 32)
        Me.btnIngresar.Text = "Ingresar"
        Me.btnIngresar.UseVisualStyleBackColor = True
        AddHandler Me.btnIngresar.Click, AddressOf Me.btnIngresar_Click
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.ForeColor = System.Drawing.Color.Firebrick
        Me.lblMensaje.Location = New System.Drawing.Point(40, 225)
        Me.lblMensaje.Size = New System.Drawing.Size(320, 40)
        Me.lblMensaje.MaximumSize = New System.Drawing.Size(320, 0)
        Me.lblMensaje.Text = ""
        '
        'FrmLogin
        '
        Me.AcceptButton = Me.btnIngresar
        Me.ClientSize = New System.Drawing.Size(400, 290)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.txtUsuario)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.btnIngresar)
        Me.Controls.Add(Me.lblMensaje)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingreso al sistema"
        CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
