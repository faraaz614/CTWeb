
-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <21-dec-2018>
-- Description:	<Save/UPDATE Vechicle>
-- =============================================
CREATE PROCEDURE USP_CT_Insert_Update_Vehicle
(
@UserID bigint,
@RoleID int,
@ID bigint,
@VehicleName nvarchar(150),
@StockID nvarchar(50),
@Description nvarchar(150) = null,
@ReturnID bigint OUT,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;	
	BEGIN TRANSACTION savevehicle
	BEGIN TRY 
	IF(@ID = 0)--INSERT
		BEGIN
		  INSERT INTO CT_TRAN_Vehicle (StockID,VehicleName,[Description],IsActive,IsDealClosed,IsDelete,CreatedOn,CreatedBy)
		  SELECT @StockID,@VehicleName,@Description,1,0,0,GETDATE(),@UserID;
		  SET @ReturnID = SCOPE_IDENTITY();
		  SET @Status = 1;   
		  SET @Message = dbo.UDF_CT_SuccessMessage('insert') ;
		END
	
	ELSE --UPDATE
	   BEGIN
		  UPDATE CT_TRAN_Vehicle SET 
		  VehicleName = @VehicleName
		  ,[Description] = @Description
		  ,ModifiedOn = GETDATE()
		  ,ModifiedBy = @UserID
		  WHERE ID = @ID;
		  SET @ReturnID = @ID;
		  SET @Status = 1;   
		  SET @Message = dbo.UDF_CT_SuccessMessage('update') ;
	   END
	
	COMMIT TRANSACTION savevehicle
	END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION savevehicle
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  	
END CATCH 	
END