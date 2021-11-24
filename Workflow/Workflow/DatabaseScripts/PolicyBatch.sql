/****** Object:  Table [dbo].[PolicyBatch]    Script Date: 24/11/2021 19:54:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PolicyBatch](
	[PolicyBatchId] [int] IDENTITY(1,1) NOT NULL,
	[BatchType] [int] NOT NULL,
	[BrokerExternalId] [int] NULL,
	[TakerExternalId] [int] NULL,
	[InsuredExternalId] [int] NULL,
	[Competency] [varchar](10) NULL,
	[PolicyCount] [int] NOT NULL,
	[PolicyTotal] [decimal](18, 2) NOT NULL,
	[BatchStatus] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_PolicyBatch] PRIMARY KEY CLUSTERED 
(
	[PolicyBatchId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


