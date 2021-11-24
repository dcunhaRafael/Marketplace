/****** Object:  Table [dbo].[OccurrenceTypeDocument]    Script Date: 06/08/2021 15:14:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OccurrenceTypeDocument](
	[OccurrenceTypeDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[OccurrenceTypeId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[IsRequired] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_OccurrenceTypeDocument] PRIMARY KEY CLUSTERED 
(
	[OccurrenceTypeDocumentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OccurrenceTypeDocument]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceTypeDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO

ALTER TABLE [dbo].[OccurrenceTypeDocument] CHECK CONSTRAINT [FK_OccurrenceTypeDocument_DocumentType]
GO

ALTER TABLE [dbo].[OccurrenceTypeDocument]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceTypeDocument_OccurrenceType] FOREIGN KEY([OccurrenceTypeId])
REFERENCES [dbo].[OccurrenceType] ([OccurrenceTypeId])
GO

ALTER TABLE [dbo].[OccurrenceTypeDocument] CHECK CONSTRAINT [FK_OccurrenceTypeDocument_OccurrenceType]
GO


