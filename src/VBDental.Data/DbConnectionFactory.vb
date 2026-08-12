Imports System.Configuration
Imports System.Data.SqlClient

Public Module DbConnectionFactory

    Private ReadOnly ConnectionStringValue As String =
        ConfigurationManager.ConnectionStrings("VBDentalDb").ConnectionString

    Public Function CreateConnection() As SqlConnection
        Return New SqlConnection(ConnectionStringValue)
    End Function

End Module
