CREATE PROCEDURE [dbo].[USP_CT_SaveVehicle]
(
@UserID bigint,
@RoleID int,
@VehicleName nvarchar(150),
@StockID nvarchar(50),
@Description nvarchar(150) = null,
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
		If not exists(SELECT * FROM CT_TRAN_Vehicle WHERE VehicleName = @VehicleName and StockID = @StockID and IsActive = 1)
			begin
				INSERT INTO CT_TRAN_Vehicle(VehicleName,StockID,Description,IsDealClosed,IsActive,IsDelete,CreatedOn,CreatedBy)
				SELECT @VehicleName,@StockID,@Description,0,1,0,GETDATE(),@UserID;
				SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
			end
		else
			begin
				SET @Message = dbo.UDF_CT_SuccessMessage('vehicleexists') ;
				SET  @Status = 0;  
			end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END