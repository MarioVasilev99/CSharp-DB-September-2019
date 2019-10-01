SELECT 
	p.PeakName,
	r.RiverName,
	LOWER(LEFT(PeakName, LEN(PeakName) - 1) + RiverName) AS [Mix]
FROM Peaks p, Rivers r
WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
ORDER BY [Mix];

