Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VBDental.Business.Services
Imports VBDental.Data.Entities

''' <summary>
''' Pruebas de integración: requieren la base VBDentalDb con los datos semilla
''' (database/01_create_tables.sql + 02_seed_data.sql) cargados en (localdb)\MSSQLLocalDB.
''' </summary>
<TestClass>
Public Class AuthServiceTests

    <TestMethod>
    Public Sub Login_Falla_ConCredencialesVacias()
        Dim auth As New AuthService()

        Dim resultado = auth.Login("", "")

        Assert.IsFalse(resultado.Exitoso)
    End Sub

    <TestMethod>
    Public Sub Login_TieneExito_ParaAdminConClaveCorrecta()
        Dim auth As New AuthService()

        Dim resultado = auth.Login("admin", "admin123")

        Assert.IsTrue(resultado.Exitoso)
        Assert.AreEqual(RolUsuario.Administrador, resultado.UsuarioAutenticado.Rol)
    End Sub

    <TestMethod>
    Public Sub Login_TieneExito_ParaUsuarioConClaveCorrecta()
        Dim auth As New AuthService()

        Dim resultado = auth.Login("usuario1", "user123")

        Assert.IsTrue(resultado.Exitoso)
        Assert.AreEqual(RolUsuario.Usuario, resultado.UsuarioAutenticado.Rol)
    End Sub

    <TestMethod>
    Public Sub Login_Falla_ConClaveIncorrecta()
        Dim auth As New AuthService()

        Dim resultado = auth.Login("admin", "clave-incorrecta")

        Assert.IsFalse(resultado.Exitoso)
    End Sub

    <TestMethod>
    Public Sub Login_Falla_ConUsuarioInexistente()
        Dim auth As New AuthService()

        Dim resultado = auth.Login("no-existe-" & Guid.NewGuid().ToString("N"), "cualquiera")

        Assert.IsFalse(resultado.Exitoso)
    End Sub

End Class
