Imports System.Security.Cryptography

Namespace Services

    ''' <summary>
    ''' PBKDF2-HMACSHA256, 10000 iteraciones, hash de 32 bytes. Debe coincidir con
    ''' los parámetros usados al generar los datos semilla (database/02_seed_data.sql).
    ''' </summary>
    Public Module PasswordHasher

        Private Const Iterations As Integer = 10000
        Private Const HashSizeBytes As Integer = 32
        Private Const SaltSizeBytes As Integer = 16

        Public Function GenerarSalt() As String
            Dim saltBytes(SaltSizeBytes - 1) As Byte
            Using rng = RandomNumberGenerator.Create()
                rng.GetBytes(saltBytes)
            End Using
            Return Convert.ToBase64String(saltBytes)
        End Function

        Public Function CalcularHash(password As String, saltBase64 As String) As String
            Dim saltBytes = Convert.FromBase64String(saltBase64)
            Using pbkdf2 As New Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256)
                Return Convert.ToBase64String(pbkdf2.GetBytes(HashSizeBytes))
            End Using
        End Function

        Public Function Verificar(password As String, saltBase64 As String, hashEsperado As String) As Boolean
            Dim hashCalculado = CalcularHash(password, saltBase64)
            Return FixedTimeEquals(hashCalculado, hashEsperado)
        End Function

        Private Function FixedTimeEquals(a As String, b As String) As Boolean
            If a.Length <> b.Length Then Return False
            Dim resultado As Integer = 0
            For i = 0 To a.Length - 1
                resultado = resultado Or (Asc(a(i)) Xor Asc(b(i)))
            Next
            Return resultado = 0
        End Function

    End Module

End Namespace
