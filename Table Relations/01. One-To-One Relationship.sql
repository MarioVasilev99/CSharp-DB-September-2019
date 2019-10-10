CREATE TABLE Passports(
	PassportID INT IDENTITY(101,1) PRIMARY KEY,
	PassportNumber VARCHAR(8) NOT NULL
);


CREATE TABLE Persons(
	PersonID INT IDENTITY(1, 1) PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	Salary DECIMAL(18,2) NOT NULL,
	PassportID INT FOREIGN KEY REFERENCES Passports(PassportID)
);

INSERT INTO Passports
	VALUES
		('N34FG21B'),
		('K65LO4R7'),
		('ZE657QP2');

INSERT INTO Persons
	VALUES
		('Roberto', 43300.00, 102),
		('Tom', 56100.00, 103),
		('Yana', 60200.00, 101);