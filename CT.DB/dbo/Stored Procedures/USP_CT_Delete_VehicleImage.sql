-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <21-dec-2018>
-- Description:	<Delete Vechicle Image>
-- =============================================
CREATE PROCEDURE USP_CT_Delete_VehicleImage
(
@UserID bigint,
@RoleID int,
@ImageID bigint,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;	
	BEGIN TRANSACTION savevehicle
	BEGIN TRY 
	DELETE FROM CT_TRAN_VehicleImage WHERE ID = @ImageID
	SET  @Status = 1;  
	SET @Message = dbo.UDF_CT_SuccessMessage('delete') ;
	COMMIT TRANSACTION savevehicle
	END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION savevehicle
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  	
END CATCH 	
END