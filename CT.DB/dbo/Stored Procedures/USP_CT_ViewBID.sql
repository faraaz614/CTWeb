-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
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
		Select ID,VehicleName,StockID,Description,IsActive,IsDealClosed from [CT_TRAN_Vehicle] where ID = @VehicleID and IsDelete = 0 and IsActive = 1;
		Select * from CT_TRAN_VehicleDetail where VehicleID = @VehicleID
		Select * from CT_TRAN_VehicleImage where VehicleID = @VehicleID
		Select * from CT_TRAN_DocumentDetail where VehicleID = @VehicleID
		Select * from CT_TRAN_TechnicalDetails where VehicleID = @VehicleID
		Select bid.ID,veh.VehicleName,veh.StockID,usr.UserName as DealerName,bid.BIDAmount from CT_TRAN_VehicleBID bid
		join CT_TRAN_Vehicle veh on bid.VehicleID = veh.ID
		join CT_TRAN_User usr on bid.DealerID = usr.ID
		where VehicleID = @VehicleID and usr.IsActive = 1 order by BIDAmount desc
		SET @Message = dbo.UDF_CT_SuccessMessage('') ;
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END