USE Tactify;

CREATE TABLE [dbo].[EventStore] (
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AggregateId] [nvarchar](100) NOT NULL,
	[Aggregate] [nvarchar](100) NOT NULL,
	[Version] [int] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Payload] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE TABLE [dbo].[BoardReadModel] (
	[BoardId] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[Sequence] [bigint] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[SprintReadModel] (
	[BoardId] [nvarchar](50) NOT NULL,
	[SprintId] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[StartedAt] [datetime2](7) NULL,
	[EndedAt] [datetime2](7) NULL,
	[Sequence] [bigint] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[ActivityReadModel] (
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Sequence] [bigint] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[TicketReadModel] (
	[TicketId] [nvarchar](50) NOT NULL,
	[BoardId] [nvarchar](50) NOT NULL,
	[SprintId] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Assignee] [nvarchar](100) NULL,
	[Estimation] [int] NULL,
	[IsClosed] [bit] NOT NULL,
	[Sequence] [bigint] NOT NULL
) ON [PRIMARY];

CREATE TABLE [dbo].[TicketNumberLookup] (
	TicketNumber INT IDENTITY(1, 1) NOT NULL
) ON [PRIMARY];

CREATE CLUSTERED INDEX [SequenceIndex] ON [dbo].[EventStore] ([Sequence] ASC) WITH (
	PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF, 
	SORT_IN_TEMPDB = OFF, 
	DROP_EXISTING = OFF, 
	ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON,
	ALLOW_PAGE_LOCKS = ON,
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE UNIQUE NONCLUSTERED INDEX [ConcurrencyCheckIndex] ON [dbo].[EventStore] ([Version] ASC, [AggregateId] ASC) WITH (
	PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF, 
	SORT_IN_TEMPDB = OFF, 
	IGNORE_DUP_KEY = OFF, 
	DROP_EXISTING = OFF, 
	ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [AggregateIdIndex] ON [dbo].[EventStore] ([AggregateId] ASC) WITH (
	PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF, 
	SORT_IN_TEMPDB = OFF, 
	DROP_EXISTING = OFF,
	ONLINE = OFF,
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE CLUSTERED INDEX [BoardIdIndex] ON [dbo].[BoardReadModel] ([BoardId] ASC) WITH (
	PAD_INDEX = OFF,
	STATISTICS_NORECOMPUTE = OFF, 
	SORT_IN_TEMPDB = OFF,
	DROP_EXISTING = OFF,
	ONLINE = OFF,
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON, 
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE CLUSTERED INDEX [BoardIdSprintIdIndex] ON [dbo].[SprintReadModel] ([BoardId] ASC, [SprintId] ASC) WITH (
	PAD_INDEX = OFF, 
	STATISTICS_NORECOMPUTE = OFF,
	SORT_IN_TEMPDB = OFF,
	DROP_EXISTING = OFF, 
	ONLINE = OFF,
	ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON,
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE CLUSTERED INDEX [SequenceIndex] ON [dbo].[ActivityReadModel] ([Sequence] ASC) WITH (
	PAD_INDEX = OFF,
	STATISTICS_NORECOMPUTE = OFF,
	SORT_IN_TEMPDB = OFF, 
	DROP_EXISTING = OFF, 
	ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON,
	ALLOW_PAGE_LOCKS = ON,
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE NONCLUSTERED INDEX [TicketIdIndex] ON [dbo].[TicketReadModel] ([TicketId] ASC) WITH (
	PAD_INDEX = OFF,
	STATISTICS_NORECOMPUTE = OFF,
	SORT_IN_TEMPDB = OFF, 
	DROP_EXISTING = OFF, 
	ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON,
	ALLOW_PAGE_LOCKS = ON,
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];

CREATE CLUSTERED INDEX [SprintIdBoardIdIndex] ON [dbo].[TicketReadModel] ([SprintId] ASC, [BoardId] ASC) WITH (
	PAD_INDEX = OFF,
	STATISTICS_NORECOMPUTE = OFF,
	SORT_IN_TEMPDB = OFF, 
	DROP_EXISTING = OFF, 
	ONLINE = OFF, 
	ALLOW_ROW_LOCKS = ON,
	ALLOW_PAGE_LOCKS = ON,
	OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];