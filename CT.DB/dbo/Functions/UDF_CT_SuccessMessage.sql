CREATE FUNCTION UDF_CT_SuccessMessage
(
    @ReturnType nvarchar(50)--like insert,update,delete
)
RETURNS nvarchar(500)
AS
BEGIN

DECLARE @Message  nvarchar(500);
if(@ReturnType = 'insert')
	begin
	SET @Message='Successfully Data Saved';
	end
else if (@ReturnType = 'update')
	begin
	SET @Message='Successfully Data Updated';
	end
else if (@ReturnType = 'exists')
	begin
	SET @Message='User name already exists';
	end
else
    begin
	SET @Message='Operation Successfull';
	end
    RETURN  @Message;

END