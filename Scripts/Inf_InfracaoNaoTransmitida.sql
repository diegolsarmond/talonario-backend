USE [dbDetranNetAtelier]
GO

/****** Object:  Table [dbo].[Inf_InfracaoNaoTransmitida]    Script Date: 12/12/2023 15:40:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inf_InfracaoNaoTransmitida](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AIT] [varchar](50) NULL,
	[JSON] [varchar](max) NULL,
	[Tipo] [varchar](50) NULL,
	[DataCancelamento] [datetime] NULL,
	[DataEnviado] [datetime] NULL,
 CONSTRAINT [PK_Inf_InfracaoNaoAssinada] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


