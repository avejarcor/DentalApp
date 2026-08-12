Imports System.Data.SqlClient
Imports VBDental.Data.Entities

Public Class AgendaRepository

    Public Function ObtenerHorarios(medicoId As Integer, fecha As Date) As List(Of HorarioAgenda)
        Dim resultado As New List(Of HorarioAgenda)
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "SELECT h.HorarioId, h.MedicoId, m.Nombre AS NombreMedico, h.Fecha, h.HoraInicio, h.HoraFin, h.Estado " &
                "FROM dbo.HorariosAgenda h " &
                "INNER JOIN dbo.Medicos m ON m.MedicoId = h.MedicoId " &
                "WHERE h.MedicoId = @MedicoId AND h.Fecha = @Fecha " &
                "ORDER BY h.HoraInicio", conn)
                cmd.Parameters.AddWithValue("@MedicoId", medicoId)
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date)
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        resultado.Add(New HorarioAgenda With {
                            .HorarioId = CInt(reader("HorarioId")),
                            .MedicoId = CInt(reader("MedicoId")),
                            .NombreMedico = CStr(reader("NombreMedico")),
                            .Fecha = CDate(reader("Fecha")),
                            .HoraInicio = CType(reader("HoraInicio"), TimeSpan),
                            .HoraFin = CType(reader("HoraFin"), TimeSpan),
                            .Estado = CType([Enum].Parse(GetType(EstadoHorario), CStr(reader("Estado"))), EstadoHorario)
                        })
                    End While
                End Using
            End Using
        End Using
        Return resultado
    End Function

End Class
