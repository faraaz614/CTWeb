-- =============================================
-- Author:		Rafeeq Mohammad
-- Create date: <21-dec-2018>
-- Description:	<Save DocumentDetail>
-- =============================================
CREATE PROCEDURE USP_CT_SaveVehicleDocumentDetail
(
@UserID bigint,
@RoleID int,
@IsRCavailable [bit] NULL,
@Hypothication [bit] NULL,
@IsNOCavailable [bit] NULL,
@NoOfOwners [int] NULL,
@NoOfKeys [int] NULL,
@IsInsuranceAvailable [bit] NULL,
@IsComprehensive [bit] NULL,
@IsThirdParty [bit] NULL,
@InsuranceExpiryDate [date] NULL,
@VehicleID [bigint] NULL,
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
		SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
		
		INSERT INTO CT_TRAN_DocumentDetail
           ([IsRCavailable]
           ,[Hypothication]
           ,[IsNOCavailable]
           ,[NoOfOwners]
           ,[NoOfKeys]
           ,[IsInsuranceAvailable]
           ,[IsComprehensive]
           ,[IsThirdParty]
           ,[InsuranceExpiryDate]
           ,[VehicleID]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (@IsRCavailable
           ,@Hypothication
           ,@IsNOCavailable
           ,@NoOfOwners
           ,@NoOfKeys
           ,@IsInsuranceAvailable
           ,@IsComprehensive
           ,@IsThirdParty
           ,@InsuranceExpiryDate
           ,@VehicleID
           ,@UserID
           ,GETDATE())

	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END