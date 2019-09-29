ALTER TABLE [dbo].[tblDirector]
	ADD CONSTRAINT [tblDirector_DirectorId]
	FOREIGN KEY (Id)
	REFERENCES [tblMovie] (DirectorId)
