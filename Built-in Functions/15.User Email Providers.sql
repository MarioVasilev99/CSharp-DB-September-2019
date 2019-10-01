SELECT Username, RIGHT(Email, LEN(Email) - CHARINDEX('@', email)) AS [Email Provider]
FROM Users
ORDER BY [Email Provider], Username;