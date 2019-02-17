
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
		Declare @SQLQuery nvarchar(1000);
		Declare @CountQuery nvarchar(1000);
		SET  @Status = 1;
		SET  @Total = 0;

		set @SQLQuery = 'Select a.* from 
						(Select v.ID,v.VehicleName,v.StockID,v.Description,v.IsDealClosed,vi.ImageName,vd.Model,ROW_NUMBER() over(partition by v.id order by v.id) as rowno
						from CT_TRAN_Vehicle v
						left outer join CT_TRAN_VehicleDetail vd on v.ID = vd.VehicleID
						left outer join CT_TRAN_VehicleImage vi on v.ID = vi.VehicleID
						where v.IsActive = 1 and v.IsDelete = 0) a
						join CT_TRAN_VehicleBID vb on a.ID = vb.VehicleID
						where a.rowno = 1 and vb.DealerID = '+ CONVERT(varchar(10), @UserID);

		if (@SearchText is not null and @SearchText != '')
		begin
			set @SQLQuery += ' and VehicleName like ''%' + @SearchText + '%''';
		end

		set @CountQuery = REPLACE(@SQLQuery,'a.*','@Total = COUNT(*)')
		EXECUTE sp_executesql @CountQuery, @Params = N'@Total INT OUTPUT', @Total = @Total OUTPUT

		set @SQLQuery += ' ORDER BY ID OFFSET '+ CONVERT(varchar(10), @Skip) +' ROWS FETCH NEXT '+ CONVERT(varchar(10), @Take) +' ROWS ONLY;';
		EXECUTE sp_executesql @SQLQuery
		
		SET @Message = dbo.UDF_CT_SuccessMessage('');
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;  
		SET  @Total = 0; 
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END