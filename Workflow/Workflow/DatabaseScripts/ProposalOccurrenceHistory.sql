/****** Object:  Table [dbo].[ProposalOccurrenceHistory]    Script Date: 09/08/2021 15:06:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProposalOccurrenceHistory](
	[ProposalOccurrenceHistoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProposalOccurrenceId] [bigint] NOT NULL,
	[ActionType] [int] NOT NULL,
	[Description] [varchar](max) NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProposalOccurrenceHistory] PRIMARY KEY CLUSTERED 
(
	[ProposalOccurrenceHistoryId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProposalOccurrenceHistory]  WITH CHECK ADD  CONSTRAINT [FK_ProposalOccurrenceHistory_ProposalOccurrence] FOREIGN KEY([ProposalOccurrenceId])
REFERENCES [dbo].[ProposalOccurrence] ([ProposalOccurrenceId])
GO

ALTER TABLE [dbo].[ProposalOccurrenceHistory] CHECK CONSTRAINT [FK_ProposalOccurrenceHistory_ProposalOccurrence]
GO


