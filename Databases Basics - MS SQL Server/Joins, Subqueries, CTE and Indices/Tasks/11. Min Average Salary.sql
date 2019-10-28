SELECT TOP 1 AVG(e.Salary) AS MinAverageSalary
FROM Employees e
GROUP BY e.DepartmentID
ORDER BY AVG(e.Salary);
