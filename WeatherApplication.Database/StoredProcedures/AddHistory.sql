CREATE PROCEDURE [dbo].[AddHistory]
	@City nvarchar(50),
	@Unit nvarchar(50),  -- Assuming these are the parameters for City and Unit respectively
	@Time datetime,
	@Temperature decimal(18, 2),
	@Humidity int,
	@Wind decimal(18, 2)
AS

begin
	-- Add the logic to insert a new record into the UserHistory table
	-- Example:
	INSERT INTO dbo.[UserHistory] (City, Unit, Time, Temperature, Humidity, Wind)
	VALUES (@City, @Unit, @Time, @Temperature, @Humidity, @Wind);
end
