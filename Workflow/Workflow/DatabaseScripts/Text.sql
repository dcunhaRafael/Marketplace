/****** Object:  Table [dbo].[Text]    Script Date: 22/12/2021 22:02:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Text](
	[TextId] [int] IDENTITY(1,1) NOT NULL,
	[TextType] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Title] [varchar](200) NULL,
	[Contents] [text] NULL,
	[LegacyCode] [varchar](100) NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_Text] PRIMARY KEY CLUSTERED 
(
	[TextId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


