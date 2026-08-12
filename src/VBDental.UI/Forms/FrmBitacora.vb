Imports VBDental.Business.Services

Public Class FrmBitacora

    Private ReadOnly _logService As New LogService()

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmBitacora_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpDesde.Value = Date.Today.AddDays(-7)
        dtpHasta.Value = Date.Today
        CargarBitacora()
    End Sub

    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs)
        CargarBitacora()
    End Sub

    Private Sub btnExportarCsv_Click(sender As Object, e As EventArgs)
        ExportarACsv(dgvBitacora, $"bitacora_{Date.Today:yyyyMMdd}.csv")
    End Sub

    Private Sub CargarBitacora()
        dgvBitacora.DataSource = _logService.ObtenerBitacora(dtpDesde.Value, dtpHasta.Value)
    End Sub

End Class
