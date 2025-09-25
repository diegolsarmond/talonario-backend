USE [dbDetranNetAtelier]
GO

/****** Object:  Table [dbo].[Inf_TalonarioDispositivo]    Script Date: 09/11/2023 20:32:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inf_TalonarioDispositivo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDispositivo] [varchar](100) NULL,
	[Sequencia] [varchar](10) NULL,
 CONSTRAINT [PK_Inf_TalonarioDispositivo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


