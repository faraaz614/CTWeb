-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE USP_CT_Login
(
@UserName nvarchar(150),
@Password nvarchar(50),
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY 
		if exists(select UserName from CT_TRAN_User where UserName = @UserName)
		begin
			if exists(select UserName from CT_TRAN_User where UserName = @UserName and Password = @Password and IsActive = 1)
			begin --if
				SET  @Status = 1;
				Select ID as UserID,RoleID,UserName from CT_TRAN_User where UserName = @UserName and Password = @Password and IsActive = 1
				set @Message = dbo.UDF_CT_SuccessMessage('loginsuccess')
			end--if
			else
			begin
				set @Status = 0;
				set @Message = dbo.UDF_CT_SuccessMessage('inactiveuser')
			end
		end
		else
		begin
			set @Status = 0;
			set @Message = dbo.UDF_CT_SuccessMessage('invaliduser')
		end
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END