Imports VBDental.Business.Services

Public Class FrmLogin

    Private ReadOnly _authService As New AuthService()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs)
        lblMensaje.Text = ""

        If Not ValidarCampos() Then Return

        Dim resultado = _authService.Login(txtUsuario.Text, txtPassword.Text)

        If Not resultado.Exitoso Then
            lblMensaje.Text = resultado.Mensaje
            Return
        End If

        Dim frmMain As New FrmMain(resultado.UsuarioAutenticado)
        Me.Hide()
        frmMain.ShowDialog()
        Me.Close()
    End Sub

    Private Function ValidarCampos() As Boolean
        errorProvider1.Clear()
        Dim esValido As Boolean = True

        If String.IsNullOrWhiteSpace(txtUsuario.Text) Then
            errorProvider1.SetError(txtUsuario, "El usuario es obligatorio.")
            esValido = False
        End If

        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            errorProvider1.SetError(txtPassword, "La contraseña es obligatoria.")
            esValido = False
        End If

        Return esValido
    End Function

End Class
