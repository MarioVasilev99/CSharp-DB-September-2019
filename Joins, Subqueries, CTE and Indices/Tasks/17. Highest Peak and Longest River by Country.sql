SELECT TOP 5
	   c.CountryName,
	   MAX(p.Elevation) AS [HighestPeakElevation],
	   MAX(r.[Length]) AS [LongestRiverLength]
FROM Countries c
FULL JOIN MountainsCountries mc
ON c.CountryCode = mc.CountryCode
FULL JOIN Mountains m
ON mc.MountainId = m.Id
FULL JOIN Peaks p
ON m.Id = p.MountainId
FULL JOIN CountriesRivers cr
ON c.CountryCode = cr.CountryCode
FULL JOIN Rivers r
ON cr.RiverId = r.Id
GROUP BY c.CountryName
ORDER BY MAX(p.Elevation) DESC,
		 MAX(r.[Length]) DESC,
		 c.CountryName;