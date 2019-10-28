SELECT 
	COUNT(e.EmployeeID) AS [Count]
FROM Employees e
WHERE e.ManagerID IS NULL;