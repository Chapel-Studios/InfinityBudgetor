USE Budgetor
GO

DROP PROCEDURE IF EXISTS [dbo].[GetIncomeSource]
GO

CREATE PROCEDURE [dbo].[GetIncomeSource]
(
	@accountId int
)
AS
BEGIN
	SELECT 
        i.LocalId AS IncomeSourceId
        , a.LocalId as AccountId
        , a.AccountName
        , a.Notes
        , a.DateTime_Created
        , a.DateTime_Deactivated
        , a.IsSystem
        , i.DefaultToAccountId
        , i.ExpectedAmount
        , i.ScheduleId
        , i.TotalFromSource

    FROM
        [DBO].IncomeSources i
        LEFT JOIN [dbo].Accounts a ON i.AccountId = a.LocalId

    WHERE
        a.LocalId = @accountId
END
GO