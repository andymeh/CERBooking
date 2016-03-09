CREATE TABLE [dbo].[RouteStops]
(
	[RouteStopId] INT NOT NULL PRIMARY KEY, 
    [RouteId] INT NOT NULL, 
    [CityId] INT NOT NULL, 
    [ArrivalTime] TIME NOT NULL, 
    [DepartureTime] TIME NOT NULL, 
    CONSTRAINT [FK_RouteStops_Route] FOREIGN KEY ([RouteId]) REFERENCES [Route]([RouteId]),
	CONSTRAINT [FK_RouteStops_City] FOREIGN KEY ([CityId]) REFERENCES [Cities]([CityId])
)
