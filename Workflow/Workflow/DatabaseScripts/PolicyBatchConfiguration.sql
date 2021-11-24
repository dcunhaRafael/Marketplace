/****** Object:  Table [dbo].[PolicyBatchConfiguration]    Script Date: 05/10/2021 12:57:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PolicyBatchConfiguration](
	[PolicyBatchConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[BatchType] [int] NOT NULL,
	[GroupingType] [int] NOT NULL,
	[ProcessDays] [int] NOT NULL,
	[CompulsoryIssueDays] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_PolicyBatchConfiguration] PRIMARY KEY CLUSTERED 
(
	[PolicyBatchConfigurationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


