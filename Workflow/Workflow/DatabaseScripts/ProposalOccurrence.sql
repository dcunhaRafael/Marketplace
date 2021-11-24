/****** Object:  Table [dbo].[ProposalOccurrence]    Script Date: 09/08/2021 15:05:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProposalOccurrence](
	[ProposalOccurrenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProposalId] [int] NOT NULL,
	[OccurrenceTypeId] [int] NOT NULL,
	[OccurrenceStatus] [int] NOT NULL,
	[ApprovalRefusalDate] [datetime] NULL,
	[RefusalReasonId] [int] NULL,
	[UserComments] [varchar](max) NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_ProposalOccurrence] PRIMARY KEY CLUSTERED 
(
	[ProposalOccurrenceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProposalOccurrence]  WITH CHECK ADD  CONSTRAINT [FK_ProposalOccurrence_OccurrenceType] FOREIGN KEY([OccurrenceTypeId])
REFERENCES [dbo].[OccurrenceType] ([OccurrenceTypeId])
GO

ALTER TABLE [dbo].[ProposalOccurrence] CHECK CONSTRAINT [FK_ProposalOccurrence_OccurrenceType]
GO

ALTER TABLE [dbo].[ProposalOccurrence]  WITH CHECK ADD  CONSTRAINT [FK_ProposalOccurrence_Proposal] FOREIGN KEY([ProposalId])
REFERENCES [dbo].[Proposal] ([Id])
GO

ALTER TABLE [dbo].[ProposalOccurrence] CHECK CONSTRAINT [FK_ProposalOccurrence_Proposal]
GO

ALTER TABLE [dbo].[ProposalOccurrence]  WITH CHECK ADD  CONSTRAINT [FK_ProposalOccurrence_RefusalReason] FOREIGN KEY([RefusalReasonId])
REFERENCES [dbo].[RefusalReason] ([RefusalReasonId])
GO

ALTER TABLE [dbo].[ProposalOccurrence] CHECK CONSTRAINT [FK_ProposalOccurrence_RefusalReason]
GO


