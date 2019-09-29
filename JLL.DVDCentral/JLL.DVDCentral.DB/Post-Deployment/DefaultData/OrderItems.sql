BEGIN
	INSERT INTO [dbo].[tblOrderItem] (Id, OrderId, MovieId, Quantity)
	VALUES
	(1, 1, 3, 1),
	(2, 2, 2, 1),
	(3, 3, 1, 1)
END