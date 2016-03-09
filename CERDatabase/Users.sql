CREATE TABLE [dbo].[Users]
(
	[UserId] NCHAR(30) NOT NULL PRIMARY KEY, 
    [Forename] NCHAR(50) NOT NULL, 
    [Surname] NCHAR(50) NOT NULL, 
    [EmailAddress] NCHAR(100) NOT NULL
)
