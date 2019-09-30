BEGIN
	INSERT INTO [dbo].[tblOrder] (Id, CustomerId, OrderDate, UserId, PaymentReceipt)
	VALUES
	(1, 1, '2019-9-15', 1, 'Receipt'),
	(2, 2, '2019-9-16', 2, 'Receipt'),
	(3, 2, '2019-9-21', 2, 'Receipt')
END
