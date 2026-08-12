-- ============================================================
-- VB-Dental - Sistema de Reservas de Horas Médicas
-- Script de creación de base de datos y tablas
-- Motor: SQL Server (LocalDB / Express)
-- ============================================================

IF DB_ID('VBDentalDb') IS NULL
BEGIN
    CREATE DATABASE VBDentalDb;
END
GO

USE VBDentalDb;
GO

SET QUOTED_IDENTIFIER ON;
GO

-- ------------------------------------------------------------
-- Usuarios (login + rol)
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL DROP TABLE dbo.Usuarios;
GO
CREATE TABLE dbo.Usuarios
(
    UsuarioId       INT             IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    NombreUsuario   NVARCHAR(50)    NOT NULL,
    PasswordHash    NVARCHAR(256)   NOT NULL,
    PasswordSalt    NVARCHAR(256)   NOT NULL,
    NombreCompleto  NVARCHAR(150)   NOT NULL,
    Rol             NVARCHAR(20)    NOT NULL,
    Activo          BIT             NOT NULL DEFAULT (1),
    FechaCreacion   DATETIME        NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario),
    CONSTRAINT CK_Usuarios_Rol CHECK (Rol IN (N'Administrador', N'Usuario'))
);
GO

-- ------------------------------------------------------------
-- Medicos
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Medicos', 'U') IS NOT NULL DROP TABLE dbo.Medicos;
GO
CREATE TABLE dbo.Medicos
(
    MedicoId        INT             IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    Nombre          NVARCHAR(150)   NOT NULL,
    Especialidad    NVARCHAR(100)   NOT NULL,
    Activo          BIT             NOT NULL DEFAULT (1)
);
GO

-- ------------------------------------------------------------
-- HorariosAgenda (slots de agenda por médico)
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.HorariosAgenda', 'U') IS NOT NULL DROP TABLE dbo.HorariosAgenda;
GO
CREATE TABLE dbo.HorariosAgenda
(
    HorarioId       INT             IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    MedicoId        INT             NOT NULL,
    Fecha           DATE            NOT NULL,
    HoraInicio      TIME(0)         NOT NULL,
    HoraFin         TIME(0)         NOT NULL,
    Estado          NVARCHAR(20)    NOT NULL DEFAULT (N'Disponible'),
    CONSTRAINT FK_HorariosAgenda_Medicos FOREIGN KEY (MedicoId) REFERENCES dbo.Medicos(MedicoId),
    CONSTRAINT CK_HorariosAgenda_Estado CHECK (Estado IN (N'Disponible', N'Reservado', N'Bloqueado')),
    CONSTRAINT UQ_HorariosAgenda_Slot UNIQUE (MedicoId, Fecha, HoraInicio)
);
GO
CREATE INDEX IX_HorariosAgenda_Medico_Fecha ON dbo.HorariosAgenda (MedicoId, Fecha, Estado);
GO

-- ------------------------------------------------------------
-- Reservas
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Reservas', 'U') IS NOT NULL DROP TABLE dbo.Reservas;
GO
CREATE TABLE dbo.Reservas
(
    ReservaId       INT             IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    HorarioId       INT             NOT NULL,
    UsuarioId       INT             NOT NULL,
    FechaReserva    DATETIME        NOT NULL DEFAULT (GETDATE()),
    Estado          NVARCHAR(20)    NOT NULL DEFAULT (N'Confirmada'),
    CONSTRAINT FK_Reservas_HorariosAgenda FOREIGN KEY (HorarioId) REFERENCES dbo.HorariosAgenda(HorarioId),
    CONSTRAINT FK_Reservas_Usuarios FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuarios(UsuarioId),
    CONSTRAINT CK_Reservas_Estado CHECK (Estado IN (N'Confirmada', N'Cancelada'))
);
GO
-- Un horario solo puede tener UNA reserva activa (protege contra doble reserva a nivel de BD)
CREATE UNIQUE INDEX UQ_Reservas_Horario_Activa
    ON dbo.Reservas (HorarioId)
    WHERE Estado = N'Confirmada';
GO

-- ------------------------------------------------------------
-- Bitacora (registro de eventos)
-- ------------------------------------------------------------
IF OBJECT_ID('dbo.Bitacora', 'U') IS NOT NULL DROP TABLE dbo.Bitacora;
GO
CREATE TABLE dbo.Bitacora
(
    LogId           INT             IDENTITY(1,1)   NOT NULL PRIMARY KEY,
    UsuarioId       INT             NULL,
    Accion          NVARCHAR(100)   NOT NULL,
    Detalle         NVARCHAR(500)   NULL,
    Fecha           DATETIME        NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT FK_Bitacora_Usuarios FOREIGN KEY (UsuarioId) REFERENCES dbo.Usuarios(UsuarioId)
);
GO
