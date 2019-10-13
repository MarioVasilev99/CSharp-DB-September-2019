SELECT TOP 50
	e.EmployeeID,
	e.FirstName + ' ' + e.LastName AS EmployeeName,
	managers.FirstName + ' ' + managers.LastName AS ManagerName,
	d.[Name] AS DepartmentName
FROM Employees e
JOIN Employees AS managers
ON e.ManagerID = managers.EmployeeID
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID;
