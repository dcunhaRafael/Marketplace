/****** Object:  Table [dbo].[EndorsementTypeDocument]    Script Date: 22/12/2021 22:15:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EndorsementTypeDocument](
	[EndorsementTypeDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[EndorsementTypeId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CoverageId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[IsRequired] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_EndorsementTypeDocument] PRIMARY KEY CLUSTERED 
(
	[EndorsementTypeDocumentId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EndorsementTypeDocument]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeDocument_Coverage] FOREIGN KEY([CoverageId])
REFERENCES [dbo].[Coverage] ([CoverageId])
GO

ALTER TABLE [dbo].[EndorsementTypeDocument] CHECK CONSTRAINT [FK_EndorsementTypeDocument_Coverage]
GO

ALTER TABLE [dbo].[EndorsementTypeDocument]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeDocument_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([DocumentTypeId])
GO

ALTER TABLE [dbo].[EndorsementTypeDocument] CHECK CONSTRAINT [FK_EndorsementTypeDocument_DocumentType]
GO

ALTER TABLE [dbo].[EndorsementTypeDocument]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeDocument_EndorsementType] FOREIGN KEY([EndorsementTypeId])
REFERENCES [dbo].[EndorsementType] ([EndorsementTypeId])
GO

ALTER TABLE [dbo].[EndorsementTypeDocument] CHECK CONSTRAINT [FK_EndorsementTypeDocument_EndorsementType]
GO

ALTER TABLE [dbo].[EndorsementTypeDocument]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeDocument_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[EndorsementTypeDocument] CHECK CONSTRAINT [FK_EndorsementTypeDocument_Product]
GO


