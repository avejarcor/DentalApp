Partial Class FrmBitacora
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private lblTitulo As System.Windows.Forms.Label
    Private lblDesde As System.Windows.Forms.Label
    Private dtpDesde As System.Windows.Forms.DateTimePicker
    Private lblHasta As System.Windows.Forms.Label
    Private dtpHasta As System.Windows.Forms.DateTimePicker
    Private btnFiltrar As System.Windows.Forms.Button
    Private btnExportarCsv As System.Windows.Forms.Button
    Private dgvBitacora As System.Windows.Forms.DataGridView
    Private colFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colUsuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colAccion As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colDetalle As System.Windows.Forms.DataGridViewTextBoxColumn

    Private Sub InitializeComponent()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblDesde = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.lblHasta = New System.Windows.Forms.Label()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.btnExportarCsv = New System.Windows.Forms.Button()
        Me.dgvBitacora = New System.Windows.Forms.DataGridView()
        Me.colFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUsuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAccion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(20, 15)
        Me.lblTitulo.Text = "Bitácora de Eventos"
        '
        'lblDesde
        '
        Me.lblDesde.AutoSize = True
        Me.lblDesde.Location = New System.Drawing.Point(20, 55)
        Me.lblDesde.Text = "Desde:"
        '
        'dtpDesde
        '
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpDesde.Location = New System.Drawing.Point(70, 52)
        Me.dtpDesde.Size = New System.Drawing.Size(120, 23)
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Location = New System.Drawing.Point(210, 55)
        Me.lblHasta.Text = "Hasta:"
        '
        'dtpHasta
        '
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpHasta.Location = New System.Drawing.Point(260, 52)
        Me.dtpHasta.Size = New System.Drawing.Size(120, 23)
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Location = New System.Drawing.Point(400, 50)
        Me.btnFiltrar.Size = New System.Drawing.Size(100, 27)
        Me.btnFiltrar.Text = "Filtrar"
        Me.btnFiltrar.UseVisualStyleBackColor = True
        AddHandler Me.btnFiltrar.Click, AddressOf Me.btnFiltrar_Click
        '
        'btnExportarCsv
        '
        Me.btnExportarCsv.Location = New System.Drawing.Point(660, 50)
        Me.btnExportarCsv.Size = New System.Drawing.Size(120, 27)
        Me.btnExportarCsv.Text = "Exportar CSV"
        Me.btnExportarCsv.UseVisualStyleBackColor = True
        AddHandler Me.btnExportarCsv.Click, AddressOf Me.btnExportarCsv_Click
        '
        'dgvBitacora
        '
        Me.dgvBitacora.AllowUserToAddRows = False
        Me.dgvBitacora.AllowUserToDeleteRows = False
        Me.dgvBitacora.ReadOnly = True
        Me.dgvBitacora.AutoGenerateColumns = False
        Me.dgvBitacora.Location = New System.Drawing.Point(20, 90)
        Me.dgvBitacora.Size = New System.Drawing.Size(760, 400)
        Me.dgvBitacora.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
            Me.colFecha, Me.colUsuario, Me.colAccion, Me.colDetalle})
        '
        'colFecha
        '
        Me.colFecha.DataPropertyName = "Fecha"
        Me.colFecha.HeaderText = "Fecha"
        Me.colFecha.Width = 140
        Me.colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss"
        '
        'colUsuario
        '
        Me.colUsuario.DataPropertyName = "NombreUsuario"
        Me.colUsuario.HeaderText = "Usuario"
        Me.colUsuario.Width = 150
        '
        'colAccion
        '
        Me.colAccion.DataPropertyName = "Accion"
        Me.colAccion.HeaderText = "Acción"
        Me.colAccion.Width = 140
        '
        'colDetalle
        '
        Me.colDetalle.DataPropertyName = "Detalle"
        Me.colDetalle.HeaderText = "Detalle"
        Me.colDetalle.Width = 300
        '
        'FrmBitacora
        '
        Me.ClientSize = New System.Drawing.Size(800, 510)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblDesde)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.lblHasta)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.btnFiltrar)
        Me.Controls.Add(Me.btnExportarCsv)
        Me.Controls.Add(Me.dgvBitacora)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bitácora de Eventos"
        CType(Me.dgvBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
