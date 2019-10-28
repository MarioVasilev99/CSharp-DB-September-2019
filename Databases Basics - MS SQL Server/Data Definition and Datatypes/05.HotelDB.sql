CREATE DATABASE Hotel

CREATE TABLE Employees(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL, 
	Title VARCHAR(30),
	Notes VARCHAR(MAX)
);

INSERT INTO Employees
	VALUES
		('Iveta', 'Aleksiiewa', NULL, NULL),
		('Peshko', 'Peshev', NULL, NULL),
		('Ivo', 'ivov', NULL, NULL);

CREATE TABLE Customers(
	AccountNumber INT IDENTITY(1, 1) PRIMARY KEY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	PhoneNumber VARCHAR(30) NOT NULL,
	EmergencyName VARCHAR(30) NOT NULL,
	EmergencyNumber VARCHAR(30) NOT NULL,
	Notes VARCHAR(MAX)
);

INSERT INTO Customers
	VALUES
	('Pesho', 'Ivanov', '+359889251674', 'Ivan', '+359887256721', NULL),
	('Ivan', 'Ivanov', '+359889251674', 'Dragan', '+359887256721', NULL),
	('Georgi', 'Ivanov', '+359889251674', 'Petkan', '+359887256721', NULL);


CREATE TABLE RoomStatus(
	RoomStatus VARCHAR(30) PRIMARY KEY,
	Notes VARCHAR(MAX)
);

INSERT INTO RoomStatus
	VALUES
		('Not available', 'Currently booked'),
		('Available', 'Free'),
		('Being cleaned', NULL);

CREATE TABLE RoomTypes(
	RoomType VARCHAR(30) PRIMARY KEY,
	Notes VARCHAR(MAX)
);

INSERT INTO RoomTypes
	VALUES
		('Single', 'one person only'),
		('Double', 'Two people only'),
		('Apartment', 'Up to 4 people allowed');

CREATE TABLE BedTypes(
	BedType VARCHAR(30) PRIMARY KEY,
	Notes VARCHAR(MAX)
);

INSERT INTO BedTypes
	VALUES
		('King sized bed', NULL),
		('Queen sized bed ', NULL),
		('Single bed', NULL);

CREATE TABLE Rooms(
	RoomNumber INT PRIMARY KEY,
	RoomType VARCHAR(30) FOREIGN KEY REFERENCES RoomTypes(RoomType),
	BedType VARCHAR(30) FOREIGN KEY REFERENCES BedTypes(BedType),
	Rate INT NOT NULL,
	RoomStatus VARCHAR(30) FOREIGN KEY REFERENCES RoomStatus(RoomStatus),
	Notes VARCHAR(MAX)
);

INSERT INTO Rooms
	VALUES
		(111, 'Single', 'King sized bed', 60, 'Not available', NULL),
		(112, 'Double', 'Queen sized bed ', 90, 'Available', NULL),
		(113, 'Apartment', 'Single bed', 120, 'Being cleaned', NULL);


CREATE TABLE Payments(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate SMALLDATETIME NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays INT NOT NULL,
	AmountCharged DECIMAL(18,2) NOT NULL,
	TaxRate DECIMAL(18, 2) NOT NULL,
	TaxAmount DECIMAL(18, 2) NOT NULL,
	PaymentTotal DECIMAL(18, 2) NOT NULL,
	Notes VARCHAR(MAX)
);

INSERT INTO Payments
	VALUES
		(1, '2018-12-13 12:43:10', 1, '2018-12-13', '2018-12-15', 2, 130.22, 9.99, 19.98, 150.20, NULL),
		(2, '2018-12-13 12:43:10', 2, '2018-12-13', '2018-12-15', 2, 130.22, 9.99, 19.98, 150.20, NULL), 
		(3, '2018-12-13 12:43:10', 3, '2018-12-13', '2018-12-15', 2, 130.22, 9.99, 19.98, 150.20, NULL);
		 
CREATE TABLE Occupancies(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber),
	RateApplied DECIMAL(18, 2) NOT NULL,
	PhoneCharge DECIMAL (18, 2),
	Notes VARCHAR(MAX)
);

INSERT INTO Occupancies
	VALUES
		(1, '2019-12-13', 1, 111, 80.00, NULL, NULL),
		(2, '2019-12-14', 2, 112, 90.00, NULL, NULL),
		(3, '2019-12-15', 3, 113, 100.00, NULL, NULL);



	


		
