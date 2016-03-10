CREATE TABLE [dbo].[Booking]
(
	[BookingId] INT NOT NULL PRIMARY KEY, 
    [UserId] NCHAR(30) NOT NULL, 
    [TrainRouteId] INT NOT NULL, 
    [NoInParty] INT NOT NULL, 
    [statusOfBooking] NCHAR(20) NOT NULL, 
    CONSTRAINT [FK_Booking_TrainRoute] FOREIGN KEY ([TrainRouteId]) REFERENCES [TrainRoute]([TrainRouteId]),
	CONSTRAINT [FK_Booking_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([UserId])
)
