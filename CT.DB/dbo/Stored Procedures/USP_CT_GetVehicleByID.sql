-- =============================================
--declare @statusid int
--declare @msg varchar(100)
--exec [USP_CT_GetVehicleByID] 1,1,1,@statusid out, @msg out
--select @statusid,@msg
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_GetVehicleByID]
(
@UserID bigint,
@RoleID int,
@VehicleID int,
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
		Declare @SQLQuery nvarchar(max);
		set @SQLQuery = 'Select ID,VehicleName,StockID,Description,BidTime,(CAST(Datediff(s, GETDATE(), BidTime) AS BIGINT)*1000) as BidTimeMilliSecs,
		BidDurationID,IsActive,IsDealClosed 
		from [CT_TRAN_Vehicle] where ID = '+CONVERT(varchar(10), @VehicleID)+' and IsDelete = 0 ';

		if(@RoleID = 3)
		begin
			set @SQLQuery += ' and IsActive = 1';
		end
		EXECUTE sp_executesql @SQLQuery
		
		Select vd.*,f.Type as FuelType from CT_TRAN_VehicleDetail vd
		join CT_SYS_FuelType f on vd.FuelTypeID = f.ID
		where VehicleID = @VehicleID
		Select * from CT_TRAN_VehicleImage where VehicleID = @VehicleID
		Select * from CT_TRAN_DocumentDetail where VehicleID = @VehicleID
		Select * from CT_TRAN_TechnicalDetails where VehicleID = @VehicleID
		SELECT VehicleID,DealerID,Description,BIDAmount FROM CT_TRAN_VehicleBID where VehicleID = @VehicleID and IsActive = 1
		SET @Message = dbo.UDF_CT_SuccessMessage('') ;
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END