-- ============================================================
-- VB-Dental - Datos iniciales (usuarios, médicos, agenda demo)
-- ============================================================
USE VBDentalDb;
GO

-- ------------------------------------------------------------
-- Usuarios (contraseñas: admin/admin123, usuario1/user123)
-- Hash = PBKDF2-HMACSHA256, 10000 iteraciones, salt 16 bytes, hash 32 bytes (Base64)
-- ------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE NombreUsuario = N'admin')
BEGIN
    INSERT INTO dbo.Usuarios (NombreUsuario, PasswordHash, PasswordSalt, NombreCompleto, Rol)
    VALUES (N'admin', N'4LZGqp0N+7iZFUwOPOGYu3Gm8CPcv8w8EiXsyMnG7fw=', N'2MCSN55YAMAhd0jhDBhxrg==', N'Administrador del Sistema', N'Administrador');
END

IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE NombreUsuario = N'usuario1')
BEGIN
    INSERT INTO dbo.Usuarios (NombreUsuario, PasswordHash, PasswordSalt, NombreCompleto, Rol)
    VALUES (N'usuario1', N'GkHdF8JKxkmHlb/M/+VG9NQih3hBNSmQUqPcnOoWVTU=', N'B1T5btdLNNzydz4cnPHmQQ==', N'Paciente Demo', N'Usuario');
END
GO

-- ------------------------------------------------------------
-- Medicos
-- ------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.Medicos)
BEGIN
    -- Los acentos se arman con NCHAR() (en vez de escribirlos literales en el script)
    -- para que la carga sea inmune a la codificación con la que la herramienta cliente
    -- (sqlcmd, SSMS, etc.) interprete este archivo .sql en cualquier equipo.
    INSERT INTO dbo.Medicos (Nombre, Especialidad) VALUES
        (N'Dra. Camila Rojas', N'Odontolog' + NCHAR(237) + N'a General'),   -- Odontología
        (N'Dr. Felipe Soto', N'Ortodoncia'),
        (N'Dra. Valentina Mu' + NCHAR(241) + N'oz', N'Endodoncia');        -- Muñoz
END
GO

-- ------------------------------------------------------------
-- Horarios de agenda: próximos 7 días (lun-vie), 09:00-17:00 cada hora, por médico
-- ------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.HorariosAgenda)
BEGIN
    DECLARE @Dias TABLE (Fecha DATE);
    DECLARE @i INT = 0;
    WHILE @i < 10
    BEGIN
        DECLARE @Fecha DATE = DATEADD(DAY, @i, CAST(GETDATE() AS DATE));
        IF DATEPART(WEEKDAY, @Fecha) NOT IN (1, 7) -- excluye domingo/sábado (config regional por defecto: 1=domingo,7=sábado)
            INSERT INTO @Dias (Fecha) VALUES (@Fecha);
        SET @i += 1;
    END

    DECLARE @Horas TABLE (HoraInicio TIME);
    INSERT INTO @Horas (HoraInicio) VALUES
        ('09:00'), ('10:00'), ('11:00'), ('12:00'),
        ('14:00'), ('15:00'), ('16:00'), ('17:00');

    INSERT INTO dbo.HorariosAgenda (MedicoId, Fecha, HoraInicio, HoraFin, Estado)
    SELECT m.MedicoId, d.Fecha, h.HoraInicio, DATEADD(HOUR, 1, CAST(h.HoraInicio AS DATETIME)), N'Disponible'
    FROM dbo.Medicos m
    CROSS JOIN @Dias d
    CROSS JOIN @Horas h;
END
GO
