Imports VBDental.Data
Imports VBDental.Data.Entities

Namespace Services

    Public Class AgendaService

        Private ReadOnly _medicoRepository As New MedicoRepository()
        Private ReadOnly _agendaRepository As New AgendaRepository()

        Public Function ObtenerMedicos() As List(Of Medico)
            Return _medicoRepository.ObtenerTodos()
        End Function

        Public Function ObtenerHorarios(medicoId As Integer, fecha As Date) As List(Of HorarioAgenda)
            Return _agendaRepository.ObtenerHorarios(medicoId, fecha)
        End Function

    End Class

End Namespace
