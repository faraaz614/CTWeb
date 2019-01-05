--============================================
-- Author:		Shaik Mohammad Rafeeq
-- Create date: <21-dec-2018>
-- Description:	<Save VehicleDetail>
-- =============================================
CREATE PROCEDURE USP_CT_Save_Vechile_Details
(
@UserID bigint,
@RoleID int,
@Make [nvarchar](150) NULL,
@Model [nvarchar](150) NULL,
@Variant [nvarchar](150) NULL,
@YearOfManufacturing [date] NULL,
@FuelTypeID [int] NULL,
@VehicleID [bigint] NULL,
@Kilometers [int] NULL,
@Transmission [nvarchar](150) NULL,
@RegistrationNo [nvarchar](50) NULL,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY

	INSERT INTO CT_TRAN_VehicleDetail
           ([Make]
           ,[Model]
           ,[Variant]
           ,[YearOfManufacturing]
           ,[FuelTypeID]
           ,[VehicleID]
           ,[Kilometers]
           ,[Transmission]
           ,[RegistrationNo]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (@Make
           ,@Model
           ,@Variant
           ,@YearOfManufacturing
           ,@FuelTypeID
           ,@VehicleID
           ,@Kilometers
           ,@Transmission
           ,@RegistrationNo
           ,@UserID
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