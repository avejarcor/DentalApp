Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VBDental.Business.Models
Imports VBDental.Business.Services
Imports VBDental.Data.Entities

''' <summary>
''' Pruebas de integración contra (localdb)\MSSQLLocalDB / VBDentalDb.
''' </summary>
<TestClass>
Public Class ReservaServiceTests

    Private Function BuscarHorarioDisponible() As HorarioAgenda
        Dim agenda As New AgendaService()
        For Each medico In agenda.ObtenerMedicos()
            For dias = 0 To 9
                Dim horarios = agenda.ObtenerHorarios(medico.MedicoId, Date.Today.AddDays(dias))
                Dim libre = horarios.Find(Function(h) h.Estado = EstadoHorario.Disponible)
                If libre IsNot Nothing Then Return libre
            Next
        Next
        Return Nothing
    End Function

    <TestMethod>
    Public Sub Reservar_TieneExito_ParaHorarioDisponible()
        Dim horario = BuscarHorarioDisponible()
        Assert.IsNotNull(horario, "No hay horarios disponibles para probar; recargue database/02_seed_data.sql.")

        Dim usuario = New AuthService().Login("usuario1", "user123").UsuarioAutenticado
        Dim reservas As New ReservaService()

        Dim resultado = reservas.Reservar(horario.HorarioId, usuario.UsuarioId)

        Assert.IsTrue(resultado.Exitoso)
    End Sub

    <TestMethod>
    Public Sub Reservar_ElMismoHorarioDosVeces_SoloUnaTieneExito()
        Dim usuario = New AuthService().Login("usuario1", "user123").UsuarioAutenticado
        Dim reservas As New ReservaService()
        Dim horario = BuscarHorarioDisponible()
        Assert.IsNotNull(horario, "No hay horarios disponibles para probar; recargue database/02_seed_data.sql.")

        Dim primerIntento = reservas.Reservar(horario.HorarioId, usuario.UsuarioId)
        Dim segundoIntento = reservas.Reservar(horario.HorarioId, usuario.UsuarioId)

        Assert.IsTrue(primerIntento.Exitoso)
        Assert.IsFalse(segundoIntento.Exitoso)
    End Sub

    ''' <summary>
    ''' Prueba de condición de carrera real: dos hilos intentan reservar el MISMO
    ''' horario al mismo instante (sincronizados con una Barrier). Debe ganar exactamente uno.
    ''' </summary>
    <TestMethod>
    Public Sub Reservar_Concurrente_SoloUnGanador()
        Dim horario = BuscarHorarioDisponible()
        Assert.IsNotNull(horario, "No hay horarios disponibles para probar; recargue database/02_seed_data.sql.")

        Dim usuarioA = New AuthService().Login("admin", "admin123").UsuarioAutenticado
        Dim usuarioB = New AuthService().Login("usuario1", "user123").UsuarioAutenticado
        Dim reservas As New ReservaService()

        Dim resultadoA As ReservaResult = Nothing
        Dim resultadoB As ReservaResult = Nothing
        Dim barrera As New Barrier(2)

        Dim t1 = Task.Run(Sub()
                               barrera.SignalAndWait()
                               resultadoA = reservas.Reservar(horario.HorarioId, usuarioA.UsuarioId)
                           End Sub)
        Dim t2 = Task.Run(Sub()
                               barrera.SignalAndWait()
                               resultadoB = reservas.Reservar(horario.HorarioId, usuarioB.UsuarioId)
                           End Sub)
        Task.WaitAll(t1, t2)

        Dim exitosos = New Boolean() {resultadoA.Exitoso, resultadoB.Exitoso}.Count(Function(x) x)
        Assert.AreEqual(1, exitosos, "Debe ganar exactamente un hilo la reserva concurrente.")
    End Sub

End Class
