-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--declare @statusid int
--declare @msg varchar(100)
--exec USP_CT_GetBIDSByUserID 1,1,@statusid out, @msg out
--select @statusid,@msg

CREATE PROCEDURE [dbo].[USP_CT_GetBIDSByUserID]
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
		SET  @Status = 1;
		Select a.* from 
		(Select v.ID,v.VehicleName,v.StockID,v.Description,v.IsDealClosed,vi.ImageName,vd.Model,ROW_NUMBER() over(partition by v.id order by v.id) as rowno
		from CT_TRAN_Vehicle v
		left outer join CT_TRAN_VehicleDetail vd on v.ID = vd.VehicleID
		left outer join CT_TRAN_VehicleImage vi on v.ID = vi.VehicleID
		where v.IsActive = 1 and v.IsDelete = 0) a
		join CT_TRAN_VehicleBID vb on a.ID = vb.VehicleID
		where a.rowno = 1 and vb.DealerID = @UserID
		SET @Message = dbo.UDF_CT_SuccessMessage('');
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END