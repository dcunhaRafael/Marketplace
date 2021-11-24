/****** Object:  Table [dbo].[ProposalOccurrenceDocument]    Script Date: 09/08/2021 15:05:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProposalOccurrenceDocument](
	[ProposalOccurrenceDocumentId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProposalOccurrenceId] [bigint] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[FileContents] [image] NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_ProposalOccurrenceDocument] PRIMARY KEY CLUSTERED 
(
	[ProposalOccurrenceDocumentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProposalOccurrenceDocument]  WITH CHECK ADD  CONSTRAINT [FK_ProposalOccurrenceDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO

ALTER TABLE [dbo].[ProposalOccurrenceDocument] CHECK CONSTRAINT [FK_ProposalOccurrenceDocument_DocumentType]
GO

ALTER TABLE [dbo].[ProposalOccurrenceDocument]  WITH CHECK ADD  CONSTRAINT [FK_ProposalOccurrenceDocument_ProposalOccurrence] FOREIGN KEY([ProposalOccurrenceId])
REFERENCES [dbo].[ProposalOccurrence] ([ProposalOccurrenceId])
GO

ALTER TABLE [dbo].[ProposalOccurrenceDocument] CHECK CONSTRAINT [FK_ProposalOccurrenceDocument_ProposalOccurrence]
GO


