BEGIN
	INSERT INTO [dbo].[tblOrder] (Id, CustomerId, OrderDate, UserId, PaymentReceipt)
	VALUES
	(1, 1, '2019-9-15', 'YimYames', '$15.00'),
	(2, 2, '2019-9-16', 'Hadams', '$3.00'),
	(3, 2, '2019-9-21', 'Hadams', '$13.60')
END
