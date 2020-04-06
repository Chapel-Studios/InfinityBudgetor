USE Budgetor
GO

DROP PROCEDURE IF EXISTS [dbo].[GetBankAccount]
GO

CREATE PROCEDURE [dbo].[GetBankAccount]
(
	@accountId int
)
AS
BEGIN
	SELECT 
		d.LocalId AS DepositAccountId
		, a.LocalId AS AccountId
		, a.AccountName
		, a.Notes
		, a.DateTime_Created
		, a.DateTime_Deactivated
		, a.IsSystem
		, d.IsDefault
		, d.IsActiveCashAccount
		, d.Balance
		, d.InitialDepositId
		, CAST(CASE
			WHEN d.InitialDepositId IS NOT NULL
			THEN t.Amount
			ELSE NULL
			END AS money
		) as InitialBalance
		, CAST(CASE
			WHEN d.InitialDepositId IS NOT NULL
			THEN t.FromAccount
			ELSE NULL
			END AS int
		) AS InitialDepositAccountId
		, t.FromAccount AS InitialDepositAccountId
	FROM
		[dbo].[DepositAccount] d
		LEFT JOIN [dbo].[Accounts] a ON d.AccountId = a.LocalId
		LEFT JOIN [dbo].[Transactions] t ON d.InitialDepositId = t.LocalId
	WHERE
		a.LocalId = @accountId
END
GO
