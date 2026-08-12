Imports System.Windows.Forms
Imports VBDental.Business.Services
Imports VBDental.Data.Entities

Public Class FrmAgenda

    Private ReadOnly _usuarioActual As Usuario
    Private ReadOnly _agendaService As New AgendaService()
    Private ReadOnly _reservaService As New ReservaService()

    Public Sub New(usuarioActual As Usuario)
        InitializeComponent()
        _usuarioActual = usuarioActual
    End Sub

    Private Sub FrmAgenda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFecha.MinDate = Date.Today
        dtpFecha.Value = Date.Today

        Dim medicos = _agendaService.ObtenerMedicos()
        cboMedico.DataSource = medicos
        cboMedico.DisplayMember = "Nombre"
        cboMedico.ValueMember = "MedicoId"

        If medicos.Count > 0 Then
            CargarHorarios()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs)
        CargarHorarios()
    End Sub

    Private Sub CargarHorarios()
        lblMensaje.Text = ""
        If cboMedico.SelectedValue Is Nothing Then Return

        Dim medicoId = CInt(cboMedico.SelectedValue)
        Dim horarios = _agendaService.ObtenerHorarios(medicoId, dtpFecha.Value.Date)
        dgvHorarios.DataSource = horarios
        ColorearFilasPorEstado()
    End Sub

    Private Sub ColorearFilasPorEstado()
        For Each fila As DataGridViewRow In dgvHorarios.Rows
            Dim horario = TryCast(fila.DataBoundItem, HorarioAgenda)
            If horario Is Nothing Then Continue For

            Select Case horario.Estado
                Case EstadoHorario.Disponible
                    fila.DefaultCellStyle.BackColor = Drawing.Color.Honeydew
                Case EstadoHorario.Reservado
                    fila.DefaultCellStyle.BackColor = Drawing.Color.MistyRose
                Case EstadoHorario.Bloqueado
                    fila.DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
            End Select
        Next
    End Sub

    Private Sub btnReservar_Click(sender As Object, e As EventArgs)
        lblMensaje.Text = ""
        lblMensaje.ForeColor = Drawing.Color.Firebrick

        If dgvHorarios.CurrentRow Is Nothing Then
            lblMensaje.Text = "Seleccione un horario de la agenda."
            Return
        End If

        Dim horario = CType(dgvHorarios.CurrentRow.DataBoundItem, HorarioAgenda)

        If horario.Estado <> EstadoHorario.Disponible Then
            lblMensaje.Text = "Ese horario ya no está disponible. Seleccione otro."
            CargarHorarios()
            Return
        End If

        Dim mensajeConfirmacion = $"¿Confirma la reserva con {horario.NombreMedico} el {horario.Fecha:dd/MM/yyyy} a las {horario.HoraInicio:hh\:mm}?"
        If MessageBox.Show(mensajeConfirmacion, "Confirmar reserva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim resultado = _reservaService.Reservar(horario.HorarioId, _usuarioActual.UsuarioId)

        If resultado.Exitoso Then
            lblMensaje.ForeColor = Drawing.Color.DarkGreen
        End If
        lblMensaje.Text = resultado.Mensaje

        CargarHorarios()
    End Sub

End Class
