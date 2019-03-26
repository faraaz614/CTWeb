CREATE PROCEDURE [dbo].[USP_CT_SaveRegistration]
(
@UserID bigint,
@RoleID int,
@refreshedToken varchar(MAX),
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		SET  @Status = 1;
		if exists (Select * from CT_TRAN_UserToken where UserID = @UserID)
		begin
			update CT_TRAN_UserToken set Token = @refreshedToken, ModifiedOn = GETDATE() where UserID = @UserID
			SET @Message = dbo.UDF_CT_SuccessMessage('update');	
		end 
		else 
		begin
			INSERT INTO CT_TRAN_UserToken (Token,UserID,CreatedOn,ModifiedOn)
			SELECT @refreshedToken,@UserID,GETDATE(),GETDATE();
			SET @Message = dbo.UDF_CT_SuccessMessage('insert');	
		end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END