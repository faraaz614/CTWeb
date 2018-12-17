-- =============================================
-- Author:		faraaz
-- Create date: <12/17/2018>
-- Description:	<SaveVechile>
-- =============================================
CREATE PROCEDURE [USP_CT_SaveVehicle]
(
@UserID bigint,
@RoleID int,
@VehicleName nvarchar(150),
@Description nvarchar(150) = null,
@IsDealClosed bit,
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
		If not exists(SELECT * FROM CT_TRAN_Vehicle WHERE VehicleName = @VehicleName and IsActive = 1)
			begin
				INSERT INTO CT_TRAN_Vehicle(VehicleName,Description,IsDealClosed,IsActive,IsDelete,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy)
				SELECT @VehicleName,@Description,@IsDealClosed,1,0,GETDATE(),@UserID,GETDATE(),@UserID;
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				SET @Message = dbo.UDF_CT_SuccessMessage('exists') ;
				SET  @Status = 0;  
			end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END