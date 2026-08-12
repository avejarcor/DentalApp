Partial Class FrmAdminReservas
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private lblTitulo As System.Windows.Forms.Label
    Private btnActualizar As System.Windows.Forms.Button
    Private btnExportarCsv As System.Windows.Forms.Button
    Private dgvReservas As System.Windows.Forms.DataGridView
    Private colMedico As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colUsuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colHora As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colFechaReserva As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colEstado As System.Windows.Forms.DataGridViewTextBoxColumn

    Private Sub InitializeComponent()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.btnExportarCsv = New System.Windows.Forms.Button()
        Me.dgvReservas = New System.Windows.Forms.DataGridView()
        Me.colMedico = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUsuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHora = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFechaReserva = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvReservas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.Location = New System.Drawing.Point(20, 15)
        Me.lblTitulo.Text = "Listado de Reservas"
        '
        'btnActualizar
        '
        Me.btnActualizar.Location = New System.Drawing.Point(540, 15)
        Me.btnActualizar.Size = New System.Drawing.Size(110, 30)
        Me.btnActualizar.Text = "Actualizar"
        Me.btnActualizar.UseVisualStyleBackColor = True
        AddHandler Me.btnActualizar.Click, AddressOf Me.btnActualizar_Click
        '
        'btnExportarCsv
        '
        Me.btnExportarCsv.Location = New System.Drawing.Point(660, 15)
        Me.btnExportarCsv.Size = New System.Drawing.Size(120, 30)
        Me.btnExportarCsv.Text = "Exportar CSV"
        Me.btnExportarCsv.UseVisualStyleBackColor = True
        AddHandler Me.btnExportarCsv.Click, AddressOf Me.btnExportarCsv_Click
        '
        'dgvReservas
        '
        Me.dgvReservas.AllowUserToAddRows = False
        Me.dgvReservas.AllowUserToDeleteRows = False
        Me.dgvReservas.ReadOnly = True
        Me.dgvReservas.AutoGenerateColumns = False
        Me.dgvReservas.Location = New System.Drawing.Point(20, 55)
        Me.dgvReservas.Size = New System.Drawing.Size(760, 400)
        Me.dgvReservas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
            Me.colMedico, Me.colUsuario, Me.colFecha, Me.colHora, Me.colFechaReserva, Me.colEstado})
        '
        'colMedico
        '
        Me.colMedico.DataPropertyName = "NombreMedico"
        Me.colMedico.HeaderText = "Médico"
        Me.colMedico.Width = 160
        '
        'colUsuario
        '
        Me.colUsuario.DataPropertyName = "NombreUsuario"
        Me.colUsuario.HeaderText = "Usuario"
        Me.colUsuario.Width = 150
        '
        'colFecha
        '
        Me.colFecha.DataPropertyName = "Fecha"
        Me.colFecha.HeaderText = "Fecha Hora Médica"
        Me.colFecha.Width = 130
        Me.colFecha.DefaultCellStyle.Format = "dd/MM/yyyy"
        '
        'colHora
        '
        Me.colHora.DataPropertyName = "HoraInicio"
        Me.colHora.HeaderText = "Hora"
        Me.colHora.Width = 90
        '
        'colFechaReserva
        '
        Me.colFechaReserva.DataPropertyName = "FechaReserva"
        Me.colFechaReserva.HeaderText = "Reservado el"
        Me.colFechaReserva.Width = 140
        Me.colFechaReserva.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
        '
        'colEstado
        '
        Me.colEstado.DataPropertyName = "Estado"
        Me.colEstado.HeaderText = "Estado"
        Me.colEstado.Width = 90
        '
        'FrmAdminReservas
        '
        Me.ClientSize = New System.Drawing.Size(800, 470)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.btnActualizar)
        Me.Controls.Add(Me.btnExportarCsv)
        Me.Controls.Add(Me.dgvReservas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Administración de Reservas"
        CType(Me.dgvReservas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
