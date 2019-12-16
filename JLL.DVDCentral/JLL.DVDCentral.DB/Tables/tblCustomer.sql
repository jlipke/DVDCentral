﻿CREATE TABLE [dbo].[tblCustomer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Address] VARCHAR(50) NOT NULL, 
    [City] VARCHAR(15) NOT NULL, 
    [State] VARCHAR(2) NOT NULL, 
    [ZIP] VARCHAR(5) NOT NULL, 
    [Phone] VARCHAR(12) NOT NULL, 
    [UserId] VARCHAR(50) NOT NULL
)
