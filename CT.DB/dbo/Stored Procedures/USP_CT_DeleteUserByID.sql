CREATE PROCEDURE [dbo].[USP_CT_DeleteUserByID]
(
@UserID bigint,
@RoleID int,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY 
		SET  @Status = 1;
		Update CT_TRAN_User set IsActive = ~IsActive,CreatedOn = GETDATE() where ID = @UserID;
		SET @Message = dbo.UDF_CT_SuccessMessage('update');
	END TRY	
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END