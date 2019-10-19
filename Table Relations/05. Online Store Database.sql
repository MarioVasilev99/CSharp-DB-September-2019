CREATE DATABASE OnlineStore;

CREATE TABLE Cities(
	CityID INT IDENTITY(1, 1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
);

CREATE TABLE Customers(
	CustomerID INT IDENTITY(1, 1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	Birthday DATE NOT NULL,
	CityID INT FOREIGN KEY REFERENCES Cities(CityID)
);

CREATE TABLE ItemTypes(
	ItemTypeID INT IDENTITY(1, 1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
);

CREATE TABLE Items(
	ItemID INT IDENTITY(1, 1) PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
);

CREATE TABLE Orders(
	OrderID INT IDENTITY(1, 1) PRIMARY KEY,
	CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderItems(
	OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
	ItemID INT FOREIGN KEY REFERENCES Items(ItemID),
	PRIMARY KEY(OrderID, ItemID)
);