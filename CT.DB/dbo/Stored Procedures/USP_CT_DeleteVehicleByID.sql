-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_DeleteVehicleByID]
(
@UserID bigint,
@RoleID int,
@VehicleID int,
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
		Update CT_TRAN_Vehicle set IsDelete = 1, ModifiedBy = @UserID, ModifiedOn = GETDATE() where ID = @VehicleID;
		SET @Message = dbo.UDF_CT_SuccessMessage('delete') ;
	END TRY	
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END