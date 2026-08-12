Namespace Entities

    Public Class Reserva
        Public Property ReservaId As Integer
        Public Property HorarioId As Integer
        Public Property UsuarioId As Integer
        Public Property NombreMedico As String
        Public Property NombreUsuario As String
        Public Property Fecha As Date
        Public Property HoraInicio As TimeSpan
        Public Property FechaReserva As DateTime
        Public Property Estado As EstadoReserva
    End Class

End Namespace
