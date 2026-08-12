Namespace Entities

    Public Class HorarioAgenda
        Public Property HorarioId As Integer
        Public Property MedicoId As Integer
        Public Property NombreMedico As String
        Public Property Fecha As Date
        Public Property HoraInicio As TimeSpan
        Public Property HoraFin As TimeSpan
        Public Property Estado As EstadoHorario
    End Class

End Namespace
