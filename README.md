la creacion de la base de datos es con este script

-- Script de creación de tablas basado en las entidades del proyecto
-- Ejecutar en SQL Server (asegúrate de usar la base de datos correcta)
--CREATE DATABASE StudentDb1;
use  StudentDb1
GO
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

-- Tabla Professors
IF OBJECT_ID('dbo.Professors', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Professors (
		Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Dni INT NOT NULL,
		Name NVARCHAR(200) NOT NULL
	);
END

-- Tabla Students
IF OBJECT_ID('dbo.Students', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Students (
		Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Dni INT NOT NULL,
		Name NVARCHAR(200) NOT NULL
	);
END

-- Tabla Subjects
IF OBJECT_ID('dbo.Subjects', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Subjects (
		Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		Name NVARCHAR(200) NOT NULL,
		Credits INT NOT NULL DEFAULT(3)
	);
END

-- Tabla StudentSubjects (relación many-to-many Student <-> Subject)
IF OBJECT_ID('dbo.StudentSubjects', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.StudentSubjects (
		StudentId INT NOT NULL,
		SubjectId INT NOT NULL,
		CONSTRAINT PK_StudentSubjects PRIMARY KEY (StudentId, SubjectId)
	);

	ALTER TABLE dbo.StudentSubjects
		ADD CONSTRAINT FK_StudentSubjects_Students FOREIGN KEY (StudentId)
		REFERENCES dbo.Students (Id)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION;

	ALTER TABLE dbo.StudentSubjects
		ADD CONSTRAINT FK_StudentSubjects_Subjects FOREIGN KEY (SubjectId)
		REFERENCES dbo.Subjects (Id)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION;
END

-- Tabla ProfessorSubjects (relación many-to-many Professor <-> Subject)
IF OBJECT_ID('dbo.ProfessorSubjects', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.ProfessorSubjects (
		ProfessorId INT NOT NULL,
		SubjectId INT NOT NULL,
		CONSTRAINT PK_ProfessorSubjects PRIMARY KEY (ProfessorId, SubjectId)
	);

	ALTER TABLE dbo.ProfessorSubjects
		ADD CONSTRAINT FK_ProfessorSubjects_Professors FOREIGN KEY (ProfessorId)
		REFERENCES dbo.Professors (Id)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION;

	ALTER TABLE dbo.ProfessorSubjects
		ADD CONSTRAINT FK_ProfessorSubjects_Subjects FOREIGN KEY (SubjectId)
		REFERENCES dbo.Subjects (Id)
		ON UPDATE NO ACTION
		ON DELETE NO ACTION;
END

COMMIT TRANSACTION;
GO

-- Índices recomendados


IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_StudentSubjects_SubjectId' AND object_id = OBJECT_ID('dbo.StudentSubjects'))
BEGIN
	CREATE INDEX IX_StudentSubjects_SubjectId ON dbo.StudentSubjects(SubjectId);
END

GO
