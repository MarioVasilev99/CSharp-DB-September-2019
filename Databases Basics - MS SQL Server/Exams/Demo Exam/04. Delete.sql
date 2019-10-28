DELETE
FROM RepositoriesContributors
WHERE RepositoryId IN (
	SELECT r.Id
	FROM Repositories r
	WHERE r.Name = 'Softuni-Teamwork'
);
						

DELETE
FROM Issues
WHERE RepositoryId IN (
	SELECT r.Id
	FROM Repositories r
	WHERE r.Name = 'Softuni-Teamwork'
);