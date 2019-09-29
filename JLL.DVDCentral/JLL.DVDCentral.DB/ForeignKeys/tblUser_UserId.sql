ALTER TABLE [dbo].[tblUser]
	ADD CONSTRAINT [tblUser_UserId]
	FOREIGN KEY (Id)
	REFERENCES [tblCustomer] (UserId)
