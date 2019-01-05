-- =============================================
-- Author:		Rafeeq Mohammad
-- Create date: <21-dec-2018>
-- Description:	<update DocumentDetail>
-- =============================================
CREATE PROCEDURE USP_CT_Update_VehicleDocumentDetail
(
@UserID bigint,
@RoleID int,
@DocumentDetailID bigint,
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
	SET NOCOUNT ON;
	BEGIN TRY
		SET  @Status = 1;  
		SET @Message = dbo.UDF_CT_SuccessMessage('update') ;
		
		UPDATE CT_TRAN_DocumentDetail
		SET [IsRCavailable] = @IsRCavailable
      ,[Hypothication] = @Hypothication
      ,[IsNOCavailable] = @IsNOCavailable
      ,[NoOfOwners] = @NoOfOwners
      ,[NoOfKeys] = @NoOfKeys
      ,[IsInsuranceAvailable] = @IsInsuranceAvailable
      ,[IsComprehensive] = @IsComprehensive
      ,[IsThirdParty] = @IsThirdParty
      ,[InsuranceExpiryDate] = @InsuranceExpiryDate
      ,[VehicleID] = @VehicleID
      ,[ModifiedBy] = @UserID
      ,[ModifiedOn] = GETDATE()
	WHERE ID = @DocumentDetailID

	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END