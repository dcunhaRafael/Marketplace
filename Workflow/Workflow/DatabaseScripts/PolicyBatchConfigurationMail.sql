/****** Object:  Table [dbo].[PolicyBatchConfigurationMail]    Script Date: 05/10/2021 12:57:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PolicyBatchConfigurationMail](
	[PolicyBatchConfigurationMailId] [int] IDENTITY(1,1) NOT NULL,
	[PolicyBatchConfigurationId] [int] NOT NULL,
	[DaysBeforeExpiration] [int] NOT NULL,
	[Subject] [varchar](200) NOT NULL,
	[Body] [text] NOT NULL,
	[SendToBroker] [bit] NOT NULL,
	[SendToSubscription] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_PolicyBatchConfigurationMail] PRIMARY KEY CLUSTERED 
(
	[PolicyBatchConfigurationMailId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PolicyBatchConfigurationMail]  WITH CHECK ADD  CONSTRAINT [FK_PolicyBatchConfigurationMail_PolicyBatchConfiguration] FOREIGN KEY([PolicyBatchConfigurationId])
REFERENCES [dbo].[PolicyBatchConfiguration] ([PolicyBatchConfigurationId])
GO

ALTER TABLE [dbo].[PolicyBatchConfigurationMail] CHECK CONSTRAINT [FK_PolicyBatchConfigurationMail_PolicyBatchConfiguration]
GO


