CREATE TABLE [dbo].[Route]
(
	[RouteId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Source] INT NOT NULL, 
    [Destination] INT NOT NULL, 
    [Distance] INT NOT NULL, 

    [DepartureTime] TIME NOT NULL, 
    [ArrivalTime] TIME NOT NULL, 
    CONSTRAINT [FK_RouteSource_Cities] FOREIGN KEY ([Source]) REFERENCES [Cities]([CityId]),
	CONSTRAINT [FK_RouteDestination_Cities] FOREIGN KEY ([Destination]) REFERENCES [Cities]([CityId])
)
