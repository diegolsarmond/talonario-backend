USE [dbDetranNetAtelier]
GO

/****** Object:  Table [dbo].[Inf_TalonarioUsuarioLogado]    Script Date: 10/11/2023 13:48:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inf_TalonarioUsuarioLogado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cpf] [varchar](50) NULL,
	[idDispositivo] [varchar](100) NULL,
	[dataAutenticacao] [datetime] NULL,
 CONSTRAINT [PK_Inf_TalonarioUsuarioLogado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


