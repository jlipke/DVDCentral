ALTER TABLE [dbo].[tblFormat]
	ADD CONSTRAINT [tblFormat_FormatId]
	FOREIGN KEY (Id)
	REFERENCES [tblMovie] (FormatId)
