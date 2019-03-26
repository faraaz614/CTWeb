--declare @statusid int
--declare @msg varchar(100)
--exec [USP_CT_GetUserTokenByID] 1,@statusid out, @msg out
--select @statusid,@msg
CREATE PROCEDURE [dbo].[USP_CT_GetUserTokenByID]
(
@UserID bigint,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SET  @Status = 1;
		DECLARE @DealerID bigint;
		Select @DealerID = DealerID from CT_TRAN_VehicleBID where ID = @UserID;
		Select Token from CT_TRAN_UserToken where UserID = @DealerID
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END