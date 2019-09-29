ALTER TABLE [dbo].[tblMovie]
	ADD CONSTRAINT [tblMovie_MovieId]
	FOREIGN KEY (Id)
	REFERENCES [tblMovieGenre] (MovieId)
