SELECT 
	u.Username,
	c.Name AS [CategoryName]
FROM Users u
LEFT JOIN Reports r
ON u.Id = r.UserId
JOIN Categories c
ON r.CategoryId = c.Id
WHERE DATEPART(d, u.Birthdate) = DATEPART(d, r.OpenDate)
    AND DATEPART(m, u.Birthdate) = DATEPART(m, r.OpenDate)
ORDER BY u.Username, c.Name