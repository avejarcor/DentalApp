Imports System.Data.SqlClient
Imports VBDental.Data.Entities

Public Class LogRepository

    Public Sub Registrar(usuarioId As Integer?, accion As String, detalle As String)
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "INSERT INTO dbo.Bitacora (UsuarioId, Accion, Detalle) VALUES (@UsuarioId, @Accion, @Detalle)", conn)
                If usuarioId.HasValue Then
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId.Value)
                Else
                    cmd.Parameters.AddWithValue("@UsuarioId", DBNull.Value)
                End If
                cmd.Parameters.AddWithValue("@Accion", accion)
                cmd.Parameters.AddWithValue("@Detalle", If(CObj(detalle), CObj(DBNull.Value)))
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function ObtenerEntre(desde As Date, hasta As Date) As List(Of BitacoraEntry)
        Dim resultado As New List(Of BitacoraEntry)
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "SELECT b.LogId, ISNULL(u.NombreCompleto, '(sistema)') AS NombreUsuario, " &
                "b.Accion, b.Detalle, b.Fecha " &
                "FROM dbo.Bitacora b " &
                "LEFT JOIN dbo.Usuarios u ON u.UsuarioId = b.UsuarioId " &
                "WHERE b.Fecha >= @Desde AND b.Fecha < @Hasta " &
                "ORDER BY b.Fecha DESC", conn)
                cmd.Parameters.AddWithValue("@Desde", desde.Date)
                cmd.Parameters.AddWithValue("@Hasta", hasta.Date.AddDays(1))
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        resultado.Add(New BitacoraEntry With {
                            .LogId = CInt(reader("LogId")),
                            .NombreUsuario = CStr(reader("NombreUsuario")),
                            .Accion = CStr(reader("Accion")),
                            .Detalle = If(reader("Detalle") Is DBNull.Value, "", CStr(reader("Detalle"))),
                            .Fecha = CDate(reader("Fecha"))
                        })
                    End While
                End Using
            End Using
        End Using
        Return resultado
    End Function

End Class
