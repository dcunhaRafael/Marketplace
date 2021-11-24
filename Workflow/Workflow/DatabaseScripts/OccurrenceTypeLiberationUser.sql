/****** Object:  Table [dbo].[OccurrenceTypeLiberationUser]    Script Date: 06/08/2021 15:14:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OccurrenceTypeLiberationUser](
	[OccurrenceTypeLiberationUserId] [int] IDENTITY(1,1) NOT NULL,
	[OccurrenceTypeId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[InclusionUserId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_OccurrenceTypeLiberationUser] PRIMARY KEY CLUSTERED 
(
	[OccurrenceTypeLiberationUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OccurrenceTypeLiberationUser]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceTypeLiberationUser_OccurrenceType] FOREIGN KEY([OccurrenceTypeId])
REFERENCES [dbo].[OccurrenceType] ([OccurrenceTypeId])
GO

ALTER TABLE [dbo].[OccurrenceTypeLiberationUser] CHECK CONSTRAINT [FK_OccurrenceTypeLiberationUser_OccurrenceType]
GO

ALTER TABLE [dbo].[OccurrenceTypeLiberationUser]  WITH CHECK ADD  CONSTRAINT [FK_OccurrenceTypeLiberationUser_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[OccurrenceTypeLiberationUser] CHECK CONSTRAINT [FK_OccurrenceTypeLiberationUser_Users]
GO


