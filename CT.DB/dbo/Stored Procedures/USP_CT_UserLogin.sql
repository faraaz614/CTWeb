-- =============================================
-- Author:		<SMR,Mohammad Rafeeq Shaik>
-- Create date: <21-Dec-2018>
-- Description:	<User Login>
-- Declare @Status int,@Message nvarchar(500) exec USP_CT_UserLogin 'srikanth@cartimez.in','car@123',@Status out,@Message out ; select @Status Status ,@Message Message
-- =============================================
CREATE PROCEDURE USP_CT_UserLogin
@UserName nvarchar(150),
@Password nvarchar(50),
@Status int out,
@Message nvarchar(500) out
AS
BEGIN
	SET NOCOUNT ON;
BEGIN TRY 

IF EXISTS(SELECT * FROM CT_TRAN_User where UserName = @UserName)
	BEGIN
		IF EXISTS(SELECT * FROM CT_TRAN_User where UserName = @UserName AND [Password]=@Password AND IsActive = 1)
		 BEGIN
			SET	@Status =1;
			SET @Message = dbo.UDF_CT_SuccessMessage('loginsuccess')
			SELECT ID,FirstName,LastName,UserName,[Password],ProfilePic,RoleID FROM CT_TRAN_User where UserName = @UserName AND [Password]=@Password AND IsActive = 1
		 END
		ELSE
		 BEGIN
			SET	@Status = 0;
			SET @Message = dbo.UDF_CT_SuccessMessage('inactiveuser')			
		 END

	END
ELSE 
	BEGIN		
			SET	@Status = 0;
			SET @Message = dbo.UDF_CT_SuccessMessage('invaliduser')
	END
END TRY	

BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
END CATCH  	
END