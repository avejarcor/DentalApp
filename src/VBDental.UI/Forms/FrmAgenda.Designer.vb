Partial Class FrmAgenda
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private lblMedico As System.Windows.Forms.Label
    Private cboMedico As System.Windows.Forms.ComboBox
    Private lblFecha As System.Windows.Forms.Label
    Private dtpFecha As System.Windows.Forms.DateTimePicker
    Private btnBuscar As System.Windows.Forms.Button
    Private dgvHorarios As System.Windows.Forms.DataGridView
    Private colHorarioId As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colHoraInicio As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colHoraFin As System.Windows.Forms.DataGridViewTextBoxColumn
    Private colEstado As System.Windows.Forms.DataGridViewTextBoxColumn
    Private btnReservar As System.Windows.Forms.Button
    Private lblMensaje As System.Windows.Forms.Label

    Private Sub InitializeComponent()
        Me.lblMedico = New System.Windows.Forms.Label()
        Me.cboMedico = New System.Windows.Forms.ComboBox()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.dgvHorarios = New System.Windows.Forms.DataGridView()
        Me.colHorarioId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHoraInicio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colHoraFin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnReservar = New System.Windows.Forms.Button()
        Me.lblMensaje = New System.Windows.Forms.Label()
        CType(Me.dgvHorarios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMedico
        '
        Me.lblMedico.AutoSize = True
        Me.lblMedico.Location = New System.Drawing.Point(20, 20)
        Me.lblMedico.Text = "Médico:"
        '
        'cboMedico
        '
        Me.cboMedico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMedico.Location = New System.Drawing.Point(100, 17)
        Me.cboMedico.Size = New System.Drawing.Size(260, 23)
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Location = New System.Drawing.Point(380, 20)
        Me.lblFecha.Text = "Fecha:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFecha.Location = New System.Drawing.Point(440, 17)
        Me.dtpFecha.Size = New System.Drawing.Size(120, 23)
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(580, 15)
        Me.btnBuscar.Size = New System.Drawing.Size(100, 27)
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        AddHandler Me.btnBuscar.Click, AddressOf Me.btnBuscar_Click
        '
        'dgvHorarios
        '
        Me.dgvHorarios.AllowUserToAddRows = False
        Me.dgvHorarios.AllowUserToDeleteRows = False
        Me.dgvHorarios.ReadOnly = True
        Me.dgvHorarios.AutoGenerateColumns = False
        Me.dgvHorarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvHorarios.MultiSelect = False
        Me.dgvHorarios.Location = New System.Drawing.Point(20, 60)
        Me.dgvHorarios.Size = New System.Drawing.Size(660, 320)
        Me.dgvHorarios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
            Me.colHorarioId, Me.colHoraInicio, Me.colHoraFin, Me.colEstado})
        '
        'colHorarioId
        '
        Me.colHorarioId.DataPropertyName = "HorarioId"
        Me.colHorarioId.HeaderText = "Id"
        Me.colHorarioId.Visible = False
        '
        'colHoraInicio
        '
        Me.colHoraInicio.DataPropertyName = "HoraInicio"
        Me.colHoraInicio.HeaderText = "Hora Inicio"
        Me.colHoraInicio.Width = 150
        '
        'colHoraFin
        '
        Me.colHoraFin.DataPropertyName = "HoraFin"
        Me.colHoraFin.HeaderText = "Hora Fin"
        Me.colHoraFin.Width = 150
        '
        'colEstado
        '
        Me.colEstado.DataPropertyName = "Estado"
        Me.colEstado.HeaderText = "Estado"
        Me.colEstado.Width = 150
        '
        'btnReservar
        '
        Me.btnReservar.Location = New System.Drawing.Point(20, 395)
        Me.btnReservar.Size = New System.Drawing.Size(160, 32)
        Me.btnReservar.Text = "Reservar Hora"
        Me.btnReservar.UseVisualStyleBackColor = True
        AddHandler Me.btnReservar.Click, AddressOf Me.btnReservar_Click
        '
        'lblMensaje
        '
        Me.lblMensaje.AutoSize = True
        Me.lblMensaje.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblMensaje.Location = New System.Drawing.Point(200, 402)
        Me.lblMensaje.Size = New System.Drawing.Size(480, 20)
        Me.lblMensaje.Text = ""
        '
        'FrmAgenda
        '
        Me.ClientSize = New System.Drawing.Size(700, 450)
        Me.Controls.Add(Me.lblMedico)
        Me.Controls.Add(Me.cboMedico)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.dgvHorarios)
        Me.Controls.Add(Me.btnReservar)
        Me.Controls.Add(Me.lblMensaje)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Agenda Médica y Reserva de Horas"
        CType(Me.dgvHorarios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
