CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
BEGIN
	
	DECLARE @time INT;

	IF @StartDate IS NULL
	BEGIN
		RETURN 0
	END

	IF @EndDate IS NULL
	BEGIN
		RETURN 0
	END

	SET @time = DATEDIFF(hh, @StartDate, @EndDate)
	RETURN @time
END

GO
SELECT dbo.udf_HoursToComplete1(OpenDate, CloseDate) AS TotalHours
   FROM Reports
