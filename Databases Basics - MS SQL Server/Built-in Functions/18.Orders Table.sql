SELECT
	o.ProductName,
	o.OrderDate,
	DATEADD(day, 3, o.OrderDate) AS [Pay Due],
	DATEADD(month, 1, o.OrderDate) AS [Deliver Due]
FROM Orders o





