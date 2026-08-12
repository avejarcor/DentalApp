Partial Class FrmReportes
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private lblTitulo As System.Windows.Forms.Label
    Private lblMedico As System.Windows.Forms.Label
    Private cboMedico As System.Windows.Forms.ComboBox
    Private lblDesde As System.Windows.Forms.Label
    Private dtpDesde As System.Windows.Forms.DateTimePicker
    Private lblHasta As System.Windows.Forms.Label
    Private dtpHasta As System.Windows.Forms.DateTimePicker
    Private btnGenerar As System.Windows.Forms.Button
    Private btnExportarCsv As System.Windows.Forms.Button
    Private chartReservas As System.Windows.Forms.DataVisualization.Charting.Chart
    Private lblDetalle As System.Windows.Forms.Label
    Private dgvResultados As System.Windows.Forms.DataGridView
    Private colMedico As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colUsuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colHora As System.Windows.Forms.DataGridViewTextBoxColumn
    Private lblTotal As System.Windows.Forms.Label

    Private Sub InitializeComponent()
        Dim chartArea1 As New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim legend1 As New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim series1 As New System.Windows.Forms.DataVisualization.Charting.Series()

        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblMedico = New System.Windows.Forms.Label()
        Me.cboMedico = New System.Windows.Forms.ComboBox()
        Me.lblDesde = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.lblHasta = New System.Windows.Forms.Label()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.btnExportarCsv = New System.Windows.Forms.Button()
        Me.chartReservas = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.lblDetalle = New System.Windows.Forms.Label()
        Me.dgvResultados = New System.Windows.Forms.DataGridView()
        Me.colMedico = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUsuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHora = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblTotal = New System.Windows.Forms.Label()
        CType(Me.chartReservas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvResultados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(20, 15)
        Me.lblTitulo.Text = "Reporte de Reservas"
        '
        'lblMedico
        '
        Me.lblMedico.AutoSize = True
        Me.lblMedico.Location = New System.Drawing.Point(20, 55)
        Me.lblMedico.Text = "Médico:"
        '
        'cboMedico
        '
        Me.cboMedico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMedico.Location = New System.Drawing.Point(80, 52)
        Me.cboMedico.Size = New System.Drawing.Size(220, 23)
        '
        'lblDesde
        '
        Me.lblDesde.AutoSize = True
        Me.lblDesde.Location = New System.Drawing.Point(320, 55)
        Me.lblDesde.Text = "Desde:"
        '
        'dtpDesde
        '
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpDesde.Location = New System.Drawing.Point(375, 52)
        Me.dtpDesde.Size = New System.Drawing.Size(110, 23)
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Location = New System.Drawing.Point(495, 55)
        Me.lblHasta.Text = "Hasta:"
        '
        'dtpHasta
        '
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpHasta.Location = New System.Drawing.Point(545, 52)
        Me.dtpHasta.Size = New System.Drawing.Size(110, 23)
        '
        'btnGenerar
        '
        Me.btnGenerar.Location = New System.Drawing.Point(670, 50)
        Me.btnGenerar.Size = New System.Drawing.Size(100, 27)
        Me.btnGenerar.Text = "Generar"
        Me.btnGenerar.UseVisualStyleBackColor = True
        AddHandler Me.btnGenerar.Click, AddressOf Me.btnGenerar_Click
        '
        'btnExportarCsv
        '
        Me.btnExportarCsv.Location = New System.Drawing.Point(780, 50)
        Me.btnExportarCsv.Size = New System.Drawing.Size(120, 27)
        Me.btnExportarCsv.Text = "Exportar CSV"
        Me.btnExportarCsv.UseVisualStyleBackColor = True
        AddHandler Me.btnExportarCsv.Click, AddressOf Me.btnExportarCsv_Click
        '
        'chartReservas
        '
        chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro
        chartArea1.AxisX.Title = "Médico"
        chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro
        chartArea1.AxisY.Title = "Cantidad de reservas"
        chartArea1.AxisY.Interval = 1
        chartArea1.Name = "AreaPrincipal"
        Me.chartReservas.ChartAreas.Add(chartArea1)
        legend1.Name = "Leyenda"
        Me.chartReservas.Legends.Add(legend1)
        series1.ChartArea = "AreaPrincipal"
        series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
        series1.Legend = "Leyenda"
        series1.Name = "Reservas"
        series1.IsValueShownAsLabel = True
        Me.chartReservas.Series.Add(series1)
        Me.chartReservas.Location = New System.Drawing.Point(20, 90)
        Me.chartReservas.Size = New System.Drawing.Size(860, 230)
        Me.chartReservas.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones
        Me.chartReservas.BackColor = System.Drawing.Color.White
        '
        'lblDetalle
        '
        Me.lblDetalle.AutoSize = True
        Me.lblDetalle.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblDetalle.Location = New System.Drawing.Point(20, 335)
        Me.lblDetalle.Text = "Detalle (clic en un encabezado para ordenar)"
        '
        'dgvResultados
        '
        Me.dgvResultados.AllowUserToAddRows = False
        Me.dgvResultados.AllowUserToDeleteRows = False
        Me.dgvResultados.ReadOnly = True
        Me.dgvResultados.AutoGenerateColumns = False
        Me.dgvResultados.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvResultados.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResultados.Location = New System.Drawing.Point(20, 360)
        Me.dgvResultados.Size = New System.Drawing.Size(860, 280)
        Me.dgvResultados.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
            Me.colMedico, Me.colUsuario, Me.colFecha, Me.colHora})
        AddHandler Me.dgvResultados.ColumnHeaderMouseClick, AddressOf Me.dgvResultados_ColumnHeaderMouseClick
        '
        'colMedico
        '
        Me.colMedico.DataPropertyName = "NombreMedico"
        Me.colMedico.HeaderText = "Médico"
        Me.colMedico.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.colMedico.Width = 240
        '
        'colUsuario
        '
        Me.colUsuario.DataPropertyName = "NombreUsuario"
        Me.colUsuario.HeaderText = "Usuario"
        Me.colUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.colUsuario.Width = 220
        '
        'colFecha
        '
        Me.colFecha.DataPropertyName = "Fecha"
        Me.colFecha.HeaderText = "Fecha"
        Me.colFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.colFecha.Width = 140
        Me.colFecha.DefaultCellStyle.Format = "dd/MM/yyyy"
        '
        'colHora
        '
        Me.colHora.DataPropertyName = "HoraInicio"
        Me.colHora.HeaderText = "Hora"
        Me.colHora.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.colHora.Width = 110
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.Location = New System.Drawing.Point(20, 650)
        Me.lblTotal.Text = "Total: 0 reservas"
        '
        'FrmReportes
        '
        Me.ClientSize = New System.Drawing.Size(900, 690)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblMedico)
        Me.Controls.Add(Me.cboMedico)
        Me.Controls.Add(Me.lblDesde)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.lblHasta)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.btnGenerar)
        Me.Controls.Add(Me.btnExportarCsv)
        Me.Controls.Add(Me.chartReservas)
        Me.Controls.Add(Me.lblDetalle)
        Me.Controls.Add(Me.dgvResultados)
        Me.Controls.Add(Me.lblTotal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reporte de Reservas"
        CType(Me.chartReservas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvResultados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
