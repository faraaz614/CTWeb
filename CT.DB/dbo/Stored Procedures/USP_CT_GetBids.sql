-- =============================================
--declare @st int,@msg nvarchar(100)
--exec [USP_CT_GetBids] 1, 1, @st out, @msg out
--select @st st,@msg msg
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_GetBids]
(
@UserID bigint,
@RoleID int,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRY 
		if exists(select UserName from CT_TRAN_User where ID = @UserID and RoleID = @RoleID) and (@RoleID = 1)
		begin --if
			SET  @Status = 1;
			Select veh.ID as VehicleID,veh.VehicleName,veh.StockID,veh.IsDealClosed,MAX(bid.BIDAmount) as BIDAmount
			from CT_TRAN_Vehicle veh 
			left outer join CT_TRAN_VehicleBID bid on bid.VehicleID = veh.ID
			where veh.IsDelete = 0 and veh.IsActive = 1 
			group by veh.ID,veh.VehicleName,veh.StockID,veh.IsDealClosed 
			order by IsDealClosed;
			set @Message = dbo.UDF_CT_SuccessMessage('')
		end --if
		else
		begin
			set @Status = 0;
			set @Message = dbo.UDF_CT_SuccessMessage('denied')
		end
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END