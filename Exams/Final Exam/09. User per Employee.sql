SELECT
	CONCAT(e.FirstName, ' ' + e.LastName) AS [FullName],
	COUNT(e.Id) AS [UsersCount]
FROM Employees e
JOIN Reports r
ON e.Id = r.EmployeeId
GROUP BY e.Id, e.FirstName, e.LastName
ORDER BY COUNT(e.Id) DESC, CONCAT(e.FirstName, ' ' + e.LastName);
