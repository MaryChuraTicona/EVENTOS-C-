create database EVENTOSC#;
use EVENTOSC#;

CREATE TABLE Rol (
    IdRol          INT IDENTITY(1,1) PRIMARY KEY,
    Nombre         VARCHAR(50) NOT NULL,       
    Descripcion    VARCHAR(200) NULL,
    Estado         BIT NOT NULL DEFAULT 1     
);
GO

CREATE TABLE TipoProcedencia (
    IdTipoProcedencia INT IDENTITY(1,1) PRIMARY KEY,
    Nombre            VARCHAR(80) NOT NULL,
    Descripcion       VARCHAR(200) NULL,
    Estado            BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE Usuario (
    IdUsuario         INT IDENTITY(1,1) PRIMARY KEY,
    IdRol             INT NOT NULL,
    IdTipoProcedencia INT NULL,
    Nombres           VARCHAR(100) NOT NULL,
    Apellidos         VARCHAR(120) NOT NULL,
    Correo            VARCHAR(150) NOT NULL UNIQUE,
    ContrasenaHash    VARCHAR(200) NOT NULL,
    DNI               VARCHAR(20) NULL,
    CodigoUPT         VARCHAR(20) NULL,
    Telefono          VARCHAR(30) NULL,
    EsVerificado      BIT NOT NULL DEFAULT 0,
    FechaRegistro     DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Estado            BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    CONSTRAINT FK_Usuario_TipoProcedencia FOREIGN KEY (IdTipoProcedencia) REFERENCES TipoProcedencia(IdTipoProcedencia)
);
GO


CREATE TABLE CategoriaEvento (
    IdCategoriaEvento INT IDENTITY(1,1) PRIMARY KEY,
    Nombre            VARCHAR(80) NOT NULL,
    Descripcion       VARCHAR(200) NULL,
    Estado            BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE ModalidadEvento (
    IdModalidadEvento INT IDENTITY(1,1) PRIMARY KEY,
    Nombre            VARCHAR(30) NOT NULL,  -- 'PRESENCIAL', 'VIRTUAL', 'HIBRIDO'
    Descripcion       VARCHAR(150) NULL,
    Estado            BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE MetodoPago (
    IdMetodoPago  INT IDENTITY(1,1) PRIMARY KEY,
    Nombre        VARCHAR(80) NOT NULL,
    Descripcion   VARCHAR(200) NULL,
    EsOnline      BIT NOT NULL DEFAULT 1,
    Estado        BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE PasarelaPago (
    IdPasarelaPago INT IDENTITY(1,1) PRIMARY KEY,
    Nombre         VARCHAR(100) NOT NULL,
    Descripcion    VARCHAR(200) NULL,
    Activa         BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE TipoCertificado (
    IdTipoCertificado  INT IDENTITY(1,1) PRIMARY KEY,
    Nombre             VARCHAR(80) NOT NULL,
    Descripcion        VARCHAR(200) NULL,
    PlantillaURL       VARCHAR(400) NULL,
    MinHorasRequeridas DECIMAL(5,2) NULL,
    Estado             BIT NOT NULL DEFAULT 1
);
GO



CREATE TABLE SolicitudEvento (
    IdSolicitudEvento      INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuarioSolicitante   INT NOT NULL,   
    TituloPropuesto        VARCHAR(200) NOT NULL,
    Objetivo               VARCHAR(MAX) NULL,
    Justificacion          VARCHAR(MAX) NULL,
    PublicoObjetivo        VARCHAR(200) NULL,
    ModalidadPropuesta     VARCHAR(50) NULL,
    LugarPropuesto         VARCHAR(200) NULL,
    FechaInicioPropuesta   DATETIME2 NOT NULL,
    FechaFinPropuesta      DATETIME2 NOT NULL,
    PresupuestoEstimado    DECIMAL(12,2) NULL,
    EstadoSolicitud        VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE',
   
    IdUsuarioAltaDireccion INT NULL,        
    FechaSolicitud         DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaRespuesta         DATETIME2 NULL,
    ComentarioAltaDireccion VARCHAR(MAX) NULL,
    CONSTRAINT FK_SolicitudEvento_Solicitante FOREIGN KEY (IdUsuarioSolicitante) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_SolicitudEvento_AltaDireccion FOREIGN KEY (IdUsuarioAltaDireccion) REFERENCES Usuario(IdUsuario)
);
GO


CREATE TABLE Evento (
    IdEvento          INT IDENTITY(1,1) PRIMARY KEY,
    IdSolicitudEvento INT NULL,
    IdCategoriaEvento INT NOT NULL,
    IdModalidadEvento INT NOT NULL,
    Nombre            VARCHAR(200) NOT NULL,
    Descripcion       VARCHAR(MAX) NULL,
    PublicoObjetivo   VARCHAR(200) NULL,
    Lugar             VARCHAR(200) NULL,
    EnlaceVirtual     VARCHAR(300) NULL,
    FechaInicio       DATETIME2 NOT NULL,
    FechaFin          DATETIME2 NOT NULL,
    AforoMaximo       INT NULL,
    EsDePago          BIT NOT NULL DEFAULT 0,
    PrecioBase        DECIMAL(10,2) NULL,
    CodigoEvento      VARCHAR(30) NOT NULL UNIQUE,
    BannerURL         VARCHAR(400) NULL,
    Estado            VARCHAR(20) NOT NULL DEFAULT 'PROGRAMADO',
    
    FechaRegistro     DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UsuarioRegistroId INT NULL,
    CONSTRAINT FK_Evento_Categoria       FOREIGN KEY (IdCategoriaEvento) REFERENCES CategoriaEvento(IdCategoriaEvento),
    CONSTRAINT FK_Evento_Modalidad       FOREIGN KEY (IdModalidadEvento) REFERENCES ModalidadEvento(IdModalidadEvento),
    CONSTRAINT FK_Evento_UsuarioRegistro FOREIGN KEY (UsuarioRegistroId) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Evento_SolicitudEvento FOREIGN KEY (IdSolicitudEvento) REFERENCES SolicitudEvento(IdSolicitudEvento)
);
GO


CREATE TABLE Ponente (
    IdPonente    INT IDENTITY(1,1) PRIMARY KEY,
    Nombres      VARCHAR(100) NOT NULL,
    Apellidos    VARCHAR(120) NOT NULL,
    Biografia    VARCHAR(MAX) NULL,
    Especialidad VARCHAR(200) NULL,
    FotoURL      VARCHAR(400) NULL,
    Correo       VARCHAR(150) NULL,
    LinkedIn     VARCHAR(250) NULL,
    Estado       BIT NOT NULL DEFAULT 1
);
GO


CREATE TABLE EventoPonente (
    IdEventoPonente INT IDENTITY(1,1) PRIMARY KEY,
    IdEvento        INT NOT NULL,
    IdPonente       INT NOT NULL,
    Orden           INT NULL,
    Tema            VARCHAR(300) NULL,
    HoraInicio      DATETIME2 NULL,
    HoraFin         DATETIME2 NULL,
    CreadoPor       INT NULL,             
    FechaCreacion   DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT FK_EventoPonente_Evento    FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento),
    CONSTRAINT FK_EventoPonente_Ponente   FOREIGN KEY (IdPonente) REFERENCES Ponente(IdPonente),
    CONSTRAINT FK_EventoPonente_CreadoPor FOREIGN KEY (CreadoPor) REFERENCES Usuario(IdUsuario)
);
GO



CREATE TABLE EventoOrganizador (
    IdEventoOrganizador     INT IDENTITY(1,1) PRIMARY KEY,
    IdEvento                INT NOT NULL,
    IdUsuarioOrganizador    INT NOT NULL,
    RolInterno              VARCHAR(80) NULL,   
    EsResponsablePrincipal  BIT NOT NULL DEFAULT 0,
    PuedeCrearPonentes      BIT NOT NULL DEFAULT 1,
    PuedeGestionarAsistencia BIT NOT NULL DEFAULT 1,
    PuedeVerReportes        BIT NOT NULL DEFAULT 1,
    PuedeEditarEvento       BIT NOT NULL DEFAULT 0,
    Estado                  BIT NOT NULL DEFAULT 1,
    FechaAsignacion         DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    AsignadoPor             INT NULL,      
    CONSTRAINT UQ_EventoOrganizador UNIQUE (IdEvento, IdUsuarioOrganizador),
    CONSTRAINT FK_EventoOrganizador_Evento      FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento),
    CONSTRAINT FK_EventoOrganizador_Usuario     FOREIGN KEY (IdUsuarioOrganizador) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_EventoOrganizador_AsignadoPor FOREIGN KEY (AsignadoPor) REFERENCES Usuario(IdUsuario)
);
GO


CREATE TABLE Inscripcion (
    IdInscripcion    INT IDENTITY(1,1) PRIMARY KEY,
    IdEvento         INT NOT NULL,
    IdUsuario        INT NOT NULL,         
    FechaInscripcion DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Estado           VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE',
 
    Observacion      VARCHAR(300) NULL,
    IdOrdenPago      INT NULL,             
    RegistradoPor    INT NULL,              
    CONSTRAINT UQ_Inscripcion UNIQUE (IdEvento, IdUsuario),
    CONSTRAINT FK_Inscripcion_Evento        FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento),
    CONSTRAINT FK_Inscripcion_Usuario       FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Inscripcion_RegistradoPor FOREIGN KEY (RegistradoPor) REFERENCES Usuario(IdUsuario)
);
GO


CREATE TABLE Asistencia (
    IdAsistencia  INT IDENTITY(1,1) PRIMARY KEY,
    IdInscripcion INT NOT NULL,
    FechaHora     DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Tipo          VARCHAR(20) NOT NULL DEFAULT 'ENTRADA', 

    RegistradoPor INT NULL, 
    Observacion   VARCHAR(200) NULL,
    CONSTRAINT FK_Asistencia_Inscripcion   FOREIGN KEY (IdInscripcion) REFERENCES Inscripcion(IdInscripcion),
    CONSTRAINT FK_Asistencia_RegistradoPor FOREIGN KEY (RegistradoPor) REFERENCES Usuario(IdUsuario)
);
GO



CREATE TABLE Certificado (
    IdCertificado      INT IDENTITY(1,1) PRIMARY KEY,
    IdInscripcion      INT NOT NULL,
    IdTipoCertificado  INT NOT NULL,
    CodigoCertificado  VARCHAR(50) NOT NULL UNIQUE,
    URLCertificado     VARCHAR(400) NOT NULL,
    FechaEmision       DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    EmitidoPor         INT NULL,           
    Observacion        VARCHAR(300) NULL,
    CONSTRAINT FK_Certificado_Inscripcion 
        FOREIGN KEY (IdInscripcion) REFERENCES Inscripcion(IdInscripcion),
    CONSTRAINT FK_Certificado_Tipo 
        FOREIGN KEY (IdTipoCertificado) REFERENCES TipoCertificado(IdTipoCertificado),
    CONSTRAINT FK_Certificado_EmitidoPor 
        FOREIGN KEY (EmitidoPor) REFERENCES Usuario(IdUsuario)
);
GO



CREATE TABLE Notificacion (
    IdNotificacion INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario      INT NOT NULL,
    Titulo         VARCHAR(150) NOT NULL,
    Mensaje        VARCHAR(MAX) NOT NULL,
    FechaEnvio     DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    Leido          BIT NOT NULL DEFAULT 0,
    Tipo           VARCHAR(50) NULL, 
    IdReferencia   INT NULL,
    CONSTRAINT FK_Notificacion_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
GO



CREATE TABLE OrdenPago (
    IdOrdenPago       INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario         INT NOT NULL,       
    IdEvento          INT NOT NULL,
    MontoTotal        DECIMAL(10,2) NOT NULL,
    Moneda            VARCHAR(10) NOT NULL DEFAULT 'PEN',
    FechaCreacion     DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    FechaConfirmacion DATETIME2 NULL,
    Estado            VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE',

    IdMetodoPago      INT NOT NULL,
    Observacion       VARCHAR(300) NULL,
    CONSTRAINT FK_OrdenPago_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_OrdenPago_Evento  FOREIGN KEY (IdEvento)  REFERENCES Evento(IdEvento),
    CONSTRAINT FK_OrdenPago_Metodo  FOREIGN KEY (IdMetodoPago) REFERENCES MetodoPago(IdMetodoPago)
);
GO

CREATE TABLE DetalleOrdenPago (
    IdDetalleOrdenPago INT IDENTITY(1,1) PRIMARY KEY,
    IdOrdenPago        INT NOT NULL,
    Concepto           VARCHAR(200) NOT NULL,
    Cantidad           INT NOT NULL DEFAULT 1,
    PrecioUnitario     DECIMAL(10,2) NOT NULL,
    Subtotal           AS (Cantidad * PrecioUnitario) PERSISTED,
    CONSTRAINT FK_DetalleOrdenPago_Orden FOREIGN KEY (IdOrdenPago) REFERENCES OrdenPago(IdOrdenPago)
);
GO

CREATE TABLE TransaccionPasarela (
    IdTransaccionPasarela INT IDENTITY(1,1) PRIMARY KEY,
    IdOrdenPago           INT NOT NULL,
    IdPasarelaPago        INT NOT NULL,
    IdTransaccionExterna  VARCHAR(150) NOT NULL,
    EstadoPasarela        VARCHAR(30) NOT NULL,   
    MontoAutorizado       DECIMAL(10,2) NULL,
    CodigoAutorizacion    VARCHAR(100) NULL,
    FechaTransaccion      DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    RawResponseJSON       NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Transaccion_OrdenPago FOREIGN KEY (IdOrdenPago)     REFERENCES OrdenPago(IdOrdenPago),
    CONSTRAINT FK_Transaccion_Pasarela FOREIGN KEY (IdPasarelaPago)   REFERENCES PasarelaPago(IdPasarelaPago)
);
GO



ALTER TABLE Inscripcion
ADD CONSTRAINT FK_Inscripcion_OrdenPago 
    FOREIGN KEY (IdOrdenPago) REFERENCES OrdenPago(IdOrdenPago);
GO



CREATE TABLE CupoEventoRol (
    IdCupoEventoRol INT IDENTITY(1,1) PRIMARY KEY,
    IdEvento        INT NOT NULL,
    IdRol           INT NOT NULL,
    CupoMaximo      INT NOT NULL,
    CONSTRAINT FK_Cupo_Evento FOREIGN KEY (IdEvento) REFERENCES Evento(IdEvento),
    CONSTRAINT FK_Cupo_Rol    FOREIGN KEY (IdRol)    REFERENCES Rol(IdRol)
);
GO



CREATE TABLE Auditoria (
    IdAuditoria       INT IDENTITY(1,1) PRIMARY KEY,
    TablaAfectada     VARCHAR(100) NOT NULL,   
    IdRegistro        VARCHAR(50)  NULL,        
    Accion            VARCHAR(20)  NOT NULL,   
    UsuarioAccionId   INT         NULL,        
    DetalleAccion     VARCHAR(300) NULL,        
    ValoresAnteriores NVARCHAR(MAX) NULL,       
    ValoresNuevos     NVARCHAR(MAX) NULL,       
    FechaAccion       DATETIME2   NOT NULL DEFAULT SYSDATETIME(),
    IPOrigen          VARCHAR(50) NULL,         
    Observacion       VARCHAR(300) NULL
);
GO


CREATE PROCEDURE usp_S_Usuario_Login
    @Correo      VARCHAR(150),
    @Contrasena  VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.IdUsuario,
        u.Nombres,
        u.Apellidos,
        u.Correo,
        u.IdRol,
        r.Nombre AS NombreRol
    FROM Usuario u
    INNER JOIN Rol r ON r.IdRol = u.IdRol
    WHERE 
        u.Correo = @Correo
        AND u.ContrasenaHash = @Contrasena  
        AND u.Estado = 1;                   
END;
GO 


INSERT INTO Rol (Nombre, Descripcion, Estado)
VALUES ('ADMIN_EVENTOS', 'Administrador general de eventos EPIS', 1);
GO


INSERT INTO Usuario
    (IdRol, IdTipoProcedencia, Nombres, Apellidos, Correo, ContrasenaHash, DNI, CodigoUPT, Telefono, EsVerificado, Estado)
VALUES
    (1, NULL, 'Mary Luz', 'Chura Ticona', 'admin@epis.upt.pe', '123456', '12345678', '2019065163', '999999999', 1, 1);
GO


IF OBJECT_ID('dbo.usp_S_Usuario_Login', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_S_Usuario_Login;
GO

CREATE PROCEDURE dbo.usp_S_Usuario_Login
    @Correo      VARCHAR(150),
    @Contrasena  VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.IdUsuario,
        u.Nombres,
        u.Apellidos,
        u.Correo,
        u.IdRol,
        r.Nombre AS NombreRol
    FROM Usuario u
    INNER JOIN Rol r ON r.IdRol = u.IdRol
    WHERE 
        u.Correo = @Correo
        AND u.ContrasenaHash = @Contrasena
        AND u.Estado = 1;
END;
GO

EXEC dbo.usp_S_Usuario_Login
    @Correo = 'admin@epis.upt.pe',
    @Contrasena = '123456';



 
SELECT DB_NAME() AS BaseActual;
GO

SELECT name, SCHEMA_NAME(schema_id) AS Esquema
FROM sys.procedures
WHERE name = 'usp_S_Usuario_Login';
GO

--/USUARIOS---

CREATE PROCEDURE dbo.usp_S_Usuario_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.IdUsuario,
        u.IdRol,
        r.Nombre AS NombreRol,
        u.IdTipoProcedencia,
        tp.Nombre AS NombreTipoProcedencia,
        u.Nombres,
        u.Apellidos,
        u.Correo,
        u.ContrasenaHash,
        u.DNI,
        u.CodigoUPT,
        u.Telefono,
        u.EsVerificado,
        u.Estado,
        u.FechaRegistro
    FROM Usuario u
    INNER JOIN Rol r ON r.IdRol = u.IdRol
    LEFT JOIN TipoProcedencia tp ON tp.IdTipoProcedencia = u.IdTipoProcedencia;
END;
GO

-- INSERTAR USUARIO
IF OBJECT_ID('dbo.usp_I_Usuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_I_Usuario;
GO

CREATE PROCEDURE dbo.usp_I_Usuario
    @IdRol             INT,
    @IdTipoProcedencia INT = NULL,
    @Nombres           VARCHAR(100),
    @Apellidos         VARCHAR(120),
    @Correo            VARCHAR(150),
    @Contrasena        VARCHAR(200),
    @DNI               VARCHAR(20) = NULL,
    @CodigoUPT         VARCHAR(20) = NULL,
    @Telefono          VARCHAR(30) = NULL,
    @EsVerificado      BIT = 0,
    @Estado            BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuario
    (
        IdRol,
        IdTipoProcedencia,
        Nombres,
        Apellidos,
        Correo,
        ContrasenaHash,
        DNI,
        CodigoUPT,
        Telefono,
        EsVerificado,
        Estado
    )
    VALUES
    (
        @IdRol,
        @IdTipoProcedencia,
        @Nombres,
        @Apellidos,
        @Correo,
        @Contrasena,
        @DNI,
        @CodigoUPT,
        @Telefono,
        @EsVerificado,
        @Estado
    );

    SELECT SCOPE_IDENTITY() AS IdGenerado;
END;
GO


-- ACTUALIZAR USUARIO
IF OBJECT_ID('dbo.usp_U_Usuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_U_Usuario;
GO

CREATE PROCEDURE dbo.usp_U_Usuario
    @IdUsuario         INT,
    @IdRol             INT,
    @IdTipoProcedencia INT = NULL,
    @Nombres           VARCHAR(100),
    @Apellidos         VARCHAR(120),
    @Correo            VARCHAR(150),
    @Contrasena        VARCHAR(200),
    @DNI               VARCHAR(20) = NULL,
    @CodigoUPT         VARCHAR(20) = NULL,
    @Telefono          VARCHAR(30) = NULL,
    @EsVerificado      BIT = 0,
    @Estado            BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET
        IdRol             = @IdRol,
        IdTipoProcedencia = @IdTipoProcedencia,
        Nombres           = @Nombres,
        Apellidos         = @Apellidos,
        Correo            = @Correo,
        ContrasenaHash    = @Contrasena,
        DNI               = @DNI,
        CodigoUPT         = @CodigoUPT,
        Telefono          = @Telefono,
        EsVerificado      = @EsVerificado,
        Estado            = @Estado
    WHERE IdUsuario = @IdUsuario;
END;
GO


-- "ELIMINAR" (BAJA LÓGICA)
IF OBJECT_ID('dbo.usp_D_Usuario', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_D_Usuario;
GO

CREATE PROCEDURE dbo.usp_D_Usuario
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Usuario
    SET Estado = 0
    WHERE IdUsuario = @IdUsuario;
END;
GO


-- LISTAR ROLES ACTIVOS (para el combo)
IF OBJECT_ID('dbo.usp_S_Rol_Activos', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_S_Rol_Activos;
GO

CREATE PROCEDURE dbo.usp_S_Rol_Activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdRol, Nombre
    FROM Rol
    WHERE Estado = 1;
END;
GO


-- LISTAR TIPO PROCEDENCIA ACTIVOS (para el combo)
IF OBJECT_ID('dbo.usp_S_TipoProcedencia_Activos', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_S_TipoProcedencia_Activos;
GO

CREATE PROCEDURE dbo.usp_S_TipoProcedencia_Activos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdTipoProcedencia, Nombre
    FROM TipoProcedencia
    WHERE Estado = 1;
END;
GO

--// ponentes--

ALTER TABLE Ponente
ADD Telefono VARCHAR(30) NULL;
GO

IF OBJECT_ID('dbo.usp_I_Ponente', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_I_Ponente;
GO

CREATE PROCEDURE dbo.usp_I_Ponente
    @Nombres      VARCHAR(100),
    @Apellidos    VARCHAR(120),
    @Biografia    VARCHAR(MAX)     = NULL,
    @Especialidad VARCHAR(200)     = NULL,
    @Correo       VARCHAR(150)     = NULL,
    @Telefono     VARCHAR(30)      = NULL,
    @FotoURL      VARCHAR(400)     = NULL,
    @IdGenerado   INT OUTPUT,
    @Mensaje      VARCHAR(300) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Ponente
        (Nombres, Apellidos, Biografia, Especialidad, Correo, Telefono, FotoURL, Estado)
        VALUES
        (@Nombres, @Apellidos, @Biografia, @Especialidad, @Correo, @Telefono, @FotoURL, 1);

        SET @IdGenerado = SCOPE_IDENTITY();
        SET @Mensaje = 'Ponente registrado correctamente.';
    END TRY
    BEGIN CATCH
        SET @IdGenerado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO


IF OBJECT_ID('dbo.usp_S_Ponente_Listar', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_S_Ponente_Listar;
GO

CREATE PROCEDURE dbo.usp_S_Ponente_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdPonente,
        Nombres,
        Apellidos,
        Biografia,
        Especialidad,
        Correo,
        Telefono,
        FotoURL,
        Estado
    FROM Ponente
    WHERE Estado = 1;
END;
GO

--//actualizar ponente--
IF OBJECT_ID('dbo.usp_U_Ponente', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_U_Ponente;
GO

CREATE PROCEDURE dbo.usp_U_Ponente
    @IdPonente   INT,
    @Nombres      VARCHAR(100),
    @Apellidos    VARCHAR(120),
    @Biografia    VARCHAR(MAX)     = NULL,
    @Especialidad VARCHAR(200)     = NULL,
    @Correo       VARCHAR(150)     = NULL,
    @Telefono     VARCHAR(30)      = NULL,
    @FotoURL      VARCHAR(400)     = NULL,
    @Mensaje      VARCHAR(300) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Ponente
        SET
            Nombres      = @Nombres,
            Apellidos    = @Apellidos,
            Biografia    = @Biografia,
            Especialidad = @Especialidad,
            Correo       = @Correo,
            Telefono     = @Telefono,
            FotoURL      = @FotoURL
        WHERE IdPonente = @IdPonente;

        SET @Mensaje = 'Ponente actualizado correctamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

--// eliminar

IF OBJECT_ID('dbo.usp_D_Ponente', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_D_Ponente;
GO

CREATE PROCEDURE dbo.usp_D_Ponente
    @IdPonente INT,
    @Mensaje   VARCHAR(300) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Ponente
        SET Estado = 0
        WHERE IdPonente = @IdPonente;

        SET @Mensaje = 'Ponente dado de baja correctamente.';
    END TRY
    BEGIN CATCH
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END;
GO

select * from Ponente; 

INSERT INTO Rol (Nombre, Descripcion, Estado)
VALUES 
('ADMIN', 'Administrador general del sistema de eventos', 1),
('ORGANIZADOR', 'Gestor y organizador de eventos', 1),
('PONENTE', 'Expositor invitado o conferencista', 1),
('ESTUDIANTE', 'Usuario que participa en los eventos EPIS', 1);
GO
