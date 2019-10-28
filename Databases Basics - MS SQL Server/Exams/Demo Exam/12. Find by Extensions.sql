CREATE PROC usp_FindByExtension(@extension VARCHAR(MAX))
AS
	SELECT
	f.Id,
	f.[Name],
	CONCAT(f.Size, 'KB') AS [Size]
	FROM Files f
	WHERE f.[Name] LIKE CONCAT('%.', @extension)
	ORDER BY f.Id, f.[Name], f.Size DESC

GO
EXEC usp_FindByExtension 'txt'