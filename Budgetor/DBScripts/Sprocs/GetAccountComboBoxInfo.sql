USE Budgetor
GO

DROP PROCEDURE IF EXISTS [dbo].[GetAccountComboBoxInfo]
GO

CREATE PROCEDURE [dbo].[GetAccountComboBoxInfo]
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
		) AS IsDefault,
		a.AccountType AS AccountType
	FROM 
		Accounts a
		LEFT JOIN [dbo].DepositAccount d ON a.LocalId = d.AccountId
		JOIN @TempList t ON a.AccountType = t.AccountType
	WHERE
		a.DateTime_Deactivated IS NULL
		AND (a.IsSystem = 0 OR (a.IsSystem = 1 AND a.AccountType = 'IncomeSource'))

END
GO
