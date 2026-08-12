# VB-Dental — Sistema de Reservas de Horas Médicas

Aplicación de escritorio en **VB.NET / .NET Framework 4.8** (WinForms) para gestionar
reservas de horas médicas, desarrollada como prueba técnica.

## 1. Descripción general

La solución está organizada en tres capas:

| Proyecto | Responsabilidad |
|---|---|
| `VBDental.Data` | Acceso a datos (ADO.NET puro, `SqlClient`), entidades y repositorios. |
| `VBDental.Business` | Lógica de negocio: autenticación (hash PBKDF2), reglas de reserva (transaccional, evita doble reserva), bitácora. |
| `VBDental.UI` | Formularios Windows Forms: Login, Menú principal, Agenda/Reserva, Administración, Bitácora, Reportes. |
| `VBDental.Tests` | Pruebas MSTest: unitarias (`PasswordHasher`) y de integración contra LocalDB (`AuthService`, `ReservaService`, incluyendo la condición de carrera en reservas). |

### Funcionalidades implementadas

- **Autenticación** con usuario/contraseña (hash PBKDF2-HMACSHA256, salt por usuario) y dos roles: `Administrador` y `Usuario`.
- **Agenda médica**: listado de médicos, consulta de horarios por médico y fecha.
- **Reserva de horas**: selección de médico → fecha → horario → confirmación. La operación es **transaccional** (`SERIALIZABLE` + UPDATE condicional) y existe además un **índice único filtrado** en la tabla `Reservas`, por lo que un horario no puede quedar doblemente reservado aunque dos usuarios reserven al mismo tiempo.
- **Administración**: el perfil Administrador visualiza el listado completo de reservas (médico, fecha/hora, usuario, estado), con exportación a CSV.
- **Bitácora de eventos**: se registran login (éxito/fallo), reservas (confirmadas/fallidas) y errores no controlados en la tabla `Bitacora`. El Administrador puede consultarla desde una pantalla dedicada (`Bitácora de Eventos`), con filtro por rango de fechas y exportación a CSV.
- **Validaciones de datos**: feedback visual con `ErrorProvider` en el login (usuario/contraseña obligatorios); el selector de fecha de la agenda no permite elegir fechas pasadas.
- **Manejo de errores no controlados**: `Application.ThreadException` y `AppDomain.UnhandledException` capturan cualquier excepción imprevista (ej. caída de la BD), la registran en la bitácora y muestran un mensaje amigable en vez de cerrar la aplicación abruptamente.
- **Mejoras de interfaz**: las filas de la agenda se colorean según su estado (verde=Disponible, rosado=Reservado, gris=Bloqueado) para lectura rápida.
- **Reportes**: pantalla `Reporte de Reservas` (Admin) con filtro por médico (o todos) y rango de fechas, total de resultados y exportación a CSV.
- **Pruebas automatizadas**: proyecto `VBDental.Tests` (MSTest) con 13 pruebas — unitarias sobre el hashing de contraseñas y de integración sobre login y reglas de reserva (incluida una prueba de concurrencia real con dos hilos compitiendo por el mismo horario).

## 2. Requisitos del ambiente

- Windows con **.NET Framework 4.8** (Developer Pack si se va a compilar).
- **Visual Studio** 2019/2022 o superior con el workload ".NET desktop development".
- **SQL Server** (se usó **SQL Server LocalDB**, instancia `MSSQLLocalDB`).

## 3. Pasos para ejecutar la aplicación

### a) Base de datos

1. Ejecutar `database/01_create_tables.sql` contra el motor SQL (crea la base `VBDentalDb` y sus tablas).
2. Ejecutar `database/02_seed_data.sql` (carga usuarios demo, médicos y 10 días de agenda).

Con `sqlcmd`, por ejemplo:

```
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\01_create_tables.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database\02_seed_data.sql
```

La cadena de conexión por defecto (`src/VBDental.UI/App.config`) apunta a
`(localdb)\MSSQLLocalDB`, base `VBDentalDb`, con autenticación integrada. Ajustarla
si se usa otra instancia/motor.

### b) Compilar y ejecutar

- Abrir `VBDental.sln` en Visual Studio y ejecutar (F5), **o**
- Desde consola: `dotnet build VBDental.sln` y luego ejecutar
  `src\VBDental.UI\bin\Debug\net48\VBDental.UI.exe`.

### d) Ejecutar las pruebas automatizadas

```
dotnet test src\VBDental.Tests\VBDental.Tests.vbproj
```

Requiere la misma base `VBDentalDb` operativa (los tests de `AuthServiceTests` y
`ReservaServiceTests` son de integración, no usan mocks).

### c) Usuarios de prueba

| Usuario | Contraseña | Rol |
|---|---|---|
| `admin` | `admin123` | Administrador |
| `usuario1` | `user123` | Usuario |

## 4. Estructura de base de datos

- **Usuarios** — login, hash/salt de contraseña, rol (`Administrador`/`Usuario`).
- **Medicos** — nombre, especialidad.
- **HorariosAgenda** — slots de agenda por médico (fecha, hora inicio/fin, estado: `Disponible`/`Reservado`/`Bloqueado`). Único por (médico, fecha, hora).
- **Reservas** — vincula un `HorarioAgenda` con el `Usuario` que reservó. Índice único filtrado por `HorarioId` mientras `Estado = 'Confirmada'` (evita doble reserva a nivel de BD).
- **Bitacora** — registro de eventos (login, reservas) con usuario, acción, detalle y fecha.

## 5. Decisiones técnicas relevantes

- Se usó **ADO.NET puro** (sin ORM) para mantener la solución simple y explícita, acorde al alcance de la prueba.
- La prevención de doble reserva se resuelve en dos niveles: transacción `SERIALIZABLE` con `UPDATE ... WHERE Estado = 'Disponible'` (falla silenciosamente si ya fue tomado) y un índice único filtrado como respaldo a nivel de esquema.
- Las contraseñas se almacenan con **PBKDF2-HMACSHA256** (10.000 iteraciones, salt aleatorio de 16 bytes por usuario), no en texto plano ni con hash simple.
- Proyectos en formato **SDK-style** (`Microsoft.NET.Sdk`, `TargetFramework=net48`) por simplicidad de mantenimiento, totalmente compatibles con Visual Studio y `.NET Framework 4.8`.
- Los valores de rol/estado (`Administrador`/`Usuario`, `Disponible`/`Reservado`/`Bloqueado`, `Confirmada`/`Cancelada`) se modelan como **Enums** en vez de strings sueltos, evitando errores de tipeo; se serializan/parsean en el límite del repositorio, sin cambiar el esquema de BD (sigue siendo `NVARCHAR` con `CHECK`).
