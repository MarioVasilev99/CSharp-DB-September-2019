CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
DECLARE @empDepartment VARCHAR(50);

SET @empDepartment = 
	(
		SELECT d.Id
		FROM Employees e
		JOIN Departments d
		ON e.DepartmentId = d.Id
		WHERE e.Id = @EmployeeId
	)

DECLARE @reportDepartment VARCHAR(50);

SET @reportDepartment =
	(
		SELECT c.DepartmentId
		FROM Reports r
		JOIN Categories c
		ON r.CategoryId = c.Id
		WHERE r.Id = @ReportId
	)

IF @empDepartment <> @reportDepartment
BEGIN
	RAISERROR('Employee doesn''t belong to the appropriate department!',16,1)  
END
ELSE
BEGIN
UPDATE Reports
SET EmployeeId = @EmployeeId
WHERE Id = @ReportId
END



EXEC usp_AssignEmployeeToReport 17, 2