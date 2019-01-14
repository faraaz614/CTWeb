--============================================
-- Author:		Shaik Mohammad Rafeeq
-- Create date: <21-dec-2018>
-- Description:	<Save Vehicle BID>
-- =============================================
create PROCEDURE USP_CT_Save_VehicleBID
(
@UserID bigint,
@RoleID int,
@VehicleID [bigint] =NULL,
@BIDAmount [decimal](18, 0) =NULL,
@Description [nvarchar](500)= NULL,
@DealerID [bigint] =NULL,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY
	
INSERT INTO CT_TRAN_VehicleBID
           ([VehicleID]
           ,[BIDAmount]
           ,[Description]
           ,[DealerID]
           ,[CreatedOn])
     VALUES
           (@VehicleID
           ,@BIDAmount
           ,@Description
           ,@DealerID
           ,GETDATE())

	SET  @Status = 1;  
	SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
		
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END