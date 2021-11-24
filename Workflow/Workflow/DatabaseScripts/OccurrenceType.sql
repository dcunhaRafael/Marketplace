/****** Object:  Table [dbo].[OccurrenceType]    Script Date: 06/08/2021 15:14:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OccurrenceType](
	[OccurrenceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CoverageId] [int] NOT NULL,
	[OccurrenceCode] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](2048) NOT NULL,
	[Type] [int] NOT NULL,
	[ValidationRule] [int] NOT NULL,
	[IsTransmissionLocked] [bit] NOT NULL,
	[IsIssuanceLocked] [bit] NOT NULL,
	[RequiredAction] [int] NOT NULL,
	[AutomaticRefusal] [int] NOT NULL,
	[ProfileId] [int] NOT NULL,
	[NormalSignalingTimeout] [int] NULL,
	[WarningSignalingTimeout] [int] NULL,
	[CriticalSignalingTimeout] [int] NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_OccurrenceType] PRIMARY KEY CLUSTERED 
(
	[OccurrenceTypeId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OccurrenceType] ADD  CONSTRAINT [DF_OccurrenceType_IsTransmissionLocked]  DEFAULT ((0)) FOR [IsTransmissionLocked]
GO

ALTER TABLE [dbo].[OccurrenceType] ADD  CONSTRAINT [DF_OccurrenceType_IsIssuanceLocked]  DEFAULT ((0)) FOR [IsIssuanceLocked]
GO

ALTER TABLE [dbo].[OccurrenceType]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceType_Coverage] FOREIGN KEY([CoverageId])
REFERENCES [dbo].[Coverage] ([CoverageId])
GO

ALTER TABLE [dbo].[OccurrenceType] CHECK CONSTRAINT [FK_OccurrenceType_Coverage]
GO

ALTER TABLE [dbo].[OccurrenceType]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceType_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO

ALTER TABLE [dbo].[OccurrenceType] CHECK CONSTRAINT [FK_OccurrenceType_Product]
GO

ALTER TABLE [dbo].[OccurrenceType]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceType_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([Id])
GO

ALTER TABLE [dbo].[OccurrenceType] CHECK CONSTRAINT [FK_OccurrenceType_Profile]
GO


