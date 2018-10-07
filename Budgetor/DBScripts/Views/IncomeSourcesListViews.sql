USE Budgetor
GO

IF (OBJECT_ID ('[dbo].[IncomeSourcesListViews]', 'V') IS NOT NULL)
BEGIN
	DROP VIEW [dbo].[IncomeSourcesListViews];  
END
GO 

CREATE VIEW [dbo].[IncomeSourcesListViews] AS 
SELECT 
	a.LocalId
	, a.AccountName
	, a.DateTime_Created
	, a.DateTime_Deactivated
	, i.ExpectedAmount
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
FROM IncomeSources i
	LEFT JOIN Accounts a ON a.LocalId = i.Account
	LEFT JOIN Schedules s on i.Schedule = s.LocalId
	LEFT JOIN ScheduleFrequencyTypes f on s.Frequency = f.FrequencyType
WHERE
	a.DateTime_Deactivated IS NULL