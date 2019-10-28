SELECT
	wd.DepositGroup, 
	MAX(MagicWandSize) AS [LongestMagicWand]
FROM WizzardDeposits wd
GROUP BY wd.DepositGroup