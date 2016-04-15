CREATE TABLE [dbo].[TrainRoute]
(
	[TrainRouteId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TrainId] INT NOT NULL, 
    [RouteId] INT NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [FirstClassSeats] INT NOT NULL, 
    [EconomySeats] INT NOT NULL, 
    [CostEconomy] FLOAT NOT NULL, 
    [CostFirstClass] FLOAT NOT NULL, 
    CONSTRAINT [FK_TrainRoute_Train] FOREIGN KEY ([TrainId]) REFERENCES [Train]([TrainId]),
	CONSTRAINT [FK_TrainRoute_Route] FOREIGN KEY ([RouteId]) REFERENCES [Route]([RouteId])
)
