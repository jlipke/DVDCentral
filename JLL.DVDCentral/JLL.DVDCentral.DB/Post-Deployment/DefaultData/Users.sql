BEGIN
	INSERT INTO [dbo].[tblUser] (Id, UserId, FirstName, LastName, UserPass)
	VALUES
	(1, 1, 'Jim', 'James', 'p@ssword'),
	(2, 2, 'Henry', 'Adams', 'password'),
	(3, 3, 'John', 'Hancock', 'merica')
END
