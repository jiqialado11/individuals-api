USE [IndividualsDB]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE   PROCEDURE [dbo].[spPaging]
@TableName VARCHAR(32) ,
@PageNumber INT ,
@PageSize INT ,
@OrderBy VARCHAR(255)
AS
BEGIN
   SET NOCOUNT ON 

	DECLARE @SqlCommand NVARCHAR(MAX) 
	SET @SqlCommand =  'SELECT *, (SELECT COUNT(*) FROM ' + @TableName + ') AS TotalRowCount FROM ' + @TableName 	
	SET @SqlCommand += ' ORDER BY ' + @OrderBy 
	SET @SqlCommand += ' OFFSET ' + CONVERT(VARCHAR,@PageSize) + '*' + CONVERT(VARCHAR, @PageNumber - 1) + ' ROWS '
	SET @SqlCommand += ' FETCH NEXT ' + CONVERT(VARCHAR,@PageSize) + ' ROWS ONLY'

	--SELECT @SqlCommand

	EXECUTE (@SqlCommand)

END
GO
