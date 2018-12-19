-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_AddVehicleDetails]
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@Make varchar(150),
@Model varchar(150),
@Variant varchar(150),
@YearOfManufacturing date,
@FuelTypeID int,
@Kilometers int,
@Transmission varchar(150),
@RegistrationNo varchar(150),
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
		If not exists(SELECT * FROM CT_TRAN_VehicleDetail WHERE VehicleID = @VehicleID)
			begin
				INSERT INTO CT_TRAN_VehicleDetail (Make,Model,Variant,YearOfManufacturing,FuelTypeID,VehicleID,Kilometers,Transmission,RegistrationNo,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn)
				SELECT @Make,@Model,@Variant,@YearOfManufacturing,@FuelTypeID,@VehicleID,@Kilometers,@Transmission,@RegistrationNo,@UserID,GETDATE(),@UserID,GETDATE();
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				Update CT_TRAN_VehicleDetail set Make=@Make,Model=@Model,Variant=@Variant,YearOfManufacturing=@YearOfManufacturing,
				FuelTypeID=@FuelTypeID,Kilometers=@Kilometers,Transmission=@Transmission,
				RegistrationNo=@RegistrationNo,ModifiedBy=@UserID,ModifiedOn=GETDATE()
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