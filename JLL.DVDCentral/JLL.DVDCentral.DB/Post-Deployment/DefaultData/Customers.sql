BEGIN
	INSERT INTO [dbo].[tblCustomer] (Id, FirstName, LastName, Address, City, State, ZIP, Phone, UserId)
	VALUES
	(1, 'Jim', 'James', '123 Easy st.', 'Appleton', 'WI', '54915', '9201231234', 'YimYames'),
	(2, 'Henry', 'Adams', '412 Weston st.', 'Appleton', 'WI', '54915', '9201235673', 'Hadams'),
	(3, 'John', 'Hancock', '342 Liberty st.', 'Appleton', 'WI', '54915', '9209932345', 'Yankee'),
	(4, 'cpine','enipc', '123 Ceape st.', 'Oshkosh', 'WI', '54901', '9203334444','cpine')
	
END
