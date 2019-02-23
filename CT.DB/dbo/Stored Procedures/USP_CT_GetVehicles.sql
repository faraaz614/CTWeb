-- =============================================
--declare @st int,@msg nvarchar(100), @t int
--exec [USP_CT_GetVehicles] null,0,10, 1, 1, @t out, @st out, @msg out
--select @st st,@msg msg, @t take
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_GetVehicles]
(
@SearchText nvarchar(150) = NULL,
@Skip int,
@Take int,
@UserID bigint,
@RoleID int,
@Total int out,
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
			Declare @SQLQuery nvarchar(1000);
			Declare @CountQuery nvarchar(1000);
			SET  @Status = 1;
			SET  @Total = 0;

			set @SQLQuery = 'Select * from 
			(Select v.ID,v.VehicleName,v.StockID,v.Description,v.IsDealClosed,vi.ImageName,
			vd.Make,vd.Model,vd.Variant,vd.YearOfManufacturing,vd.Kilometers,vd.Transmission,vd.RegistrationNo,fl.Type,
			dt.IsRCavailable,dt.Hypothication,dt.IsNOCavailable,dt.NoOfOwners,dt.NoOfKeys,dt.IsInsuranceAvailable,dt.IsComprehensive,
			dt.IsThirdParty,dt.InsuranceExpiryDate,
			n.VehicleID as NotificationVID,ROW_NUMBER() over(partition by v.id order by v.id) as rowno
			from CT_TRAN_Vehicle v
			left outer join CT_TRAN_VehicleDetail vd on v.ID = vd.VehicleID
			left outer join CT_TRAN_VehicleImage vi on v.ID = vi.VehicleID
			left outer join CT_TRAN_Notification n on v.ID = n.VehicleID
			left outer join CT_SYS_FuelType fl on vd.FuelTypeID = fl.ID
			left outer join CT_TRAN_DocumentDetail dt on v.ID = dt.VehicleID
			where v.IsActive = 1 and v.IsDelete = 0) a
			where a.rowno = 1 ';

			if (@SearchText is not null and @SearchText != '')
			begin
				set @SQLQuery += ' and VehicleName like ''%' + @SearchText + '%''';
			end
			
			set @CountQuery = REPLACE(@SQLQuery,'*','@Total = COUNT(*)')
			EXECUTE sp_executesql @CountQuery, @Params = N'@Total INT OUTPUT', @Total = @Total OUTPUT

			set @SQLQuery += ' ORDER BY ID OFFSET '+ CONVERT(varchar(10), @Skip) +' ROWS FETCH NEXT '+ CONVERT(varchar(10), @Take) +' ROWS ONLY;';
			EXECUTE sp_executesql @SQLQuery
			set @Message = dbo.UDF_CT_SuccessMessage('')
		end--if
		else
		begin
			set @Status = 0;
			SET  @Total = 0;
			set @Message = dbo.UDF_CT_SuccessMessage('denied')
		end
	END TRY	
	BEGIN CATCH
		SET  @Status = 0; 
		SET  @Total = 0;  
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH  
END