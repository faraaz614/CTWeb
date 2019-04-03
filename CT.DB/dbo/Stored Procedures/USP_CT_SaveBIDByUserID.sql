--declare @statusid int
--declare @msg varchar(100)
--exec [USP_CT_SaveBIDByUserID] 2,3,56,1234,@statusid out, @msg out
--select @statusid,@msg
CREATE PROCEDURE [dbo].[USP_CT_SaveBIDByUserID]
(
@UserID bigint,
@RoleID int,
@VehicleID bigint,
@BIDAmount decimal,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY
		Declare @IsDealClosed bit;
		Declare @BidTime int;
		Declare @PreviousBidAmount int;
		set @PreviousBidAmount = 0;
		SET  @Status = 1;

		Select @IsDealClosed = IsDealClosed, @BidTime = DATEDIFF(SECOND,GETDATE(), BidTime) from CT_TRAN_Vehicle 
		where ID = @VehicleID and IsActive = 1 and IsDelete = 0;

		if ((select COUNT(*) from CT_TRAN_VehicleBID where VehicleID = @VehicleID) > 0)
		begin
			select @PreviousBidAmount = MAX(BIDAmount) from CT_TRAN_VehicleBID where VehicleID = @VehicleID;
		end

		if (@IsDealClosed = 1)
		begin
			SET @Message = dbo.UDF_CT_SuccessMessage('dealclosed');	
		end 
		else if (@PreviousBidAmount >= @BIDAmount)
		begin
			SET @Message = dbo.UDF_CT_SuccessMessage('enterhigherbid');			
		end
		else if (@BidTime < -5)
		begin
			SET @Message = dbo.UDF_CT_SuccessMessage('bidclosed');	
		end 
		else if (@IsDealClosed = 0 and @PreviousBidAmount < @BIDAmount and @BidTime > -5)
		begin
			INSERT INTO CT_TRAN_VehicleBID (VehicleID,BIDAmount,DealerID,Description,CreatedOn,ModifiedOn)
			SELECT @VehicleID,@BIDAmount,@UserID,null,GETDATE(),GETDATE();
			SET @Message = dbo.UDF_CT_SuccessMessage('insert');	
			if (@BidTime < 120)
			begin
				update CT_TRAN_Vehicle set BidTime = DATEADD(SECOND,120,BidTime) where ID = @VehicleID
			end

			Select ID,VehicleName,StockID,Description,BidTime,(CAST(Datediff(s, GETDATE(), BidTime) AS BIGINT)*1000) as BidTimeMilliSecs,
			case when DATEDIFF(MINUTE,GETDATE(),BidTime) > 30 then 60 else 30 end as BidDurationID,IsActive,IsDealClosed 
			from [CT_TRAN_Vehicle] where ID = @VehicleID
		end
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END