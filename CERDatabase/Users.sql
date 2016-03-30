CREATE TABLE [dbo].[Users]
(
	[UserId] int NOT NULL PRIMARY KEY IDENTITY, 
    [Forename] NVARCHAR(50) NOT NULL, 
    [Surname] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(100) NOT NULL, 
    [Password] NVARCHAR(40) NOT NULL, 
    [Employee] BIT NOT NULL
)
