-- =============================================
--declare @st int,@msg nvarchar(100), @t int
--exec USP_CT_GetUsers '',0,4, 1, 1,@t out,@st out,@msg out
--select @st st,@msg msg, @t take
-- =============================================
CREATE PROCEDURE [dbo].[USP_CT_GetUsers]
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
	SET NOCOUNT ON;
	BEGIN TRY 
		if exists(select UserName from CT_TRAN_User where ID = @UserID and RoleID = @RoleID) and (@RoleID = 1)
		begin --if
			Declare @SQLQuery nvarchar(1000);
			Declare @CountQuery nvarchar(1000);
			SET  @Status = 1;
			SET  @Total = 0;

			set @SQLQuery = 'Select ID,RoleID,FirstName,LastName,UserName,IsActive from CT_TRAN_User';
			if (@SearchText is not null and @SearchText != '')
			begin
				set @SQLQuery += ' where UserName like ''%' + @SearchText + '%''';
			end

			set @CountQuery = REPLACE(@SQLQuery,'ID,RoleID,FirstName,LastName,UserName,IsActive','@Total = COUNT(*)')
			EXECUTE sp_executesql @CountQuery, @Params = N'@Total INT OUTPUT', @Total = @Total OUTPUT

			set @SQLQuery += ' ORDER BY ID DESC OFFSET '+ CONVERT(varchar(10), @Skip) +' ROWS FETCH NEXT '+ CONVERT(varchar(10), @Take) +' ROWS ONLY;';
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