SELECT
	f.Id,
	f.Name,
	CONCAT(f.Size, 'KB') AS [Size]
FROM Files f
LEFT JOIN Files
ON f.Id = Files.ParentId
WHERE Files.ParentId IS NULL
ORDER BY f.Id, f.Name, f.Size DESC;