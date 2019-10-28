SELECT *
INTO newTable
FROM Employees e
WHERE e.Salary > 30000

DELETE
FROM newTable
WHERE ManagerID = 42;

UPDATE newTable
SET Salary = Salary + 5000
WHERE DepartmentID = 1;

SELECT
	nt.DepartmentID,
	AVG(nt.Salary) AS [AverageSalary]
FROM newTable nt
GROUP BY nt.DepartmentID