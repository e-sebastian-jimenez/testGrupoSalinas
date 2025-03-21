CREATE DATABASE GestionEscolar;
GO
USE GestionEscolar;
GO

CREATE TABLE Profesores (
    ProfesorID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
);

CREATE TABLE Cursos (
    CursoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Codigo NVARCHAR(50) UNIQUE NOT NULL,
    ProfesorID INT NULL, 
    CONSTRAINT FK_Cursos_Profesores FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID) ON DELETE NO ACTION
);

CREATE TABLE Alumnos (
    AlumnoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
);

CREATE TABLE Alumnos_Inscripcion_Cursos (
    InscripcionID INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoID INT NOT NULL,
    CursoID INT NOT NULL,
    FechaInscripcion DATE NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Inscripciones_Alumnos FOREIGN KEY (AlumnoID) REFERENCES Alumnos(AlumnoID) ON DELETE CASCADE,
    CONSTRAINT FK_Inscripciones_Cursos FOREIGN KEY (CursoID) REFERENCES Cursos(CursoID) ON DELETE CASCADE,
    CONSTRAINT UQ_Alumno_Curso UNIQUE (AlumnoID, CursoID)
);

GO

CREATE OR ALTER PROCEDURE sp_Cursos_CRUD
    @Operacion CHAR(1),
    @CursoID INT = NULL,
    @Nombre NVARCHAR(100) = NULL,
    @Descripcion NVARCHAR(255) = NULL,
    @Codigo NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT OFF;

    IF @Operacion = 'C' 
	BEGIN
		INSERT INTO Cursos (Nombre, Descripcion, Codigo)
		VALUES (@Nombre, @Descripcion, @Codigo)
	END

    ELSE IF @Operacion = 'R' 
        SELECT * FROM Cursos WHERE CursoID = @CursoID OR @CursoID IS NULL;

	ELSE IF @Operacion = 'U'
	BEGIN
		UPDATE Cursos
		SET Nombre = @Nombre, Descripcion = @Descripcion, Codigo = @Codigo
		WHERE CursoID = @CursoID;
	END

	ELSE IF @Operacion = 'D'
	BEGIN
		DELETE FROM Cursos WHERE CursoID = @CursoID;
	END
END;

GO

CREATE OR ALTER PROCEDURE sp_Alumnos_CRUD
    @Operacion CHAR(1),
    @AlumnoID INT = NULL,
    @Nombre NVARCHAR(100) = NULL,
    @Apellido NVARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL
AS
BEGIN
    SET NOCOUNT OFF;

    IF @Operacion = 'C' 
	BEGIN
        INSERT INTO Alumnos (Nombre, Apellido, FechaNacimiento) 
        VALUES (@Nombre, @Apellido, @FechaNacimiento);
	END

    ELSE IF @Operacion = 'R' 
        SELECT * FROM Alumnos WHERE AlumnoID = @AlumnoID OR @AlumnoID IS NULL;

    ELSE IF @Operacion = 'U' 
	BEGIN
        UPDATE Alumnos 
        SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento
        WHERE AlumnoID = @AlumnoID;
	END

    ELSE IF @Operacion = 'D' 
		BEGIN
        DELETE FROM Alumnos WHERE AlumnoID = @AlumnoID;
	END
END;

GO

CREATE OR ALTER PROCEDURE sp_Profesores_CRUD
    @Operacion CHAR(1),
    @ProfesorID INT = NULL,
    @Nombre NVARCHAR(100) = NULL,
    @Apellido NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT OFF;

	    IF @Operacion = 'C' 
	BEGIN
        INSERT INTO Profesores (Nombre, Apellido) 
        VALUES (@Nombre, @Apellido);
	END

    ELSE IF @Operacion = 'R' 
        SELECT * FROM Profesores WHERE ProfesorID = @ProfesorID OR @ProfesorID IS NULL;

    ELSE IF @Operacion = 'U' 
		BEGIN
        UPDATE Profesores 
        SET Nombre = @Nombre, Apellido = @Apellido
        WHERE ProfesorID = @ProfesorID;
	END

	ELSE IF @Operacion = 'D' 
		BEGIN
        DELETE FROM Profesores WHERE ProfesorID = @ProfesorID;
	END
END;

GO

CREATE PROCEDURE sp_AsignarProfesor
    @CursoID INT,
    @ProfesorID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Cursos WHERE CursoID = @CursoID)
    BEGIN
        RAISERROR('El curso no existe.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
    BEGIN
        RAISERROR('El profesor no existe.', 16, 1);
        RETURN;
    END

    UPDATE Cursos
    SET ProfesorID = @ProfesorID
    WHERE CursoID = @CursoID;
    
    PRINT 'Profesor asignado correctamente al curso.';
END;


GO

CREATE PROCEDURE sp_InscribirAlumno
    @AlumnoID INT,
    @CursoID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
    BEGIN
        RAISERROR('El alumno no existe.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Cursos WHERE CursoID = @CursoID)
    BEGIN
        RAISERROR('El curso no existe.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Alumnos_Inscripcion_Cursos WHERE AlumnoID = @AlumnoID AND CursoID = @CursoID)
    BEGIN
        RAISERROR('El alumno ya está inscrito en este curso.', 16, 1);
        RETURN;
    END

    INSERT INTO Alumnos_Inscripcion_Cursos (AlumnoID, CursoID, FechaInscripcion)
    VALUES (@AlumnoID, @CursoID, GETDATE());

    PRINT 'Alumno inscrito correctamente en el curso.';
END;


GO

CREATE PROCEDURE sp_ConsultarAlumnosPorCurso
    @CursoID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Cursos WHERE CursoID = @CursoID)
    BEGIN
        RAISERROR('El curso no existe.', 16, 1);
        RETURN;
    END

    SELECT 
        A.AlumnoID,
        A.Nombre AS NombreAlumno,
        A.Apellido AS ApellidoAlumno,
        P.ProfesorID,
        P.Nombre AS NombreProfesor,
        P.Apellido AS ApellidoProfesor
    FROM Alumnos_Inscripcion_Cursos I
    INNER JOIN Alumnos A ON I.AlumnoID = A.AlumnoID
    INNER JOIN Cursos C ON I.CursoID = C.CursoID
    LEFT JOIN Profesores P ON C.ProfesorID = P.ProfesorID
    WHERE C.CursoID = @CursoID;
END;

GO

CREATE PROCEDURE sp_ConsultarCursosPorProfesor
    @ProfesorID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
    BEGIN
        RAISERROR('El profesor no existe.', 16, 1);
        RETURN;
    END

    SELECT 
        C.CursoID,
        C.Nombre AS NombreCurso,
        C.Codigo AS CodigoCurso,
        A.AlumnoID,
        A.Nombre AS NombreAlumno,
        A.Apellido AS ApellidoAlumno
    FROM Cursos C
    LEFT JOIN Alumnos_Inscripcion_Cursos I ON C.CursoID = I.CursoID
    LEFT JOIN Alumnos A ON I.AlumnoID = A.AlumnoID
    WHERE C.ProfesorID = @ProfesorID
    ORDER BY C.CursoID, A.AlumnoID;
END;

GO

