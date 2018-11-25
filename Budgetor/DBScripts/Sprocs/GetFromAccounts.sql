USE Budgetor
GO

DROP PROCEDURE IF EXISTS [dbo].[GetFromAccounts]
GO

CREATE PROCEDURE [dbo].[GetFromAccounts]
(
	@typeList NVARCHAR(500)  = 'BankAccount,IncomeSource'
)
AS BEGIN
	SET NOCOUNT ON

	DECLARE @TempList TABLE
	(
		AccountType NVARCHAR(20)
	)

	DECLARE @AccountType NVARCHAR(20), @pos INT

	SET @typeList = LTRIM(RTRIM(@typeList))+ ','
	SET @pos = CHARINDEX(',', @typeList, 1)
	
	IF REPLACE(@typeList, ',', '') <> ''
	BEGIN
		WHILE @pos > 0
		BEGIN
			SET @AccountType = LTRIM(RTRIM(LEFT(@typeList, @pos -1)))
			IF @AccountType <> ''
			BEGIN
				INSERT INTO @TempList(AccountType) VALUES (@AccountType)
			END
			SET @typeList = RIGHT(@typeList, LEN(@typeList) - @pos)
			SET @pos = CHARINDEX(',', @typeList, 1)
		END
	END

	SELECT 
		a.LocalId AS AccountId
		, a.AccountName AS AccountName
		, CAST(CASE
			WHEN d.LocalId IS NOT NULL
			THEN d.IsDefault
			ELSE 0
			END AS BIT
		) AS IsDefault
	FROM 
		Accounts a
		LEFT JOIN [dbo].DepositAccount d ON a.LocalId = d.AccountId
		JOIN @TempList t ON a.AccountType = t.AccountType
	WHERE
		a.DateTime_Deactivated IS NULL
		AND (a.IsSystem = 0 OR (a.IsSystem = 1 AND a.AccountType = 'IncomeSource'))

	--SET @SQL = 
	--	'SELECT 
	--		a.LocalId AS AccountId
	--		, a.AccountName AS AccountName
	--		, COALESCE(i.IsDefault, d.IsDefault) as IsDefault
	--	FROM 
	--		Accounts a
	--		LEFT JOIN [dbo].[IncomeSources] i on a.LocalId = i.AccountId
	--		LEFT JOIN [dbo].DepositAccount d on a.LocalId = d.AccountId
	--	WHERE
	--		a.DateTime_Deactivated IS NULL
	--		AND a.AccountType in (
	--			' + @typeList + '
	--		)
	--		AND (a.IsSystem = 0 OR (a.IsSystem = 1 AND a.AccountType = ''IncomeSource''))'
	--EXEC(@SQL)
END
GO