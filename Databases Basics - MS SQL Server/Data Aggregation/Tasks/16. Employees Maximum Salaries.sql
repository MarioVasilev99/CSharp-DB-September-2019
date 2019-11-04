SELECT 
	e.DepartmentID,
	MAX(e.Salary) AS [MaxSalary]
FROM Employees e
GROUP BY e.DepartmentID
HAVING MAX(e.Salary) NOT BETWEEN 30000 AND 69999;