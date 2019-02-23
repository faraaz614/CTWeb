﻿-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_SaveBIDByUserID]
(
@UserID bigint,
@RoleID int,
@VehicleID bigint,
@BIDAmount decimal,
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
		if exists (Select IsDealClosed from CT_TRAN_Vehicle where ID = @VehicleID and IsActive = 1 and IsDelete = 0 and IsDealClosed = 0)
		begin
			INSERT INTO CT_TRAN_VehicleBID (VehicleID,BIDAmount,DealerID,Description,CreatedOn,ModifiedOn)
			SELECT @VehicleID,@BIDAmount,@UserID,null,GETDATE(),GETDATE();
			SET @Message = dbo.UDF_CT_SuccessMessage('insert');	
		end 
		else
		begin
			SET @Message = dbo.UDF_CT_SuccessMessage('dealclosed');	
		end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END