Imports System.Data.SqlClient
Imports VBDental.Data.Entities

Public Class MedicoRepository

    Public Function ObtenerTodos() As List(Of Medico)
        Dim resultado As New List(Of Medico)
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "SELECT MedicoId, Nombre, Especialidad, Activo FROM dbo.Medicos WHERE Activo = 1 ORDER BY Nombre", conn)
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        resultado.Add(New Medico With {
                            .MedicoId = CInt(reader("MedicoId")),
                            .Nombre = CStr(reader("Nombre")),
                            .Especialidad = CStr(reader("Especialidad")),
                            .Activo = CBool(reader("Activo"))
                        })
                    End While
                End Using
            End Using
        End Using
        Return resultado
    End Function

End Class
