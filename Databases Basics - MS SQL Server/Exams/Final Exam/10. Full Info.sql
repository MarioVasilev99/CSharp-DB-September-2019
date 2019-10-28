SELECT
	CASE
		WHEN e.FirstName IS NULL THEN 'None'
		WHEN e.LastName IS NULL THEN 'None'
		ELSE CONCAT(e.FirstName, ' ',e.LastName)
	END AS [Employee],
	CASE
		WHEN d.Name IS NULL THEN 'None'
		ELSE d.Name
	END AS [Department],
	CASE
		WHEN c.Name IS NULL THEN 'None'
		ELSE c.Name
	END AS [Category],
	CASE
		WHEN r.Description IS NULL THEN 'None'
		ELSE r.Description
	END AS [Description],
	COALESCE(NULLIF(CONVERT(VARCHAR(10), r.OpenDate, 104),''), 'None') AS [OpenDate],
	COALESCE(NULLIF(s.Label,''), 'None') AS [Status],
	COALESCE(NULLIF(u.Name,''), 'None')  AS [User]
FROM Reports r
LEFT JOIN Employees e
ON r.EmployeeId = e.Id
LEFT JOIN Departments d
ON e.DepartmentId = d.Id
LEFT JOIN Categories c
ON r.CategoryId = c.Id
LEFT JOIN Status s
ON r.StatusId = s.Id
LEFT JOIN Users u
ON r.UserId = u.Id
ORDER BY e.FirstName DESC, e.LastName DESC, d.Name, c.Name, r.Description,
r.OpenDate, s.Label, u.Name
