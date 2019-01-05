
-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <21-dec-2018>
-- Description:	<deactivate/delete/closedeal Vechicle>
-- =============================================
CREATE PROCEDURE USP_CT_DeActive_Delete_Close_Vehicle
(
@UserID bigint,
@RoleID int,
@VehicleID bigint,
@Action int,--(1 deactivate,2 close deal,3 delete)
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;	
	BEGIN TRANSACTION savevehicle
	BEGIN TRY 

	IF(@Action = 1)
	BEGIN
		UPDATE CT_TRAN_Vehicle SET IsActive = 0 WHERE ID= @VehicleID
		SET  @Status = 1;
		SET @Message = 'Vehicle DeActivated successfully';
		END
	ELSE IF(@Action = 2)
	 BEGIN
		UPDATE CT_TRAN_Vehicle SET IsDealClosed = 1 WHERE ID= @VehicleID
		SET  @Status = 1;
		SET @Message = 'Vehicle Deal closed successfully';
		END
	ELSE IF(@Action = 3)
	BEGIN
		UPDATE CT_TRAN_Vehicle SET IsDelete = 1 WHERE ID= @VehicleID
		SET  @Status = 1;
		SET @Message = 'Vehicle deleted successfully';
		END
	COMMIT TRANSACTION savevehicle
	END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION savevehicle
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  	
END CATCH 	
END