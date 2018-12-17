-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [USP_CT_UpdateVehicle]
(
@UserID bigint,
@RoleID int,
@VehicleID bigint,
@VehicleName nvarchar(150),
@Description nvarchar(150) = null,
@IsDealClosed bit,
@IsActive bit,
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
		Update CT_TRAN_Vehicle set VehicleName = @VehicleName, Description = @Description, IsDealClosed = @IsDealClosed, 
		IsActive = @IsActive, ModifiedBy = @UserID, ModifiedOn = GETDATE()
		where ID = @VehicleID;
		SET @Message = dbo.UDF_CT_SuccessMessage('update') ;
	END TRY	
	BEGIN CATCH     
		SET  @Status = 0;
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END