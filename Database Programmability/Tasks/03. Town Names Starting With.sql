CREATE PROCEDURE usp_GetTownsStartingWith
	@Letter NVARCHAR(20)
AS   
    SELECT t.[Name] AS [Town]
    FROM Towns t 
    WHERE LEFT(t.[Name], LEN(@Letter)) = @Letter;