USE Budgetor
GO

IF (OBJECT_ID ('[dbo].[BankAccountsListViews]', 'V') IS NOT NULL)
BEGIN
	DROP VIEW [dbo].[BankAccountsListViews];  
END
GO 

CREATE VIEW [dbo].[BankAccountsListViews] AS 
SELECT 
	a.LocalId
	, a.AccountName
	, a.DateTime_Created
	, a.DateTime_Deactivated
	, d.IsDefault
	, d.IsActiveCashAccount
	, d.Balance
FROM DepositAccount d
	LEFT JOIN Accounts a ON a.LocalId = d.Account
WHERE
	a.DateTime_Deactivated IS NULL