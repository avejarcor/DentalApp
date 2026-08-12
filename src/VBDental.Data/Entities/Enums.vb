Namespace Entities

    ''' <summary>Los nombres deben coincidir exactamente con los valores permitidos por los CHECK de la BD.</summary>
    Public Enum RolUsuario
        Usuario
        Administrador
    End Enum

    Public Enum EstadoHorario
        Disponible
        Reservado
        Bloqueado
    End Enum

    Public Enum EstadoReserva
        Confirmada
        Cancelada
    End Enum

End Namespace
