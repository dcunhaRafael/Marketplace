/****** Object:  Table [dbo].[EndorsementTypeInsuredObject]    Script Date: 22/12/2021 22:04:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EndorsementTypeInsuredObject](
	[EndorsementTypeInsuredObjectId] [int] IDENTITY(1,1) NOT NULL,
	[EndorsementTypeId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CoverageId] [int] NOT NULL,
	[TextId] [int] NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_EndorsementTypeInsuredObject] PRIMARY KEY CLUSTERED 
(
	[EndorsementTypeInsuredObjectId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeInsuredObject_Coverage] FOREIGN KEY([CoverageId])
REFERENCES [dbo].[Coverage] ([CoverageId])
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject] CHECK CONSTRAINT [FK_EndorsementTypeInsuredObject_Coverage]
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeInsuredObject_EndorsementType] FOREIGN KEY([EndorsementTypeId])
REFERENCES [dbo].[EndorsementType] ([EndorsementTypeId])
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject] CHECK CONSTRAINT [FK_EndorsementTypeInsuredObject_EndorsementType]
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeInsuredObject_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject] CHECK CONSTRAINT [FK_EndorsementTypeInsuredObject_Product]
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject]  WITH CHECK ADD  CONSTRAINT [FK_EndorsementTypeInsuredObject_Text] FOREIGN KEY([TextId])
REFERENCES [dbo].[Text] ([TextId])
GO

ALTER TABLE [dbo].[EndorsementTypeInsuredObject] CHECK CONSTRAINT [FK_EndorsementTypeInsuredObject_Text]
GO


