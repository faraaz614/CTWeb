
--declare @st int,@msg nvarchar(100), @t int
--exec USP_CT_GetBIDSByUserID '',0,4, 1, 1,@t out,@st out,@msg out
--select @st st,@msg msg, @t take

CREATE PROCEDURE [dbo].[USP_CT_GetBIDSByUserID]
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
			Declare @SQLQuery nvarchar(max);
			Declare @CountQuery nvarchar(max);
			SET  @Status = 1;
			SET  @Total = 0;

			select VehicleID,MAX(BIDAmount) as BIDAmount into #tempBIDS from CT_TRAN_VehicleBID
			where DealerID = @UserID and IsActive = 1
			group by VehicleID

			set @SQLQuery = 'with cte as (select ROW_NUMBER() OVER(partition by CASE WHEN vi.VehicleID IS NOT NULL THEN vi.VehicleID ELSE v.ID END 
							ORDER BY vb.CreatedOn desc) as VIVID,
							v.ID,v.IsActive,v.VehicleName,v.ModifiedOn,v.StockID,v.Description,v.IsDealClosed,v.BidTime,
							(CAST(Datediff(s, GETDATE(), v.BidTime) AS BIGINT)*1000) as BidTimeMilliSecs,vi.ImageName,
							vd.Make,vd.Model,vd.Variant,vd.YearOfManufacturing,vd.Kilometers,vd.Transmission,vd.RegistrationNo,fl.Type,
							dt.IsRCavailable,dt.Hypothication,dt.IsNOCavailable,dt.NoOfOwners,dt.NoOfKeys,dt.IsInsuranceAvailable,dt.IsComprehensive,
							dt.IsThirdParty,dt.InsuranceExpiryDate,n.VehicleID as NotificationVID 
							from CT_TRAN_Vehicle v 
							left join CT_TRAN_VehicleDetail vd on v.ID = vd.VehicleID 
							left join CT_TRAN_VehicleImage vi on v.ID = vi.VehicleID 
							left outer join CT_TRAN_Notification n on v.ID = n.VehicleID 
							left outer join CT_SYS_FuelType fl on vd.FuelTypeID = fl.ID 
							left outer join CT_TRAN_DocumentDetail dt on v.ID = dt.VehicleID 
							left outer join CT_TRAN_VehicleBID vb on v.ID = vb.VehicleID 
							where v.IsDelete = 0) 
							select * from cte join #tempBIDS on cte.ID = #tempBIDS.VehicleID where VIVID = 1 and IsActive = 1 ';

			if (@SearchText is not null and @SearchText != '')
			begin
				set @SQLQuery += ' and VehicleName like ''%' + @SearchText + '%''';
			end
			
			set @CountQuery = REPLACE(@SQLQuery,' * ',' @Total = COUNT(*)')
			EXECUTE sp_executesql @CountQuery, @Params = N'@Total INT OUTPUT', @Total = @Total OUTPUT

			set @SQLQuery += ' ORDER BY ModifiedOn desc OFFSET '+ CONVERT(varchar(10), @Skip) +' ROWS FETCH NEXT '+ CONVERT(varchar(10), @Take) +' ROWS ONLY;';
			EXECUTE sp_executesql @SQLQuery
			drop table #tempBIDS
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