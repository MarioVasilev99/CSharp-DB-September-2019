SELECT COUNT(*) AS Count
FROM Countries c
FULL JOIN MountainsCountries mc
ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL
