USE [dbDetranNetAtelier]
GO

/****** Object:  Table [dbo].[Inf_PessoaAbordada]    Script Date: 15/12/2023 08:52:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inf_PessoaAbordada](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPessoa] [varchar](50) NULL,
	[JSON] [varchar](max) NULL,
 CONSTRAINT [PK_Inf_PessoaAbordada] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


