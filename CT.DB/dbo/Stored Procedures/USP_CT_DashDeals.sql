-- =============================================
--declare @startdate date,@enddate date
--set @startdate = DATEADD(month, -1, GETDATE())
--set @enddate = GETDATE()
--declare @st int,@msg nvarchar(100)
--exec [USP_CT_DashDeals] 1, 1, @startdate, @enddate,@st out,@msg out
--select @st st,@msg msg
-- =============================================

CREATE PROCEDURE [dbo].[USP_CT_DashDeals]
(
@UserID int,
@RoleID int,
@StartDate date,
@EndDate date,
@Status int out,
@Message varchar(500) out
)
AS
BEGIN
	BEGIN TRY
		SET  @Status = 1;  
		
		Create table #tempDashboard
		(
		c1 varchar(100),
		c2 varchar(100),
		c3 varchar(100),
		c4 varchar(100),
		c5 varchar(100),
		rowtype varchar(10)
		)
		
		insert into #tempDashboard(c1,c2,c3,c4,c5,rowtype)  
		select u.FirstName +' '+u.LastName, u.Mobile,v.VehicleName, vb.BIDAmount,v.ModifiedOn, 'deals' 
		from CT_TRAN_Vehicle v
		left outer join CT_TRAN_VehicleBID vb on v.BidID = vb.ID
		left outer join CT_TRAN_User u on vb.DealerID = u.ID
		where v.IsDealClosed = 1 and (CONVERT(date, v.ModifiedOn) between @StartDate and @EndDate)
		order by v.ModifiedOn desc

		select * from #tempDashboard;
		drop table #tempDashboard;

		SET @Message = dbo.UDF_CT_SuccessMessage('') ;
	END TRY
	BEGIN CATCH
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH
END