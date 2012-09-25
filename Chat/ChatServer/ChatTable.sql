CREATE TABLE dbo.tblBenutzer
(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	LoginName NVARCHAR(128),
	Password NVARCHAR(128)
)

CREATE TABLE dbo.tblVerbindungen
(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Datum DATETIME NOT NULL,
	BenutzerID INT REFERENCES [dbo].[tblBenutzer] ([ID])	
)

/*
DROP TABLE
	dbo.tblVerbindungen,
	dbo.tblBenutzer
*/