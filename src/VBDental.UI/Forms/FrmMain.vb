Imports VBDental.Data.Entities

Public Class FrmMain

    Private ReadOnly _usuarioActual As Usuario

    Public Sub New(usuarioActual As Usuario)
        InitializeComponent()
        _usuarioActual = usuarioActual
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblBienvenida.Text = $"Bienvenido/a, {_usuarioActual.NombreCompleto} ({_usuarioActual.Rol})"
        btnAdmin.Visible = (_usuarioActual.Rol = RolUsuario.Administrador)
        btnBitacora.Visible = (_usuarioActual.Rol = RolUsuario.Administrador)
        btnReportes.Visible = (_usuarioActual.Rol = RolUsuario.Administrador)
    End Sub

    Private Sub btnAgenda_Click(sender As Object, e As EventArgs)
        Using frm As New FrmAgenda(_usuarioActual)
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnAdmin_Click(sender As Object, e As EventArgs)
        Using frm As New FrmAdminReservas()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnBitacora_Click(sender As Object, e As EventArgs)
        Using frm As New FrmBitacora()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnReportes_Click(sender As Object, e As EventArgs)
        Using frm As New FrmReportes()
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnCerrarSesion_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class
