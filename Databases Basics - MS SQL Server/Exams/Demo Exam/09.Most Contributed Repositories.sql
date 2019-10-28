SELECT TOP 5 r.Id,r.Name ,COUNT(rp.RepositoryId) AS [Commits]
FROM Repositories r
JOIN Commits c
ON r.Id = c.RepositoryId
JOIN RepositoriesContributors rp
ON c.RepositoryId = rp.RepositoryId
GROUP BY r.Id, rp.RepositoryId, r.Name
ORDER BY COUNT(rp.RepositoryId) DESC, r.Id, r.Name