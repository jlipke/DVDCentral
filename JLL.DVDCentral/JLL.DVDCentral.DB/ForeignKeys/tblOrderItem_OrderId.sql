ALTER TABLE [dbo].[tblOrderItem]
	ADD CONSTRAINT [tblOrderItem_OrderId]
	FOREIGN KEY (OrderId)
	REFERENCES [tblOrder] (Id)
