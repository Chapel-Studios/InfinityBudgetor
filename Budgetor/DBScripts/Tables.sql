USE Budgetor
GO


IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'IncomeSources' AND TABLE_SCHEMA = 'dbo')
	DROP TABLE dbo.IncomeSources

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DepositAccount' AND TABLE_SCHEMA = 'dbo')
	DROP TABLE dbo.DepositAccount

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Transactions' AND TABLE_SCHEMA = 'dbo')
	DROP TABLE dbo.Transactions

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Accounts' AND TABLE_SCHEMA = 'dbo')
	DROP TABLE dbo.Accounts

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Schedules' AND TABLE_SCHEMA = 'dbo')
	DROP TABLE dbo.Schedules

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ScheduleFrequencyTypes' AND TABLE_SCHEMA = 'dbo')
	DROP TABLE dbo.ScheduleFrequencyTypes

	

CREATE TABLE dbo.ScheduleFrequencyTypes (
	FrequencyType NVARCHAR(10) NOT NULL,
	FrequencyName NVARCHAR(50) NOT NULL,
	PRIMARY KEY NONCLUSTERED (FrequencyType),
)
ALTER TABLE [dbo].[ScheduleFrequencyTypes] WITH CHECK ADD CHECK ((NOT [FrequencyType] LIKE '% %'))

CREATE TABLE dbo.Schedules (
	LocalId INT NOT NULL IDENTITY(1,1),
	PRIMARY KEY NONCLUSTERED (LocalId),
	Frequency NVARCHAR(10) NOT NULL,
	FOREIGN KEY (Frequency) REFERENCES dbo.ScheduleFrequencyTypes(FrequencyType),
	Occurrence_First DATETIME NOT NULL,
	Occurrence_LastConfirmed DATETIME NULL,
	Occurrence_LastPlanned DATETIME NULL,
	Occurrence_Final DATETIME NULL,
    HasCustomTransactionTime BIT NOT NULL,
    IsAutoConfirm BIT NOT NULL,
	DateTime_Created DATETIME DEFAULT getDate() NOT NULL,
	DateTime_Deactivated DATETIME NULL,

)

CREATE TABLE dbo.Accounts (
	LocalId INT NOT NULL IDENTITY(1,1),
	PRIMARY KEY NONCLUSTERED (LocalId),
	AccountName NVARCHAR(50) NOT NULL,
	AccountType NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(250) NULL,
	DateTime_Created DATETIME DEFAULT getDate() NOT NULL,
	DateTime_Deactivated DATETIME NULL,
	IsSystem BIT DEFAULT 0 NOT NULL
)

CREATE TABLE dbo.Transactions (
	LocalId INT NOT NULL IDENTITY(1,1),
	PRIMARY KEY NONCLUSTERED (LocalId),
	Title NVARCHAR(50) NOT NULL,
	Amount MONEY DEFAULT 0 NOT NULL,
	ToAccount INT NULL,
	FOREIGN KEY (ToAccount) REFERENCES dbo.Accounts(LocalId),
	FromAccount INT NOT NULL,
	FOREIGN KEY (FromAccount) REFERENCES dbo.Accounts(LocalId),
	DateTime_Created DATETIME DEFAULT getDate() NOT NULL,
	DateTime_Occurred DATETIME NOT NULL,
	TransactionType NVARCHAR(15) NOT NULL,
	IsUserCreated BIT NOT NULL,
	IsConfirmed BIT NOT NULL,
	OccerrenceAccount INT NULL,
	FOREIGN KEY (OccerrenceAccount) REFERENCES dbo.Accounts(LocalId),
	Notes NVARCHAR(MAX) NULL
)
ALTER TABLE [dbo].[Transactions] WITH CHECK ADD CHECK ([ToAccount] <> [FromAccount])

CREATE TABLE dbo.DepositAccount (
	LocalId INT NOT NULL IDENTITY(1,1),
	PRIMARY KEY NONCLUSTERED (LocalId),
	AccountId INT NOT NULL,
	FOREIGN KEY (AccountId) REFERENCES dbo.Accounts(LocalId),
	IsDefault BIT NOT NULL,
	IsActiveCashAccount BIT NOT NULL,
	Balance MONEY DEFAULT 0 NOT NULL,
	InitialDepositId INT NULL,
	FOREIGN KEY (InitialDepositId) REFERENCES dbo.Transactions(LocalId),
)

CREATE TABLE dbo.IncomeSources (
	LocalId INT NOT NULL IDENTITY(1,1),
	PRIMARY KEY NONCLUSTERED (LocalId),
	AccountId INT NOT NULL,
	FOREIGN KEY (AccountId) REFERENCES dbo.Accounts(LocalId),
	ExpectedAmount MONEY DEFAULT 0 NOT NULL,
	TotalFromSource MONEY DEFAULT 0 NOT NULL,
	DefaultToAccountId INT NULL,
	FOREIGN KEY (DefaultToAccountId) REFERENCES dbo.DepositAccount(LocalId),
	ScheduleId INT NULL,
	FOREIGN KEY (ScheduleId) REFERENCES dbo.Schedules(LocalId),
)

GO