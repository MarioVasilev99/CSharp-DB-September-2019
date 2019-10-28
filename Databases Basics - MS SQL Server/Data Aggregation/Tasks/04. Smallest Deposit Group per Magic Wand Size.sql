SELECT TOP(2)
	wd.DepositGroup
FROM WizzardDeposits wd
GROUP BY wd.DepositGroup
ORDER BY AVG(MagicWandSize)