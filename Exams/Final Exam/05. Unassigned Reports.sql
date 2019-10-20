SELECT
	r.Description,
	CONCAT(FORMAT(r.OpenDate, 'dd'),'-',FORMAT(r.OpenDate, 'MM'), '-', FORMAT(r.OpenDate, 'yyyy')) AS [OpenDate]
FROM Reports r
WHERE r.EmployeeId IS NULL
ORDER BY r.OpenDate, r.Description;