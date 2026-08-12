Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VBDental.Business.Services

<TestClass>
Public Class PasswordHasherTests

    <TestMethod>
    Public Sub CalcularHash_EsDeterministico_ParaMismaClaveYSalt()
        Dim salt = PasswordHasher.GenerarSalt()

        Dim hash1 = PasswordHasher.CalcularHash("miClave123", salt)
        Dim hash2 = PasswordHasher.CalcularHash("miClave123", salt)

        Assert.AreEqual(hash1, hash2)
    End Sub

    <TestMethod>
    Public Sub CalcularHash_DifiereEntreClavesDistintas_ConMismoSalt()
        Dim salt = PasswordHasher.GenerarSalt()

        Dim hashA = PasswordHasher.CalcularHash("claveA", salt)
        Dim hashB = PasswordHasher.CalcularHash("claveB", salt)

        Assert.AreNotEqual(hashA, hashB)
    End Sub

    <TestMethod>
    Public Sub GenerarSalt_ProduceValoresDistintosCadaVez()
        Dim salt1 = PasswordHasher.GenerarSalt()
        Dim salt2 = PasswordHasher.GenerarSalt()

        Assert.AreNotEqual(salt1, salt2)
    End Sub

    <TestMethod>
    Public Sub Verificar_RetornaTrue_ParaClaveCorrecta()
        Dim salt = PasswordHasher.GenerarSalt()
        Dim hash = PasswordHasher.CalcularHash("clave-correcta", salt)

        Assert.IsTrue(PasswordHasher.Verificar("clave-correcta", salt, hash))
    End Sub

    <TestMethod>
    Public Sub Verificar_RetornaFalse_ParaClaveIncorrecta()
        Dim salt = PasswordHasher.GenerarSalt()
        Dim hash = PasswordHasher.CalcularHash("clave-correcta", salt)

        Assert.IsFalse(PasswordHasher.Verificar("clave-incorrecta", salt, hash))
    End Sub

End Class
