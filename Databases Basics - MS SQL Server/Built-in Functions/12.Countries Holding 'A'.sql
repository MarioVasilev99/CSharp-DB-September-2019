SELECT CountryName, IsoCode
FROM Countries
WHERE LEN(CountryName) - LEN(replace(CountryName,'a','')) > 2
ORDER BY IsoCode;