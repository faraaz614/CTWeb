-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE USP_CT_DeleteVehicleImage
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@ImageName nvarchar(150),
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
		If exists(SELECT * FROM CT_TRAN_VehicleImage WHERE ImageName = @ImageName and VehicleID = @VehicleID)
		begin
			delete from CT_TRAN_VehicleImage where ImageName = @ImageName and VehicleID = @VehicleID;
			SET @Message = dbo.UDF_CT_SuccessMessage('delete') ;
		end		
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END