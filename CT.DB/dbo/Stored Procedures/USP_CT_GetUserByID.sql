-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--declare @statusid int
--declare @msg varchar(100)
--exec [USP_CT_GetUserByID] 1,1,1,@statusid out, @msg out
--select @statusid,@msg
CREATE PROCEDURE [dbo].[USP_CT_GetUserByID]  
(  
@ID bigint,
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
  Select * from [CT_TRAN_User] where ID = @ID and IsActive = 1;  
  SET @Message = dbo.UDF_CT_SuccessMessage('') ;  
 END TRY   
 BEGIN CATCH  
  SET  @Status = 0;     
  SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;  
  EXECUTE USP_CT_GetErrorInfo;    
 END CATCH    
END