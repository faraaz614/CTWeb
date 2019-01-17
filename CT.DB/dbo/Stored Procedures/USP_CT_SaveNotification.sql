-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <11/25/2018>
-- Description:	<Save User>
-- =============================================
Create PROCEDURE [dbo].[USP_CT_SaveNotification]
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@Body nvarchar(200),
@Title nvarchar(150),
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
		INSERT INTO CT_TRAN_Notification(VehicleID,Body,Title,CreatedBy,CreatedOn)
		SELECT @VehicleID,@Body,@Title,@UserID,GETDATE();
		SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END