SELECT FirstName
FROM Employees
WHERE DepartmentID = 3 OR DepartmentID = 10 AND
	  HireDate >= '1995-01-01 00:00:00' AND HireDate <= '2005-12-30 23:59:59';