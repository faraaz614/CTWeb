-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <21-dec-2018>
-- Description:	<Save Vechicle Image>
-- =============================================
CREATE PROCEDURE USP_CT_Insert_VehicleImage
(
@UserID bigint,
@RoleID int,
@VehicleID bigint,
@ImageName nvarchar(150),
@Status int out,
@Message nvarchar(500) out
)
AS
BEGIN
	SET NOCOUNT ON;	
	BEGIN TRANSACTION savevehicle
	BEGIN TRY 
	INSERT INTO CT_TRAN_VehicleImage
           ([ImageName]
           ,[VehicleID]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (@ImageName
           ,@VehicleID
           ,GETDATE()
           ,@UserID);
		SET  @Status = 1;   
		SET @Message = dbo.UDF_CT_SuccessMessage('insert');
	COMMIT TRANSACTION savevehicle
	END TRY
BEGIN CATCH
		ROLLBACK TRANSACTION savevehicle
		SET  @Status = 0;   
		SET  @Message = 'Generic Error: '+ ERROR_MESSAGE() ;
		EXECUTE USP_CT_GetErrorInfo;  	
END CATCH 	
END