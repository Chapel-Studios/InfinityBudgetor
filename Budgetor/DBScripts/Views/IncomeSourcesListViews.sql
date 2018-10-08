USE Budgetor
GO

IF (OBJECT_ID ('[dbo].[IncomeSourcesListViews]', 'V') IS NOT NULL)
BEGIN
	DROP VIEW [dbo].[IncomeSourcesListViews];  
END
GO 

CREATE VIEW [dbo].[IncomeSourcesListViews] AS 
SELECT 
	a.LocalId as AccountId
	, i.LocalId as IncomeSourceId
	, a.AccountName
	, a.DateTime_Created
	, a.DateTime_Deactivated
	, i.ExpectedAmount
	, i.TotalFromSource
	, f.FrequencyName as PayCycle
	, (
		SELECT TOP 1
			t.DateTime_Occurred
		FROM Transactions t
		WHERE 
			t.OccerrenceAccount = a.LocalId
			AND t.DateTime_Occurred > CURRENT_TIMESTAMP
		ORDER BY t.DateTime_Occurred ASC
	) as DateTime_NextTransaction
	, i.DefaultToAccountId
FROM IncomeSources i
	LEFT JOIN Accounts a ON a.LocalId = i.AccountId
	LEFT JOIN Schedules s on i.ScheduleId = s.LocalId
	LEFT JOIN ScheduleFrequencyTypes f on s.Frequency = f.FrequencyType
WHERE
	a.DateTime_Deactivated IS NULL
	AND a.IsSystem = 0