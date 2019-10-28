CREATE PROCEDURE usp_GetEmployeesFromTown(@townName VARCHAR(30))
AS   
BEGIN
	SELECT e.FirstName, e.LastName
	FROM Employees e
	JOIN Addresses AS a ON e.AddressID = a.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.Name = @townName
END;