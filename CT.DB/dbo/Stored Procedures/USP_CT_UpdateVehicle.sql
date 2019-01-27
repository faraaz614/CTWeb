
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_UpdateVehicle]
(
@UserID bigint,
@RoleID int,
@VehicleID bigint,
@VehicleName nvarchar(150),
@StockID nvarchar(50),
@Description nvarchar(150) = null,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY 
		If not exists(SELECT * FROM CT_TRAN_Vehicle WHERE VehicleName = @VehicleName and StockID = @StockID and IsActive = 1)
			begin
				SET  @Status = 1;
				Update CT_TRAN_Vehicle set VehicleName = @VehicleName, StockID = @StockID, Description = @Description, 
				ModifiedBy = @UserID, ModifiedOn = GETDATE() where ID = @VehicleID;
				SET @Message = dbo.UDF_CT_SuccessMessage('update') ;
			end
		else
			begin
				SET @Message = dbo.UDF_CT_SuccessMessage('vehicleexists') ;
				SET  @Status = 0;  
			end
	END TRY	
	BEGIN CATCH     
		SET  @Status = 0;
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END