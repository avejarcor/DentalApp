Namespace Entities

    Public Class Usuario
        Public Property UsuarioId As Integer
        Public Property NombreUsuario As String
        Public Property PasswordHash As String
        Public Property PasswordSalt As String
        Public Property NombreCompleto As String
        Public Property Rol As RolUsuario
        Public Property Activo As Boolean
    End Class

End Namespace
