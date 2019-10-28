SELECT
	LEFT(wd.FirstName, 1) AS [FirstLetter]
FROM WizzardDeposits wd
WHERE wd.DepositGroup = 'Troll Chest'
GROUP BY LEFT(wd.FirstName, 1)
ORDER BY LEFT(wd.FirstName, 1);
