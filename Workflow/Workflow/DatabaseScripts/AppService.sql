/****** Object:  Table [dbo].[AppService]    Script Date: 23/10/2021 12:29:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AppService](
	[AppServiceId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Timeout] [int] NOT NULL,
	[KeepAlive] [datetime] NULL,
	[ExecutionStatus] [int] NULL,
	[ExecutionDate] [datetime] NULL,
	[ExecutionMessage] [varchar](1000) NULL,
	[ExecutionData] [varchar](8000) NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_AppService] PRIMARY KEY CLUSTERED 
(
	[AppServiceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, FILLFACTOR = 100, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


