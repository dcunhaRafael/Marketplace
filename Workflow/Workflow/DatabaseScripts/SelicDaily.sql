/****** Object:  Table [dbo].[SelicDaily]    Script Date: 23/10/2021 12:30:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SelicDaily](
	[Date] [datetime] NOT NULL,
	[Value] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_SelicDaily_1] PRIMARY KEY CLUSTERED 
(
	[Date] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

