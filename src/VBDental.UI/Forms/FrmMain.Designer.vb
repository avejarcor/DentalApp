Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private lblBienvenida As System.Windows.Forms.Label
    Private btnAgenda As System.Windows.Forms.Button
    Private btnAdmin As System.Windows.Forms.Button
    Private btnBitacora As System.Windows.Forms.Button
    Private btnReportes As System.Windows.Forms.Button
    Private btnCerrarSesion As System.Windows.Forms.Button

    Private Sub InitializeComponent()
        Me.lblBienvenida = New System.Windows.Forms.Label()
        Me.btnAgenda = New System.Windows.Forms.Button()
        Me.btnAdmin = New System.Windows.Forms.Button()
        Me.btnBitacora = New System.Windows.Forms.Button()
        Me.btnReportes = New System.Windows.Forms.Button()
        Me.btnCerrarSesion = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblBienvenida
        '
        Me.lblBienvenida.AutoSize = True
        Me.lblBienvenida.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblBienvenida.Location = New System.Drawing.Point(30, 25)
        Me.lblBienvenida.Text = "Bienvenido/a"
        '
        'btnAgenda
        '
        Me.btnAgenda.Location = New System.Drawing.Point(30, 80)
        Me.btnAgenda.Size = New System.Drawing.Size(300, 40)
        Me.btnAgenda.Text = "Agenda Médica / Reservar Hora"
        Me.btnAgenda.UseVisualStyleBackColor = True
        AddHandler Me.btnAgenda.Click, AddressOf Me.btnAgenda_Click
        '
        'btnAdmin
        '
        Me.btnAdmin.Location = New System.Drawing.Point(30, 130)
        Me.btnAdmin.Size = New System.Drawing.Size(300, 40)
        Me.btnAdmin.Text = "Administración de Reservas"
        Me.btnAdmin.UseVisualStyleBackColor = True
        AddHandler Me.btnAdmin.Click, AddressOf Me.btnAdmin_Click
        '
        'btnBitacora
        '
        Me.btnBitacora.Location = New System.Drawing.Point(30, 180)
        Me.btnBitacora.Size = New System.Drawing.Size(300, 40)
        Me.btnBitacora.Text = "Bitácora de Eventos"
        Me.btnBitacora.UseVisualStyleBackColor = True
        AddHandler Me.btnBitacora.Click, AddressOf Me.btnBitacora_Click
        '
        'btnReportes
        '
        Me.btnReportes.Location = New System.Drawing.Point(30, 230)
        Me.btnReportes.Size = New System.Drawing.Size(300, 40)
        Me.btnReportes.Text = "Reporte de Reservas"
        Me.btnReportes.UseVisualStyleBackColor = True
        AddHandler Me.btnReportes.Click, AddressOf Me.btnReportes_Click
        '
        'btnCerrarSesion
        '
        Me.btnCerrarSesion.Location = New System.Drawing.Point(30, 290)
        Me.btnCerrarSesion.Size = New System.Drawing.Size(300, 32)
        Me.btnCerrarSesion.Text = "Cerrar sesión"
        Me.btnCerrarSesion.UseVisualStyleBackColor = True
        AddHandler Me.btnCerrarSesion.Click, AddressOf Me.btnCerrarSesion_Click
        '
        'FrmMain
        '
        Me.ClientSize = New System.Drawing.Size(360, 350)
        Me.Controls.Add(Me.lblBienvenida)
        Me.Controls.Add(Me.btnAgenda)
        Me.Controls.Add(Me.btnAdmin)
        Me.Controls.Add(Me.btnBitacora)
        Me.Controls.Add(Me.btnReportes)
        Me.Controls.Add(Me.btnCerrarSesion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menú Principal"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
