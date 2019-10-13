SELECT e.EmployeeID, e.FirstName, e.ManagerID, mg.FirstName
FROM Employees e
JOIN Employees mg
ON e.ManagerID = mg.EmployeeID
WHERE mg.EmployeeID IN (3, 7)
ORDER BY e.EmployeeID;
