-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [USP_CT_SaveVehicleTechnical]
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@Engine varchar(500),
@Body varchar(500),
@SuspensionSteeringSystem varchar(500),
@Transmission varchar(500),
@Electrical varchar(500),
@AirCondition varchar(500),
@Brakes varchar(500),
@TyresCondition varchar(500),
@OtherInformation varchar(500),
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
		If not exists(SELECT * FROM CT_TRAN_TechnicalDetails WHERE VehicleID = @VehicleID)
			begin
				INSERT INTO CT_TRAN_TechnicalDetails(Engine,Body,SuspensionSteeringSystem,Transmission,Electrical,AirCondition,Brakes,TyresCondition,OtherInformation,VehicleID,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn)
				SELECT @Engine,@Body,@SuspensionSteeringSystem,@Transmission,@Electrical,@AirCondition,@Brakes,@TyresCondition,@OtherInformation,@VehicleID,@UserID,GETDATE(),@UserID,GETDATE();
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				Update CT_TRAN_TechnicalDetails set Engine=@Engine,Body=@Body,SuspensionSteeringSystem=@SuspensionSteeringSystem,
				Transmission=@Transmission,Electrical=@Electrical,AirCondition=@AirCondition,Brakes=@Brakes,
				TyresCondition=@TyresCondition,OtherInformation=@OtherInformation,
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