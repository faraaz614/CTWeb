-- =============================================
-- Author:		<Shaik Mohammad Rafeeq>
-- Create date: <11/25/2018>
-- Description:	<Get Generic error info>
-- =============================================
CREATE PROCEDURE USP_CT_GetErrorInfo  
AS  
SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_SEVERITY() AS ErrorSeverity  
    ,ERROR_STATE() AS ErrorState  
    ,ERROR_PROCEDURE() AS ErrorProcedure  
    ,ERROR_LINE() AS ErrorLine  
    ,ERROR_MESSAGE() AS ErrorMessage;