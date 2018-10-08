USE Budgetor
GO

IF (OBJECT_ID ('[dbo].[BankAccountsListViews]', 'V') IS NOT NULL)
BEGIN
	DROP VIEW [dbo].[BankAccountsListViews];  
END
GO 

CREATE VIEW [dbo].[BankAccountsListViews] AS 
SELECT 
	d.LocalId as DepositAccountId
	, a.LocalId as AccountId
	, a.AccountName
	, a.DateTime_Created
	, a.DateTime_Deactivated
	, d.IsDefault
	, d.IsActiveCashAccount
	, d.InitialDepositId
FROM DepositAccount d
	LEFT JOIN Accounts a ON a.LocalId = d.AccountId
WHERE
	a.DateTime_Deactivated IS NULL
	AND a.IsSystem = 0