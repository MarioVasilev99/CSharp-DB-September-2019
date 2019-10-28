CREATE FUNCTION udf_UserTotalCommits(@username NVARCHAR(MAX))
RETURNS INT
AS
	BEGIN
		DECLARE @commits INT;

		SET @commits =
		(
			SELECT COUNT(c.ContributorId)
			FROM Users u
			JOIN Commits c
			ON u.Id = c.ContributorId
			WHERE u.Username = 'UnderSinduxrein'
		);

		RETURN @commits;
	END

GO
SELECT dbo.udf_UserTotalCommits('UnderSinduxrein') AS [Result]

