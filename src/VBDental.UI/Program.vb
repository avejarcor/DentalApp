Imports System.Threading
Imports System.Windows.Forms
Imports VBDental.Business.Models
Imports VBDental.Business.Services

Public Module Program

    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        AddHandler Application.ThreadException, AddressOf OnThreadException
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnUnhandledException

        Application.Run(New FrmLogin())
    End Sub

    Private Sub OnThreadException(sender As Object, e As ThreadExceptionEventArgs)
        ManejarErrorNoControlado(e.Exception)
    End Sub

    Private Sub OnUnhandledException(sender As Object, e As UnhandledExceptionEventArgs)
        ManejarErrorNoControlado(TryCast(e.ExceptionObject, Exception))
    End Sub

    ''' <summary>
    ''' Evita que un error no previsto (ej. caída de la BD) cierre la aplicación sin
    ''' aviso: se registra en la bitácora (si es posible) y se informa al usuario.
    ''' </summary>
    Private Sub ManejarErrorNoControlado(ex As Exception)
        Try
            Dim logService As New LogService()
            logService.Registrar(Nothing, TipoEventoBitacora.ErrorNoControlado, ex?.ToString())
        Catch
            ' Si ni siquiera se puede registrar el error (p.ej. BD no disponible), se ignora
            ' para no producir un segundo error dentro del manejador de errores.
        End Try

        MessageBox.Show(
            $"Ocurrió un error inesperado y la operación no pudo completarse.{Environment.NewLine}{Environment.NewLine}{ex?.Message}",
            "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Module
