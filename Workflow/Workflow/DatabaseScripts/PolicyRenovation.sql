/****** Object:  Table [dbo].[PolicyRenovation]    Script Date: 22/12/2021 10:48:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PolicyRenovation](
	[PolicyRenovationId] [int] IDENTITY(1,1) NOT NULL,
	[PolicyBatchId] [int] NOT NULL,
	[PolicyCode] [bigint] NOT NULL,
	[EndorsementId] [int] NULL,
	[ProductExternalId] [int] NULL,
	[CoverageExternalId] [int] NULL,
	[StartOfTerm] [datetime] NULL,
	[EndOfTerm] [datetime] NULL,
	[BrokerExternalId] [int] NULL,
	[InsuredExternalId] [int] NULL,
	[TakerExternalId] [int] NULL,
	[InsuredObject] [varchar](max) NULL,
	[InsuredAmount] [decimal](18, 2) NULL,
	[RenovationUpdateIndexId] [int] NULL,
	[RenovationUpdateIndexBcCode] [int] NULL,
	[NewInsuredAmount] [decimal](18, 2) NULL,
	[NewPremiumValue] [decimal](18, 2) NULL,
	[NewStartOfTerm] [datetime] NULL,
	[NewEndOfTerm] [datetime] NULL,
	[NewProposalNumber] [int] NULL,
	[NewProposalStatusId] [int] NULL,
	[NewPolicyCode] [bigint] NULL,
	[NewInsuredObject] [varchar](max) NULL,
	[RenovationStatusId] [int] NULL,
	[ProposalId] [int] NULL,
	[LastChangeUserId] [int] NULL,
	[LastChangeDate] [datetime] NULL,
 CONSTRAINT [PK_PolicyRenovation] PRIMARY KEY CLUSTERED 
(
	[PolicyRenovationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PolicyRenovation]  WITH CHECK ADD  CONSTRAINT [FK_PolicyRenovation_PolicyBatch] FOREIGN KEY([PolicyBatchId])
REFERENCES [dbo].[PolicyBatch] ([PolicyBatchId])
GO

ALTER TABLE [dbo].[PolicyRenovation] CHECK CONSTRAINT [FK_PolicyRenovation_PolicyBatch]
GO


