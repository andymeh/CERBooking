CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY, 
    [Forename] NCHAR(50) NOT NULL, 
    [Surname] NCHAR(50) NOT NULL, 
    [Address] NCHAR(200) NOT NULL
)
