﻿CREATE FUNCTION [dbo].[UDF_CT_SuccessMessage]
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
else if (@ReturnType = 'vehicleexists')
	begin
	SET @Message='Vehicle with Stock ID is already exists';
	end
else if (@ReturnType = 'bidexists')
	begin
	SET @Message='BID already placed.';
	end
else if (@ReturnType = 'delete')
	begin
	SET @Message='Record Deleted Successfully';
	end
else if (@ReturnType = 'denied')
	begin
SET @Message='Permission Denied';
	end
else if (@ReturnType = 'invaliduser')
	begin
	SET @Message='User Does not exists';
	end
else if (@ReturnType = 'loginsuccess')
	begin
	SET @Message='Login successfull';
	end
else if (@ReturnType = 'inactiveuser')
	begin
	SET @Message='Password wrong or use is not active';
	end
else if (@ReturnType = 'dealclosed')
	begin
	SET @Message='Deal is closed for this car';
	end
else if (@ReturnType = 'bidclosed')
	begin
	SET @Message='BID Closed';
	end
else if (@ReturnType = 'enterhigherbid')
	begin
	SET @Message='Your BID Amount should be higher than previous BID Amount';
	end
else
    begin
	SET @Message='Operation Successfull';
	end
    RETURN  @Message;

END