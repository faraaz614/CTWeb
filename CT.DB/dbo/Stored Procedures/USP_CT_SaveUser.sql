-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <11/25/2018>
-- Description:	<Save User>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_SaveUser]
(
@UserID bigint,
@RoleID int,
@FirstName nvarchar(150) = null,
@LastName nvarchar(150) = null,
@UserName nvarchar(150),
@Password nvarchar(50),
@Mobile nvarchar(50),
@Mobile2 nvarchar(50) = null,
@ProfilePic nvarchar(500) = null,
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
		If not exists(SELECT * FROM CT_TRAN_User WHERE UserName = @UserName and IsActive = 1)
			begin
				INSERT INTO CT_TRAN_User (RoleID,ProfilePic,FirstName,LastName,UserName,[Password],Mobile,Mobile2,IsActive,CreatedBy,CreatedOn)
				SELECT 3,@ProfilePic,@FirstName,@LastName,@UserName,@Password,@Mobile,@Mobile2,1,@UserID,GETDATE();
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				SET @Message = dbo.UDF_CT_SuccessMessage('exists') ;
				SET  @Status = 0;  
			end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END