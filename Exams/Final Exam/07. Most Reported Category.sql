SELECT TOP 5
	c.Name AS [CategoryName],
	COUNT(c.Name) AS [ReportsNumber]
FROM Categories c
JOIN Reports r
ON c.Id = r.CategoryId
GROUP BY c.Name
ORDER BY COUNT(c.Name) DESC, c.Name