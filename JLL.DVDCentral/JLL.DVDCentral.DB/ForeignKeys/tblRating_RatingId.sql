ALTER TABLE [dbo].[tblRating]
	ADD CONSTRAINT [tblRating_RatingId]
	FOREIGN KEY (Id)
	REFERENCES [tblMovie] (RatingId)
