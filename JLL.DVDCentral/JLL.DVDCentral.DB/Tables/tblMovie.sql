CREATE TABLE [dbo].[tblMovie]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(1000) NOT NULL, 
    [ImagePath] VARCHAR(50) NOT NULL, 
    [Cost] MONEY NOT NULL, 
    [InStockQty] INT NOT NULL, 
    [RatingId] INT NOT NULL, 
    [FormatId] INT NOT NULL, 
    [DirectorId] INT NOT NULL
)
