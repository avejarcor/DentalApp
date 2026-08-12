Imports VBDental.Business.Models
Imports VBDental.Data
Imports VBDental.Data.Entities

Namespace Services

    Public Class ReservaService

        Private ReadOnly _reservaRepository As New ReservaRepository()
        Private ReadOnly _logService As New LogService()

        Public Function Reservar(horarioId As Integer, usuarioId As Integer) As ReservaResult
            Dim exito = _reservaRepository.IntentarReservar(horarioId, usuarioId)

            If Not exito Then
                _logService.Registrar(usuarioId, TipoEventoBitacora.ReservaFallida, $"HorarioId={horarioId} ya no estaba disponible")
                Return New ReservaResult With {
                    .Exitoso = False,
                    .Mensaje = "Ese horario ya fue reservado por otro usuario. Por favor seleccione otro."
                }
            End If

            _logService.Registrar(usuarioId, TipoEventoBitacora.ReservaConfirmada, $"HorarioId={horarioId}")
            Return New ReservaResult With {.Exitoso = True, .Mensaje = "Reserva confirmada correctamente."}
        End Function

        Public Function ObtenerTodas() As List(Of Reserva)
            Return _reservaRepository.ObtenerTodas()
        End Function

        Public Function ObtenerFiltradas(medicoId As Integer?, desde As Date, hasta As Date) As List(Of Reserva)
            Return _reservaRepository.ObtenerFiltradas(medicoId, desde, hasta)
        End Function

    End Class

End Namespace
