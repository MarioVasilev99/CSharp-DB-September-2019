CREATE DATABASE Minions

CREATE TABLE Minions (
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(30),
	Age TINYINT
);

CREATE TABLE Towns (
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(30)
);

ALTER TABLE Minions
ADD TownId INT

ALTER TABLE Minions
ADD FOREIGN KEY (TownId) REFERENCES Towns(Id);

INSERT INTO Towns
VALUES(1, 'Sofia');

INSERT INTO Towns
VALUES(2, 'Plovdiv');

INSERT INTO Towns
VALUES(3, 'Varna');

INSERT INTO Minions
VALUES(1, 'Kevin', 22, 1);

INSERT INTO Minions
VALUES(2, 'Bob', 15, 3);

INSERT INTO Minions
VALUES(3, 'Steward', NULL, 2);

TRUNCATE TABLE Minions;

DROP TABLE Minions;
DROP TABLE Towns;

--Task 7
CREATE TABLE People (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX) CHECK (DATALENGTH(Picture) <= 2097152),
	Height DECIMAL(18,2),
	[Weight] DECIMAL(18,2),
	Gender CHAR(1) NOT NULL CHECK(Gender = 'f' OR Gender = 'm'),
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
);

INSERT INTO People
VALUES ('PESHO', NULL, 17.2, 99, 'm', '1999-01-10', 'biography');

INSERT INTO People
VALUES ('Gosho', NULL, 17.3, 99.9, 'm', '1999-02-10', 'biographyTest');

INSERT INTO People
VALUES ('Tosho', NULL, 17.2, 99, 'm', '1999-01-10', 'biography');

INSERT INTO People
VALUES ('Radka', NULL, 17.2, 99, 'f', '1999-01-10', 'biography');

INSERT INTO People
VALUES ('Piratka', NULL, 17.2, 99, 'f', '1999-01-10', 'biography');

--Task 8
CREATE TABLE Users (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR(30) NOT NULL UNIQUE,
	Password VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX) CHECK(DATALENGTH(ProfilePicture) <= 900000),
	LastLoginTime DATETIME,
	IsDeleted BIT CHECK(IsDeleted = 0 OR IsDeleted = 1)
);

INSERT INTO Users
VALUES ('Piratka', 'tainaparola123', NULL, '2008-11-11 13:23:44', 0);

INSERT INTO Users
VALUES ('Goshko', 'tainaparola123', NULL, '2008-11-11 13:23:44', 0);

INSERT INTO Users
VALUES ('razbivacha', 'tainaparola123', NULL, '2008-11-11 13:23:44', 0);

INSERT INTO Users
VALUES ('shonkata', 'tainaparola123', NULL, '2008-11-11 13:23:44', 0);

INSERT INTO Users
VALUES ('rikardo', 'tainaparola123', NULL, '2008-11-11 13:23:44', 0);

--TASK 9
ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC07BB702E02;

ALTER TABLE Users
ADD CONSTRAINT PK_CompositeIdUsername
PRIMARY KEY (Id, Username);

--TASK 10
ALTER TABLE Users
	ADD CONSTRAINT CK_Password_AtLeastFiveSymbols
	CHECK(LEN(Password) > 4);

--TASK 11
ALTER TABLE Users
	ADD CONSTRAINT col_lastLoginTime_def
	DEFAULT GETDATE() FOR LastLoginTime;

--TASK 12
ALTER TABLE Users
	DROP CONSTRAINT PK_CompositeIdUsername


ALTER TABLE Users
	ADD CONSTRAINT CK_Username_Lenght
	CHECK(LEN(Username) > 2);