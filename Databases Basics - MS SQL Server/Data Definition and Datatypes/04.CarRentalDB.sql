--TASK 14

CREATE DATABASE CarRental

CREATE TABLE Categories(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	CategoryName VARCHAR(30) NOT NULL,
	DailyRate INT NOT NULL,
	WeeklyRate INT NOT NULL,
	MonthlyRate INT NOT NULL,
	WeekendRate INT NOT NULL
);

INSERT INTO Categories
	VALUES
		('Mini Car', 10, 60, 250, 15),
		('Economy Car', 15, 70, 270, 20),
		('Compact Car', 16, 73, 290, 23);

CREATE TABLE Cars(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	PlateNumber VARCHAR(20) UNIQUE,
	Manufacturer VARCHAR(30) NOT NULL,
	Model VARCHAR(30) NOT NULL,
	CarYear INT,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT,
	Picture VARBINARY(MAX) CHECK (DATALENGTH(Picture) <= 52428800),
	Condition VARCHAR(30),
	Available BIT CHECK((Available) = 0 OR (Available) = 1) NOT NULL,
);

INSERT INTO Cars
	VALUES
		('CO2343AE', 'BMW', '535D', 2008, 1, 4, NULL, 'Mint', 0),
		('CO2343A3', 'BMW', '530D', 2008, 2, 4, NULL, 'Mint', 1),
		('CO2343AS', 'BMW', '525D', 2008, 3, 4, NULL, 'Mint', 1);

CREATE TABLE Employees(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	Title VARCHAR(20),
	Notes VARCHAR(MAX)
);

INSERT INTO Employees
	VALUES
		('David', 'Goliath', NULL, 'Monster'),
		('Michael', 'Jordan', NULL, 'BBall Legend'),
		('Rikardo', 'Pelucci', NULL, NULL);

CREATE TABLE Customers(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	DriverLicenceNumber INT UNIQUE NOT NULL,
	FullName VARCHAR(50) NOT NULL,
	[Address] VARCHAR(50) NOT NULL,
	City VARCHAR(30) NOT NULL,
	ZIPCode INT,
	Notes VARCHAR(MAX)
);

INSERT INTO Customers
	VALUES
		(323232, 'Peter Petrov', 'Boulevard Bulgaria', 'Sofia', 2, NULL),
		(323231, 'Ivan Petrov', 'Boulevard Bulgaria', 'Sofia', 3, NULL),
		(323233, 'Petkan Petrov', 'Boulevard Bulgaria', 'Sofia', 4, NULL);

CREATE TABLE RentalOrders(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel INT NOT NULL,
	KilometrageStart DECIMAL(18, 2) NOT NULL,
	KilometrageEnd DECIMAL(18, 2) NOT NULL,
	TotalKilometrage DECIMAL(18, 2) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays INT NOT NULL,
	RateApplied INT,
	TaxRate INT,
	OrderStatus VARCHAR(30),
	Notes VARCHAR(MAX)
);


INSERT INTO RentalOrders
	VALUES
		(1, 2, 1, 60, 182000.32, 183000.43, 253093.33,  '2019-09-18', '2019-09-20', 2, 30, NULL, NULL, NULL),
		(2, 1, 2, 60, 182000.32, 183000.44, 253093.32,  '2019-09-18', '2019-09-20', 2, 30, NULL, NULL, NULL),
		(3, 3, 3, 60, 182000.33, 183000.44, 253093.31,  '2019-09-18', '2019-09-20', 2, 30, NULL, NULL, NULL);