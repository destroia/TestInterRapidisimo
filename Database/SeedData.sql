-- Script de datos de ejemplo para poblar la base creada por CreateTables.sql
-- Asegúrate de ejecutar primero CreateTables.sql

SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

-- Inserta profesores de ejemplo
IF NOT EXISTS (SELECT 1 FROM dbo.Professors WHERE Dni = 11111111)
BEGIN
	INSERT INTO dbo.Professors (Dni, Name) VALUES (11111111, N'Profesor Uno');
END
IF NOT EXISTS (SELECT 1 FROM dbo.Professors WHERE Dni = 22222222)
BEGIN
	INSERT INTO dbo.Professors (Dni, Name) VALUES (22222222, N'Profesor Dos');
END
IF NOT EXISTS (SELECT 1 FROM dbo.Professors WHERE Dni = 33333333)
BEGIN
	INSERT INTO dbo.Professors (Dni, Name) VALUES (33333333, N'Profesor Tres');
END

-- Inserta alumnos de ejemplo
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE Dni = 44444444)
BEGIN
	INSERT INTO dbo.Students (Dni, Name) VALUES (44444444, N'Estudiante A');
END
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE Dni = 55555555)
BEGIN
	INSERT INTO dbo.Students (Dni, Name) VALUES (55555555, N'Estudiante B');
END
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE Dni = 66666666)
BEGIN
	INSERT INTO dbo.Students (Dni, Name) VALUES (66666666, N'Estudiante C');
END
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE Dni = 77777777)
BEGIN
	INSERT INTO dbo.Students (Dni, Name) VALUES (77777777, N'Estudiante D');
END
IF NOT EXISTS (SELECT 1 FROM dbo.Students WHERE Dni = 88888888)
BEGIN
	INSERT INTO dbo.Students (Dni, Name) VALUES (88888888, N'Estudiante E');
END

-- Inserta materias de ejemplo (asignando professors, máximo 2 por profesor)
-- Suponemos que los Ids auto-generados de profesores son 1,2,3 en el orden insertado
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE Name = 'Matemáticas I')
BEGIN
	INSERT INTO dbo.Subjects (Name, Credits, ProfessorId) VALUES (N'Matemáticas I', 4, 1);
END
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE Name = 'Programación I')
BEGIN
	INSERT INTO dbo.Subjects (Name, Credits, ProfessorId) VALUES (N'Programación I', 5, 1);
END
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE Name = 'Física I')
BEGIN
	INSERT INTO dbo.Subjects (Name, Credits, ProfessorId) VALUES (N'Física I', 4, 2);
END
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE Name = 'Química I')
BEGIN
	INSERT INTO dbo.Subjects (Name, Credits, ProfessorId) VALUES (N'Química I', 3, 2);
END
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE Name = 'Algoritmos')
BEGIN
	INSERT INTO dbo.Subjects (Name, Credits, ProfessorId) VALUES (N'Algoritmos', 5, 3);
END
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects WHERE Name = 'Bases de Datos')
BEGIN
	INSERT INTO dbo.Subjects (Name, Credits, ProfessorId) VALUES (N'Bases de Datos', 4, 3);
END

-- Inserta relaciones Profesor<->Materia en ProfessorSubjects (evita duplicados)
INSERT INTO dbo.ProfessorSubjects (ProfessorId, SubjectId)
SELECT p.Id, s.Id
FROM dbo.Professors p
JOIN dbo.Subjects s ON s.ProfessorId = p.Id
WHERE NOT EXISTS (
	SELECT 1 FROM dbo.ProfessorSubjects ps WHERE ps.ProfessorId = p.Id AND ps.SubjectId = s.Id
);

-- Asignaciones de materias a estudiantes (cada estudiante exactamente 3 materias y sin repetir profesor)
-- Para simplicidad usamos IDs conocidos asumidos por inserción secuencial; si la base tiene otros Ids, ajustar.
-- Recoger Ids reales desde la tabla

DECLARE @s1 INT = (SELECT TOP 1 Id FROM dbo.Students WHERE Dni = 44444444);
DECLARE @s2 INT = (SELECT TOP 1 Id FROM dbo.Students WHERE Dni = 55555555);
DECLARE @s3 INT = (SELECT TOP 1 Id FROM dbo.Students WHERE Dni = 66666666);
DECLARE @s4 INT = (SELECT TOP 1 Id FROM dbo.Students WHERE Dni = 77777777);
DECLARE @s5 INT = (SELECT TOP 1 Id FROM dbo.Students WHERE Dni = 88888888);

-- Obtener materias por nombre
DECLARE @m1 INT = (SELECT TOP 1 Id FROM dbo.Subjects WHERE Name = 'Matemáticas I');
DECLARE @m2 INT = (SELECT TOP 1 Id FROM dbo.Subjects WHERE Name = 'Programación I');
DECLARE @m3 INT = (SELECT TOP 1 Id FROM dbo.Subjects WHERE Name = 'Física I');
DECLARE @m4 INT = (SELECT TOP 1 Id FROM dbo.Subjects WHERE Name = 'Química I');
DECLARE @m5 INT = (SELECT TOP 1 Id FROM dbo.Subjects WHERE Name = 'Algoritmos');
DECLARE @m6 INT = (SELECT TOP 1 Id FROM dbo.Subjects WHERE Name = 'Bases de Datos');

-- Estudiante A: Matemáticas I, Física I, Algoritmos (profesores 1,2,3 -> válidos)
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s1 AND SubjectId = @m1)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s1, @m1);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s1 AND SubjectId = @m3)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s1, @m3);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s1 AND SubjectId = @m5)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s1, @m5);

-- Estudiante B: Programación I, Química I, Bases de Datos (profesores 1,2,3 -> válidos)
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s2 AND SubjectId = @m2)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s2, @m2);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s2 AND SubjectId = @m4)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s2, @m4);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s2 AND SubjectId = @m6)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s2, @m6);

-- Estudiante C: Matemáticas I, Programación I, Bases de Datos (profesores 1,1,3) -> cuidado: repite profesor 1
-- En este script evitamos repetir profesor: asignamos Matemáticas I, Física I, Algoritmos
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s3 AND SubjectId = @m1)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s3, @m1);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s3 AND SubjectId = @m3)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s3, @m3);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s3 AND SubjectId = @m5)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s3, @m5);

-- Estudiante D: Programación I, Química I, Algoritmos
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s4 AND SubjectId = @m2)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s4, @m2);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s4 AND SubjectId = @m4)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s4, @m4);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s4 AND SubjectId = @m5)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s4, @m5);

-- Estudiante E: Matemáticas I, Bases de Datos, Química I
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s5 AND SubjectId = @m1)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s5, @m1);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s5 AND SubjectId = @m6)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s5, @m6);
IF NOT EXISTS (SELECT 1 FROM dbo.StudentSubjects WHERE StudentId = @s5 AND SubjectId = @m4)
	INSERT INTO dbo.StudentSubjects (StudentId, SubjectId) VALUES (@s5, @m4);

COMMIT TRANSACTION;
GO

-- Verificación rápida: listar estudiantes con sus materias
SELECT st.Id AS StudentId, st.Name AS StudentName,
	STRING_AGG(s.Name, ', ') AS Subjects
FROM dbo.Students st
LEFT JOIN dbo.StudentSubjects ss ON ss.StudentId = st.Id
LEFT JOIN dbo.Subjects s ON s.Id = ss.SubjectId
GROUP BY st.Id, st.Name;
GO
