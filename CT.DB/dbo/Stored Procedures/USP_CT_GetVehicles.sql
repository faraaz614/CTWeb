-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [USP_CT_GetVehicles]
(
@UserID bigint,
@RoleID int,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY 
		if exists(select UserName from CT_TRAN_User where ID = @UserID and RoleID = @RoleID) and (@RoleID = 1)
		begin --if
			SET  @Status = 1;
			Select ID,VehicleName,Description,IsDealClosed from CT_TRAN_Vehicle where IsActive = 1 and IsDelete = 0;
			set @Message = dbo.UDF_CT_SuccessMessage('')
		end--if
		else
		begin
			set @Status = 0;
			set @Message = dbo.UDF_CT_SuccessMessage('denied')
		end
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END