Imports VBDental.Business.Models
Imports VBDental.Data

Namespace Services

    Public Class AuthService

        Private ReadOnly _usuarioRepository As New UsuarioRepository()
        Private ReadOnly _logService As New LogService()

        Public Function Login(nombreUsuario As String, password As String) As AuthResult
            If String.IsNullOrWhiteSpace(nombreUsuario) OrElse String.IsNullOrWhiteSpace(password) Then
                Return New AuthResult With {.Exitoso = False, .Mensaje = "Debe ingresar usuario y contraseña."}
            End If

            Dim usuario = _usuarioRepository.ObtenerPorNombreUsuario(nombreUsuario.Trim())

            If usuario Is Nothing OrElse Not usuario.Activo Then
                _logService.Registrar(Nothing, TipoEventoBitacora.LoginFallido, $"Usuario inexistente o inactivo: {nombreUsuario}")
                Return New AuthResult With {.Exitoso = False, .Mensaje = "Usuario o contraseña incorrectos."}
            End If

            If Not PasswordHasher.Verificar(password, usuario.PasswordSalt, usuario.PasswordHash) Then
                _logService.Registrar(usuario.UsuarioId, TipoEventoBitacora.LoginFallido, "Contraseña incorrecta")
                Return New AuthResult With {.Exitoso = False, .Mensaje = "Usuario o contraseña incorrectos."}
            End If

            _logService.Registrar(usuario.UsuarioId, TipoEventoBitacora.LoginExitoso, Nothing)
            Return New AuthResult With {.Exitoso = True, .Mensaje = "OK", .UsuarioAutenticado = usuario}
        End Function

    End Class

End Namespace
