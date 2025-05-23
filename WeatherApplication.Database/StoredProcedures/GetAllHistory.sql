/*CREATE PROCEDURE [dbo].[GetAllHistory]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0*/

CREATE PROCEDURE [dbo].[GetAllHistory]
AS

begin
	select * from dbo.[UserHistory]   order by Time desc
end
