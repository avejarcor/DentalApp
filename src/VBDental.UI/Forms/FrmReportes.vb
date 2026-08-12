Imports System.Linq
Imports System.Windows.Forms
Imports VBDental.Business.Services
Imports VBDental.Data.Entities

Public Class FrmReportes

    Private ReadOnly _agendaService As New AgendaService()
    Private ReadOnly _reservaService As New ReservaService()
    Private _resultadosActuales As List(Of Reserva) = New List(Of Reserva)
    Private _columnaOrdenActual As String = Nothing
    Private _ordenAscendente As Boolean = True

    Private Class MedicoComboItem
        Public Property MedicoId As Integer?
        Public Property Nombre As String
        Public Overrides Function ToString() As String
            Return Nombre
        End Function
    End Class

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDesde.Value = Date.Today.AddDays(-30)
        dtpHasta.Value = Date.Today.AddDays(30)

        Dim opciones As New List(Of MedicoComboItem) From {
            New MedicoComboItem With {.MedicoId = Nothing, .Nombre = "Todos los médicos"}
        }
        opciones.AddRange(_agendaService.ObtenerMedicos().Select(
            Function(m) New MedicoComboItem With {.MedicoId = m.MedicoId, .Nombre = m.Nombre}))

        cboMedico.DataSource = opciones
        cboMedico.DisplayMember = "Nombre"
        cboMedico.ValueMember = "MedicoId"

        GenerarReporte()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs)
        GenerarReporte()
    End Sub

    Private Sub btnExportarCsv_Click(sender As Object, e As EventArgs)
        ExportarACsv(dgvResultados, $"reporte_reservas_{Date.Today:yyyyMMdd}.csv")
    End Sub

    Private Sub GenerarReporte()
        Dim seleccion = TryCast(cboMedico.SelectedItem, MedicoComboItem)
        Dim medicoId As Integer? = If(seleccion Is Nothing, Nothing, seleccion.MedicoId)

        _resultadosActuales = _reservaService.ObtenerFiltradas(medicoId, dtpDesde.Value.Date, dtpHasta.Value.Date)
        _columnaOrdenActual = Nothing

        MostrarResultados()
        ActualizarGrafico()

        lblTotal.Text = $"Total: {_resultadosActuales.Count} reserva(s)"
    End Sub

    Private Sub MostrarResultados()
        dgvResultados.DataSource = Nothing
        dgvResultados.DataSource = _resultadosActuales
    End Sub

    Private Sub ActualizarGrafico()
        Dim serie = chartReservas.Series("Reservas")
        serie.Points.Clear()

        Dim porMedico = _resultadosActuales.
            GroupBy(Function(r) r.NombreMedico).
            Select(Function(g) New With {.Medico = g.Key, .Cantidad = g.Count()}).
            OrderByDescending(Function(x) x.Cantidad)

        For Each item In porMedico
            serie.Points.AddXY(item.Medico, item.Cantidad)
        Next

        If _resultadosActuales.Count = 0 Then
            serie.Points.AddXY("(sin datos)", 0)
        End If
    End Sub

    Private Sub dgvResultados_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        Dim columna = dgvResultados.Columns(e.ColumnIndex)
        Dim propiedad = columna.DataPropertyName
        If String.IsNullOrEmpty(propiedad) Then Return

        If _columnaOrdenActual = propiedad Then
            _ordenAscendente = Not _ordenAscendente
        Else
            _columnaOrdenActual = propiedad
            _ordenAscendente = True
        End If

        Dim ordenado As IEnumerable(Of Reserva)
        Select Case propiedad
            Case "NombreMedico"
                ordenado = _resultadosActuales.OrderBy(Function(r) r.NombreMedico)
            Case "NombreUsuario"
                ordenado = _resultadosActuales.OrderBy(Function(r) r.NombreUsuario)
            Case "Fecha"
                ordenado = _resultadosActuales.OrderBy(Function(r) r.Fecha).ThenBy(Function(r) r.HoraInicio)
            Case "HoraInicio"
                ordenado = _resultadosActuales.OrderBy(Function(r) r.HoraInicio)
            Case Else
                Return
        End Select

        If Not _ordenAscendente Then ordenado = ordenado.Reverse()

        _resultadosActuales = ordenado.ToList()
        MostrarResultados()
    End Sub

End Class
