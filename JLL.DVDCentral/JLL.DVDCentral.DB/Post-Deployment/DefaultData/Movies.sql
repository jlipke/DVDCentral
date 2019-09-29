BEGIN
	INSERT INTO [dbo].[tblMovie] (Id, Title, Description, ImagePath, Cost, InStockQty, RatingId, FormatId, DirectorId)
	VALUES
	(1, 'Spider-Man: Far From Home', 'imgpath', 9.99, 15, 3, 1, 1),
	(2, 'Toy Story 4', 'imgpath', 10.99, 23, 1, 1, 2),
	(3, 'Beetle Juice', 'imgpath', 3.99, 2, 2, 2, 3)
	

END