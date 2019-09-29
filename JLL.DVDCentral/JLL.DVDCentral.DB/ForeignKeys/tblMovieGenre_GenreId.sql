ALTER TABLE [dbo].[tblMovieGenre]
	ADD CONSTRAINT [tblMovieGenre_GenreId]
	FOREIGN KEY (GenreId)
	REFERENCES [tblGenre] (Id)
