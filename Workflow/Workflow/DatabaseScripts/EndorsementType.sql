/****** Object:  Table [dbo].[EndorsementType]    Script Date: 22/12/2021 22:03:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EndorsementType](
	[EndorsementTypeId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](1000) NULL,
	[LegacyCode] [varchar](100) NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_EndorsementType] PRIMARY KEY CLUSTERED 
(
	[EndorsementTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


