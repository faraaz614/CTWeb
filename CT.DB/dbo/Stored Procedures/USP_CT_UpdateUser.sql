CREATE PROCEDURE [dbo].[USP_CT_UpdateUser]  
(  
@ID bigint,  
@UserID bigint,  
@RoleID int,  
@FirstName nvarchar(150) = null,  
@LastName nvarchar(150) = null,  
@UserName nvarchar(150),  
@Password nvarchar(50),  
@Mobile nvarchar(50),
@Mobile2 nvarchar(50) = null,
@ProfilePic nvarchar(500) = null,
@IsActive bit,
@Status int out,  
@Message nvarchar(500) out  
)  
AS  
BEGIN  
 SET NOCOUNT ON;  
 BEGIN TRY   
  SET  @Status = 1;  
  Update CT_TRAN_User set ProfilePic = @ProfilePic,FirstName = @FirstName,LastName = @LastName,  
  [Password] = @Password,Mobile=@Mobile,Mobile2=@Mobile2,IsActive = @IsActive,ModifiedOn = GETDATE(),ModifiedBy = @UserID
  where ID = @ID;  
  SET @Message = dbo.UDF_CT_SuccessMessage('update') ;  
 END TRY   
 BEGIN CATCH       
  SET  @Status = 0;     
  SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;  
  EXECUTE USP_CT_GetErrorInfo;    
 END CATCH    
END