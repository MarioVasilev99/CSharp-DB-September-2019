CREATE DATABASE Movies

CREATE TABLE Directors(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DirectorName VARCHAR(30) NOT NULL,
	Notes VARCHAR(MAX)
);

INSERT INTO Directors 
	VALUES
		('peshko', 'asdsa'),
		('goshko', 'test'),
		('toshko', NULL),
		('ilko', 'asdsa'),
		('toncho', 'asdsa')

CREATE TABLE Genres(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	GenreName VARCHAR(30) NOT NULL,
	Notes VARCHAR(MAX)
);

INSERT INTO Genres
	VALUES
		('comedy', 'laugh'),
		('action', NULL),
		('drama', 'shit'),
		('thriller', 'nice'),
		('anime', NULL);

CREATE TABLE Categories(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CategoryName VARCHAR(30) NOT NULL,
	Notes VARCHAR(MAX)
);

INSERT INTO Categories
	VALUES
		('18+', 'adults only'),
		('16+', NULL),
		('14+', NULL),
		('10+', 'ten plus'),
		('4+', 'kids allowed')

CREATE TABLE Movies(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Title VARCHAR(30) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear INT,
	[Length] INT,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Rating DECIMAL(4,1),
	Notes VARCHAR(MAX)
);

INSERT INTO Movies
	VALUES
		('FF5', 2, 2010, 120, 3, 4, 10.0, 'TEST'),
		('Mickey Mouse', 1, 2011, 110, 2, 3, 10.0, 'TEST'), 
		('FF5', 3, 2009, 130, 5, 1, 9.0, 'TEST'), 
		('FF5', 5, 2007, 140, 1, 2, 3.0, 'TEST'), 
		('FF5', 4, 2005, 150, 4, 5, 2.0, 'TEST');