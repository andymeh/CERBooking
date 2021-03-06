﻿CREATE TABLE [dbo].[Booking]
(
	[BookingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [TrainRouteId] INT NOT NULL, 
    [NoInParty] INT NOT NULL, 
	[FirstClass] BIT NOT NULL, 
    [statusOfBooking] NVARCHAR(20) NOT NULL, 
    [DateBooked] DATETIME NOT NULL, 
    [DateCancelled] DATETIME NULL, 
    [BookingCost] FLOAT NOT NULL, 
    CONSTRAINT [FK_Booking_TrainRoute] FOREIGN KEY ([TrainRouteId]) REFERENCES [TrainRoute]([TrainRouteId]),
	CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
)
