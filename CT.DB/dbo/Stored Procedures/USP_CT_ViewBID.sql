-- =============================================
--declare @status int
--declare @message varchar(150)
--exec [USP_CT_ViewBID] 1, 1, 2, @status out, @message out
--select @status, @message
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_ViewBID]
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
		Select * from [CT_TRAN_Vehicle] where ID = @VehicleID and IsDelete = 0 and IsActive = 1;
		Select *,f.Type as FuelType from CT_TRAN_VehicleDetail vd
		join CT_SYS_FuelType f on vd.FuelTypeID = f.ID
		where VehicleID = @VehicleID;
		Select * from CT_TRAN_VehicleImage where VehicleID = @VehicleID;
		Select * from CT_TRAN_DocumentDetail where VehicleID = @VehicleID;
		Select * from CT_TRAN_TechnicalDetails where VehicleID = @VehicleID;
		with cte as
		(Select bid.ID,veh.VehicleName,veh.StockID,usr.FirstName,usr.LastName,usr.Mobile,bid.BIDAmount,bid.CreatedOn,
		ROW_NUMBER() over (partition by bid.DealerID order by bid.BIDAmount desc) as ron
		from CT_TRAN_VehicleBID bid
		join CT_TRAN_Vehicle veh on bid.VehicleID = veh.ID
		join CT_TRAN_User usr on bid.DealerID = usr.ID
		where VehicleID = @VehicleID and usr.IsActive = 1 and bid.IsActive = 1) 
		select ID,VehicleName,StockID,FirstName,LastName,Mobile,BIDAmount from cte
		where ron = 1;
		SET @Message = dbo.UDF_CT_SuccessMessage('') ;
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END