-- =============================================
--declare @st int,@msg nvarchar(100)
--exec [USP_CT_DashBoard] 1, 1,@st out,@msg out
--select @st st,@msg msg
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_DashBoard]
(
@UserID bigint,
@RoleID int,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;
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
		
		insert into #tempDashboard(c1,c2,c3,c4,c5,rowtype) values 
		(
		(Select SUM(BIDS) from (select VehicleID,COUNT(VehicleID) as BIDS 
		from CT_TRAN_VehicleBID
		group by VehicleID) a),
		(Select COUNT(VehicleID) from (select VehicleID,COUNT(VehicleID) as BIDS 
		from CT_TRAN_VehicleBID
		group by VehicleID) a),
		(select SUM(b.BIDAmount)
		from CT_TRAN_Vehicle v
		join CT_TRAN_VehicleBID b on v.ID = b.VehicleID
		where IsDealClosed = 1),
		(select COUNT(ID) as DealsClosed from CT_TRAN_Vehicle where IsDealClosed = 1),
		(select COUNT(ID) as Dealers from CT_TRAN_User),
		'first'
		)
		
		insert into #tempDashboard(c1,c2,c3,c4,c5,rowtype) 
		select top 10 FirstName+' '+LastName, Mobile, NULL,NULL,NULL,'dealer' 
		from CT_TRAN_User order by CreatedOn desc

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