CREATE DATABASE Service;

CREATE TABLE Departments(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL
);

CREATE TABLE Users(
	Id INT IDENTITY PRIMARY KEY,
	Username VARCHAR(30) UNIQUE NOT NULL,
	Password VARCHAR(50) NOT NULL,
	Name VARCHAR(50),
	Birthdate DATE,
	Age INT CHECK(Age BETWEEN 14 AND 110),
	Email VARCHAR(50) NOT NULL
);

CREATE TABLE Employees(
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(25),
	LastName VARCHAR(25),
	Birthdate DATE,
	Age INT CHECK(Age BETWEEN 14 AND 110),
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
);

CREATE TABLE Categories(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL
);

CREATE TABLE Status(
	Id INT IDENTITY PRIMARY KEY,
	Label VARCHAR(30) NOT NULL,
);

CREATE TABLE Reports(
	Id INT IDENTITY PRIMARY KEY,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	StatusId INT FOREIGN KEY REFERENCES Status(Id) NOT NULL,
	OpenDate DATE NOT NULL,
	CloseDate DATE,
	Description VARCHAR(200) NOT NULL,
	UserId INT FOREIGN KEY REFERENCES Users(Id) NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
);