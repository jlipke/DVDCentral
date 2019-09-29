ALTER TABLE [dbo].[tblOrder]
	ADD CONSTRAINT [tblOrder_CustomerId]
	FOREIGN KEY (CustomerId)
	REFERENCES [tblCustomer] (Id)
