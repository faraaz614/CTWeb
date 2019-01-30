-- =============================================
-- declare @st int,@msg nvarchar(100)
-- exec [USP_CT_GetVehicles] '', 1, 1, @st out, @msg out
-- select @st st,@msg msg

-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_GetVehicles]
(
@SearchText nvarchar(150) = NULL,
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
		if exists(select UserName from CT_TRAN_User where ID = @UserID and RoleID = @RoleID) 
		begin --if
			SET  @Status = 1;
			Declare @SQLQuery nvarchar(1000);
			set @SQLQuery = 'Select * from 
			(Select v.ID,v.VehicleName,v.StockID,v.Description,v.IsDealClosed,vi.ImageName,vd.Model,n.VehicleID as NotificationVID,ROW_NUMBER() over(partition by v.id order by v.id) as rowno
			from CT_TRAN_Vehicle v
			left outer join CT_TRAN_VehicleDetail vd on v.ID = vd.VehicleID
			left outer join CT_TRAN_VehicleImage vi on v.ID = vi.VehicleID
			left outer join CT_TRAN_Notification n on v.ID = n.VehicleID
			where v.IsActive = 1 and v.IsDelete = 0) a
			where a.rowno = 1';
			if (@SearchText is not null and @SearchText != '')
			begin
				set @SQLQuery += ' and VehicleName like ''%' + @SearchText + '%''';
			end
			EXECUTE sp_executesql @SQLQuery
			set @Message = dbo.UDF_CT_SuccessMessage('')
		end--if
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