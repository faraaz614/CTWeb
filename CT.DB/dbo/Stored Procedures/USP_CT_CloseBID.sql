--declare @st int,@msg nvarchar(100)
--exec USP_CT_CloseBID 0,@st out,@msg out
--select @st st,@msg msg
CREATE PROCEDURE [dbo].[USP_CT_CloseBID]
(
@BidID bigint,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY 
		DECLARE @VehicleID bigint;
		DECLARE @DealerID bigint;
		SET  @Status = 1;
		SET  @VehicleID = 0;
		SET  @DealerID = 0;

		if (@BidID > 0)
		begin
			select @VehicleID = VehicleID, @DealerID = DealerID from CT_TRAN_VehicleBID where ID = @BidID and IsActive = 1;
			if (select IsDealClosed from CT_TRAN_Vehicle where ID = @VehicleID) = 0
			begin
				UPDATE CT_TRAN_Vehicle SET IsDealClosed = 1, BidID = @BidID, ModifiedOn = GETDATE() WHERE ID= @VehicleID;
				select u.UserID, u.Token as token, (case when bd.DealerID = @DealerID then 1 else 0 end) as IsActive 
				from CT_TRAN_VehicleBID bd 
				left outer join CT_TRAN_UserToken u on bd.DealerID = u.UserID
				where bd.VehicleID = @VehicleID and bd.IsActive = 1 
				group by u.UserID,u.Token,bd.DealerID;
			end
			SET @Message = 'Vehicle Deal closed successfully';
		end
		else
		begin
			create table #tempBIDClose (ID int, token varchar(max), VehicleID bigint, IsActive bit)
			select * into #tempVehicle 
			from CT_TRAN_Vehicle 
			where IsActive = 1 and IsDelete = 0 and IsDealClosed = 0 and GETDATE() > BidTime

			DECLARE db_cursor CURSOR FOR 
			SELECT ID FROM #tempVehicle

			OPEN db_cursor  
			FETCH NEXT FROM db_cursor INTO @VehicleID

			WHILE @@FETCH_STATUS = 0  
			BEGIN  
				select top 1 @BidID = ID, @VehicleID = VehicleID, @DealerID = DealerID 
				from CT_TRAN_VehicleBID 
				where VehicleID = @VehicleID order by BIDAmount desc
				if @BidID > 0 and @DealerID > 0 and ((select IsDealClosed from CT_TRAN_Vehicle where ID = @VehicleID) = 0)
				begin
					UPDATE CT_TRAN_Vehicle SET IsDealClosed = 1, BidID = @BidID, ModifiedOn = GETDATE() WHERE ID= @VehicleID;
					insert into #tempBIDClose (ID, token, VehicleID, IsActive) 
					select u.UserID, u.Token, bd.VehicleID, (case when bd.DealerID = @DealerID then 1 else 0 end) 
					from CT_TRAN_VehicleBID bd 
					left outer join CT_TRAN_UserToken u on bd.DealerID = u.UserID
					where bd.VehicleID = @VehicleID and bd.IsActive = 1 
					group by u.UserID,u.Token,bd.VehicleID,bd.DealerID;
				end
			FETCH NEXT FROM db_cursor INTO @VehicleID 
			END 

			CLOSE db_cursor  
			DEALLOCATE db_cursor 

			select * from #tempBIDClose
			drop table #tempVehicle;
			drop table #tempBIDClose;
			set @Message = dbo.UDF_CT_SuccessMessage('')	
		end
	END TRY	
	BEGIN CATCH
		SET  @Status = 0;  
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END