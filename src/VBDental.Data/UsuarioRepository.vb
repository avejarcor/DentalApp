Imports System.Data
Imports System.Data.SqlClient
Imports VBDental.Data.Entities

Public Class UsuarioRepository

    Public Function ObtenerPorNombreUsuario(nombreUsuario As String) As Usuario
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "SELECT UsuarioId, NombreUsuario, PasswordHash, PasswordSalt, NombreCompleto, Rol, Activo " &
                "FROM dbo.Usuarios WHERE NombreUsuario = @NombreUsuario", conn)
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario)
                conn.Open()
                Using reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                    If reader.Read() Then
                        Return New Usuario With {
                            .UsuarioId = CInt(reader("UsuarioId")),
                            .NombreUsuario = CStr(reader("NombreUsuario")),
                            .PasswordHash = CStr(reader("PasswordHash")),
                            .PasswordSalt = CStr(reader("PasswordSalt")),
                            .NombreCompleto = CStr(reader("NombreCompleto")),
                            .Rol = CType([Enum].Parse(GetType(RolUsuario), CStr(reader("Rol"))), RolUsuario),
                            .Activo = CBool(reader("Activo"))
                        }
                    End If
                    Return Nothing
                End Using
            End Using
        End Using
    End Function

End Class
