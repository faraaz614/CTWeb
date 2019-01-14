--============================================
-- Author:		Shaik Mohammad Rafeeq
-- Create date: <21-dec-2018>
-- Description:	<Save Vechile TechnicalDetails>
-- =============================================
CREATE PROCEDURE USP_CT_Save_Vechile_TechnicalDetails
(
@UserID bigint,
@RoleID int,
@Engine [nvarchar](500) =NULL,
@Body [nvarchar](500)= NULL,
@SuspensionSteeringSystem [nvarchar](500) =NULL,
@Transmission [nvarchar](500)= NULL,
@Electrical [nvarchar](500)= NULL,
@AirCondition [nvarchar](500)= NULL,
@Brakes [nvarchar](500) =NULL,
@TyresCondition [nvarchar](500) =NULL,
@OtherInformation [nvarchar](500)= NULL,
@VehicleID [bigint] =NULL,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY

	INSERT INTO CT_TRAN_TechnicalDetails
           ([Engine]
           ,[Body]
           ,[SuspensionSteeringSystem]
           ,[Transmission]
           ,[Electrical]
           ,[AirCondition]
           ,[Brakes]
           ,[TyresCondition]
           ,[OtherInformation]
           ,[VehicleID]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (@Engine
           ,@Body
           ,@SuspensionSteeringSystem
           ,@Transmission
           ,@Electrical
           ,@AirCondition
           ,@Brakes
           ,@TyresCondition
           ,@OtherInformation
           ,@VehicleID
           ,GETDATE()
           ,@UserID)

		SET  @Status = 1;  
		SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
		
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END