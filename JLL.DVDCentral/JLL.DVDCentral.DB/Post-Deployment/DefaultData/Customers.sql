BEGIN
	INSERT INTO [dbo].[tblCustomer] (Id, FirstName, LastName, Address, City, State, ZIP, Phone, UserId)
	VALUES
	(1, 'Jim', 'James', '123 Easy st.', 'Appleton', 'WI', '54915', '9201231234', 1),
	(2, 'Henry', 'Adams', '412 Weston st.', 'Appleton', 'WI', '54915', '9201235673', 2),
	(3, 'John', 'Hankock', '342 Liberty st.', 'Appleton', 'WI', '54915', '9209932345', 3)
END
