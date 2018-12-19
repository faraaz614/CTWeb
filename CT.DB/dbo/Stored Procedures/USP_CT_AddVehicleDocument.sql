-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE USP_CT_AddVehicleDocument
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@IsRCavailable bit,
@Hypothication bit,
@IsNOCavailable bit,
@NoOfOwners int,
@NoOfKeys int,
@IsInsuranceAvailable bit,
@IsComprehensive bit,
@IsThirdParty bit,
@InsuranceExpiryDate date,
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
		If not exists(SELECT * FROM CT_TRAN_DocumentDetail WHERE VehicleID = @VehicleID)
			begin
				INSERT INTO CT_TRAN_DocumentDetail(IsRCavailable,Hypothication,IsNOCavailable,NoOfOwners,NoOfKeys,IsInsuranceAvailable,IsComprehensive,IsThirdParty,InsuranceExpiryDate,VehicleID,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn)
				SELECT @IsRCavailable,@Hypothication,@IsNOCavailable,@NoOfOwners,@NoOfKeys,@IsInsuranceAvailable,@IsComprehensive,@IsThirdParty,@InsuranceExpiryDate,@VehicleID,@UserID,GETDATE(),@UserID,GETDATE();
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				Update CT_TRAN_DocumentDetail set IsRCavailable=@IsRCavailable,Hypothication=@Hypothication,IsNOCavailable=@IsNOCavailable,
				NoOfOwners=@NoOfOwners,NoOfKeys=@NoOfKeys,IsInsuranceAvailable=@IsInsuranceAvailable,IsComprehensive=@IsComprehensive,
				IsThirdParty=@IsThirdParty,InsuranceExpiryDate=@InsuranceExpiryDate,
				ModifiedBy=@UserID,ModifiedOn=GETDATE()
				where VehicleID = @VehicleID;
				SET @Message = dbo.UDF_CT_SuccessMessage('update') ;
			end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END