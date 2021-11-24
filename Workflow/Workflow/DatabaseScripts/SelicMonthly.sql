/****** Object:  Table [dbo].[SelicMonthly]    Script Date: 23/10/2021 12:30:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SelicMonthly](
	[Date] [datetime] NOT NULL,
	[Value] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_SelicMonthly_1] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

