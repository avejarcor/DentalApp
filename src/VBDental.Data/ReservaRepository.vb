Imports System.Data
Imports System.Data.SqlClient
Imports VBDental.Data.Entities

Public Class ReservaRepository

    ''' <summary>
    ''' Intenta reservar un horario de forma atómica: solo tiene éxito si el horario
    ''' sigue en estado 'Disponible' al momento de ejecutar el UPDATE (protege la
    ''' condición de carrera entre dos usuarios reservando el mismo horario a la vez).
    ''' </summary>
    Public Function IntentarReservar(horarioId As Integer, usuarioId As Integer) As Boolean
        Using conn = DbConnectionFactory.CreateConnection()
            conn.Open()
            Using tx = conn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim filasActualizadas As Integer
                    Using cmdUpdate As New SqlCommand(
                        "UPDATE dbo.HorariosAgenda SET Estado = @EstadoReservado " &
                        "WHERE HorarioId = @HorarioId AND Estado = @EstadoDisponible", conn, tx)
                        cmdUpdate.Parameters.AddWithValue("@HorarioId", horarioId)
                        cmdUpdate.Parameters.AddWithValue("@EstadoReservado", EstadoHorario.Reservado.ToString())
                        cmdUpdate.Parameters.AddWithValue("@EstadoDisponible", EstadoHorario.Disponible.ToString())
                        filasActualizadas = cmdUpdate.ExecuteNonQuery()
                    End Using

                    If filasActualizadas = 0 Then
                        tx.Rollback()
                        Return False
                    End If

                    Using cmdInsert As New SqlCommand(
                        "INSERT INTO dbo.Reservas (HorarioId, UsuarioId, Estado) " &
                        "VALUES (@HorarioId, @UsuarioId, @EstadoConfirmada)", conn, tx)
                        cmdInsert.Parameters.AddWithValue("@HorarioId", horarioId)
                        cmdInsert.Parameters.AddWithValue("@UsuarioId", usuarioId)
                        cmdInsert.Parameters.AddWithValue("@EstadoConfirmada", EstadoReserva.Confirmada.ToString())
                        cmdInsert.ExecuteNonQuery()
                    End Using

                    tx.Commit()
                    Return True
                Catch
                    tx.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    Public Function ObtenerTodas() As List(Of Reserva)
        Dim resultado As New List(Of Reserva)
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "SELECT r.ReservaId, r.HorarioId, r.UsuarioId, m.Nombre AS NombreMedico, " &
                "u.NombreCompleto AS NombreUsuario, h.Fecha, h.HoraInicio, r.FechaReserva, r.Estado " &
                "FROM dbo.Reservas r " &
                "INNER JOIN dbo.HorariosAgenda h ON h.HorarioId = r.HorarioId " &
                "INNER JOIN dbo.Medicos m ON m.MedicoId = h.MedicoId " &
                "INNER JOIN dbo.Usuarios u ON u.UsuarioId = r.UsuarioId " &
                "ORDER BY h.Fecha DESC, h.HoraInicio DESC", conn)
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        resultado.Add(New Reserva With {
                            .ReservaId = CInt(reader("ReservaId")),
                            .HorarioId = CInt(reader("HorarioId")),
                            .UsuarioId = CInt(reader("UsuarioId")),
                            .NombreMedico = CStr(reader("NombreMedico")),
                            .NombreUsuario = CStr(reader("NombreUsuario")),
                            .Fecha = CDate(reader("Fecha")),
                            .HoraInicio = CType(reader("HoraInicio"), TimeSpan),
                            .FechaReserva = CDate(reader("FechaReserva")),
                            .Estado = CType([Enum].Parse(GetType(EstadoReserva), CStr(reader("Estado"))), EstadoReserva)
                        })
                    End While
                End Using
            End Using
        End Using
        Return resultado
    End Function

    ''' <summary>Reporte de reservas confirmadas, filtrable por médico y rango de fechas.</summary>
    Public Function ObtenerFiltradas(medicoId As Integer?, desde As Date, hasta As Date) As List(Of Reserva)
        Dim resultado As New List(Of Reserva)
        Using conn = DbConnectionFactory.CreateConnection()
            Using cmd As New SqlCommand(
                "SELECT r.ReservaId, r.HorarioId, r.UsuarioId, m.Nombre AS NombreMedico, " &
                "u.NombreCompleto AS NombreUsuario, h.Fecha, h.HoraInicio, r.FechaReserva, r.Estado " &
                "FROM dbo.Reservas r " &
                "INNER JOIN dbo.HorariosAgenda h ON h.HorarioId = r.HorarioId " &
                "INNER JOIN dbo.Medicos m ON m.MedicoId = h.MedicoId " &
                "INNER JOIN dbo.Usuarios u ON u.UsuarioId = r.UsuarioId " &
                "WHERE h.Fecha BETWEEN @Desde AND @Hasta " &
                "AND (@MedicoId IS NULL OR h.MedicoId = @MedicoId) " &
                "AND r.Estado = @EstadoConfirmada " &
                "ORDER BY h.Fecha, h.HoraInicio", conn)
                cmd.Parameters.AddWithValue("@Desde", desde.Date)
                cmd.Parameters.AddWithValue("@Hasta", hasta.Date)
                cmd.Parameters.AddWithValue("@MedicoId", If(CObj(medicoId), CObj(DBNull.Value)))
                cmd.Parameters.AddWithValue("@EstadoConfirmada", EstadoReserva.Confirmada.ToString())
                conn.Open()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        resultado.Add(New Reserva With {
                            .ReservaId = CInt(reader("ReservaId")),
                            .HorarioId = CInt(reader("HorarioId")),
                            .UsuarioId = CInt(reader("UsuarioId")),
                            .NombreMedico = CStr(reader("NombreMedico")),
                            .NombreUsuario = CStr(reader("NombreUsuario")),
                            .Fecha = CDate(reader("Fecha")),
                            .HoraInicio = CType(reader("HoraInicio"), TimeSpan),
                            .FechaReserva = CDate(reader("FechaReserva")),
                            .Estado = CType([Enum].Parse(GetType(EstadoReserva), CStr(reader("Estado"))), EstadoReserva)
                        })
                    End While
                End Using
            End Using
        End Using
        Return resultado
    End Function

End Class
