CREATE TABLE [dbo].[UserHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [City] NVARCHAR(50) NOT NULL, 
    [Unit] NVARCHAR(50) NOT NULL, 
    [Time] DATETIME NOT NULL, 
    [Temperature] DECIMAL NULL, 
    [Humidity] DECIMAL NULL, 
    [Wind] DECIMAL NULL
)
