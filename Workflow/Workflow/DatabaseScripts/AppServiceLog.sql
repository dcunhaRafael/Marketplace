SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AppServiceLog](
	[AppServiceLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[AppServiceId] [int] NOT NULL,
	[InclusionDate] [datetime] NOT NULL,
	[LogLevel] [int] NOT NULL,
	[Message] [text] NOT NULL,
 CONSTRAINT [PK_AppServiceLog] PRIMARY KEY CLUSTERED 
(
	[AppServiceLogId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, FILLFACTOR = 100, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AppServiceLog]  WITH NOCHECK ADD  CONSTRAINT [FK_AppServiceLog_AppService] FOREIGN KEY([AppServiceId])
REFERENCES [dbo].[AppService] ([AppServiceId])
GO

ALTER TABLE [dbo].[AppServiceLog] CHECK CONSTRAINT [FK_AppServiceLog_AppService]
GO


