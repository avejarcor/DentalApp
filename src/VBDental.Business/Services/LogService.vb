Imports VBDental.Business.Models
Imports VBDental.Data
Imports VBDental.Data.Entities

Namespace Services

    Public Class LogService

        Private ReadOnly _logRepository As New LogRepository()

        Public Sub Registrar(usuarioId As Integer?, tipoEvento As TipoEventoBitacora, detalle As String)
            _logRepository.Registrar(usuarioId, tipoEvento.ToString(), detalle)
        End Sub

        Public Function ObtenerBitacora(desde As Date, hasta As Date) As List(Of BitacoraEntry)
            Return _logRepository.ObtenerEntre(desde, hasta)
        End Function

    End Class

End Namespace
