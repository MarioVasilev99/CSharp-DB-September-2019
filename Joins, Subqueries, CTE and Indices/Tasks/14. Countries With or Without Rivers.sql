SELECT TOP 5 c.CountryName, r.RiverName
FROM Countries c
FULL OUTER JOIN CountriesRivers cr
ON c.CountryCode = cr.CountryCode
FULL JOIN Rivers r
ON cr.RiverId = r.Id
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName;