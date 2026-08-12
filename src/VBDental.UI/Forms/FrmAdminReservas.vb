Imports VBDental.Business.Services

Public Class FrmAdminReservas

    Private ReadOnly _reservaService As New ReservaService()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmAdminReservas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarReservas()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs)
        CargarReservas()
    End Sub

    Private Sub btnExportarCsv_Click(sender As Object, e As EventArgs)
        ExportarACsv(dgvReservas, $"reservas_{Date.Today:yyyyMMdd}.csv")
    End Sub

    Private Sub CargarReservas()
        dgvReservas.DataSource = _reservaService.ObtenerTodas()
    End Sub

End Class
