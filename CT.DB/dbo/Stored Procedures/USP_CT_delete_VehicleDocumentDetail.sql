-- =============================================
-- Author:		Rafeeq Mohammad
-- Create date: <21-dec-2018>
-- Description:	<delete DocumentDetail>
-- =============================================
CREATE PROCEDURE USP_CT_delete_VehicleDocumentDetail
(
@UserID bigint,
@RoleID int,
@DocumentDetailID bigint,
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;
	BEGIN TRY		
		DELETE FROM  CT_TRAN_DocumentDetail WHERE ID = @DocumentDetailID
		SET  @Status = 1;  
		SET @Message = dbo.UDF_CT_SuccessMessage('delete') ;
	END TRY
	BEGIN CATCH     
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  
	END CATCH 
END