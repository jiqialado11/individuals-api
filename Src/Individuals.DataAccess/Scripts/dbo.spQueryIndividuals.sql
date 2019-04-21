USE [IndividualsDB]
GO
/****** Object:  StoredProcedure [dbo].[spQueryIndividuals]    Script Date: 19/04/2019 11:53:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spQueryIndividuals]
@QueryString NVARCHAR(255),
@Name NVARCHAR(255),
@LastName NVARCHAR(255),
@PersonalId NVARCHAR(255),
@Gender INT,
@City NVARCHAR(255),
@BirthDate DATETIME,
@PageNumber INT = 1,
@PageSize INT = 1000,
@OrderBy VARCHAR(255)
AS
BEGIN
	
	SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED



	SET @PageNumber = IIF(ISNULL(@PageNumber, 0) = 0, 1, @PageNumber)
	SET @PageSize = IIF(ISNULL(@PageSize, 0) = 0, 100, @PageSize)

	SET @QueryString = '%' + ISNULL(@QueryString, '') + '%'
	
	
	IF(OBJECT_ID('tempdb..#Result') IS NOT NULL)
		DROP TABLE #Result
	CREATE TABLE #Result
	(
		Id INT,
		FirstName VARCHAR(32),
		LastName NVARCHAR(255),
		PersonalId NVARCHAR(11),
		Gender INT,
		City NVARCHAR(255),
		BirthDate DATETIME
	)

	;WITH _Individuals AS 
	(
		SELECT dbo.Individuals.Id,
			   FirstName,
			   LastName,
			   PersonalNumber as PersonalId,
			   Gender,
			   dbo.Cities.Name as City,
			   DateOfBirth as BirthDate
		FROM dbo.Individuals
		INNER JOIN dbo.Cities ON dbo.Individuals.CityId = dbo.Cities.Id
		WHERE  
		LOWER(FirstName) = IIF(@Name IS NOT NULL,LOWER(@Name),FirstName)
		AND LOWER(LastName) = IIF(@LastName IS NOT NULL,LOWER(@LastName),LastName)
		AND LOWER(PersonalNumber) = IIF(@PersonalId IS NOT NULL,LOWER(@PersonalId),PersonalNumber)
		AND LOWER(dbo.Cities.Name) = IIF(@City IS NOT NULL,LOWER(@City),dbo.Cities.Name)
		AND Gender = IIF(@Gender IS NOT NULL,@Gender,Gender)
		AND DateOfBirth = IIF(@BirthDate IS NOT NULL,@BirthDate,DateOfBirth)
		AND (@QueryString IS NULL OR (FirstName LIKE @QueryString OR LastName LIKE @QueryString OR PersonalNumber LIKE @QueryString)) 
	
	)
	INSERT INTO #Result
	SELECT Id,
	       FirstName,
	       LastName,
	       PersonalId,
	       Gender,
	       City,
	       BirthDate
	FROM _Individuals
	OPTION (RECOMPILE)	
	
	SET @OrderBy = IIF(ISNULL(@OrderBy,'') = '', 'Id DESC', @OrderBy)
	
	EXEC dbo.spPaging @TableName = '#Result', 
	                @PageNumber = @PageNumber, 
	                @PageSize = @PageSize,
	                @OrderBy = @OrderBy

	IF(OBJECT_ID('tempdb..#Result') IS NOT NULL)
		DROP TABLE #Result

END
