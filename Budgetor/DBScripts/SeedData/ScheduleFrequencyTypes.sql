USE Budgetor
GO

INSERT INTO [dbo].Schedules_FrequencyTypes (FrequencyName, FrequencyType)
VALUES ('Infrequent','Infrequent')
, ('Weekly','Weekly')
, ('BiWeekly','BiWeekly')
, ('Monthly','Monthly')
, ('Quarterly','Quarterly')
, ('Yearly','Yearly')
, ('Custom','Custom')

GO