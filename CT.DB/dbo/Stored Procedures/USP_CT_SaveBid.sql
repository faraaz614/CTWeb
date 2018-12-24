-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE USP_CT_SaveBid
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@BIDAmount int,
@Description nvarchar(500),
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY
		SET  @Status = 1;  
		If not exists(SELECT * FROM CT_TRAN_VehicleBID WHERE VehicleID = @VehicleID and DealerID = @UserID)
			begin
				INSERT INTO CT_TRAN_VehicleBID (VehicleID,BIDAmount,Description,DealerID,CreatedOn,ModifiedOn)
				SELECT @VehicleID,@BIDAmount,@Description,@UserID,GETDATE(),GETDATE();
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				Update CT_TRAN_VehicleBID set BIDAmount=@BIDAmount,Description=@Description,ModifiedOn=GETDATE()
				WHERE VehicleID = @VehicleID and DealerID = @UserID;
				SET @Message = dbo.UDF_CT_SuccessMessage('update') ;
			end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END