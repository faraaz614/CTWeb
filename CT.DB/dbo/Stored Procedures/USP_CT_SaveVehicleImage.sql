-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE USP_CT_SaveVehicleImage
(
@UserID bigint,
@RoleID int,
@VehicleID int,
@ImageName nvarchar(150),
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
		If not exists(SELECT * FROM CT_TRAN_VehicleImage WHERE ImageName = @ImageName and VehicleID = @VehicleID)
		begin
			INSERT INTO CT_TRAN_VehicleImage(ImageName,VehicleID,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy)
			SELECT @ImageName,@VehicleID,GETDATE(),@UserID,GETDATE(),@UserID;
			SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
		end		
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END