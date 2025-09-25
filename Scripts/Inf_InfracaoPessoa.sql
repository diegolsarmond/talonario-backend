USE [dbDetranNetAtelier]
GO

/****** Object:  Table [dbo].[Inf_InfracaoPessoa]    Script Date: 26/12/2023 09:52:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inf_InfracaoPessoa](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroAuto] [varchar](50) NULL,
	[Nome] [varchar](100) NULL,
	[CPF] [varchar](100) NULL,
	[RG] [varchar](100) NULL,
	[NomeMae] [varchar](100) NULL,
	[NomePai] [varchar](100) NULL,
	[DataNascimento] [varchar](100) NULL,
	[Genero] [varchar](100) NULL,
	[PessoaEndCep] [varchar](100) NULL,
	[PessoaEndRua] [varchar](100) NULL,
	[PessoaEndBairro] [varchar](100) NULL,
	[PessoaEndNumero] [varchar](100) NULL,
	[PessoaEndComplemento] [varchar](100) NULL,
	[PessoaEndCidade] [varchar](100) NULL,
	[PessoaEndEstado] [varchar](100) NULL,
	[LocalId] [int] NULL,
	[LocalCodigoMunicipio] [varchar](100) NULL,
	[LocalCep] [varchar](100) NULL,
	[LocalMunicipio] [varchar](100) NULL,
	[LocalComplemento] [varchar](100) NULL,
	[LocalBairro] [varchar](100) NULL,
	[LocalNumero] [varchar](100) NULL,
	[LocalEstado] [varchar](100) NULL,
	[LocalLogradouro] [varchar](100) NULL,
	[DataAbordagem] [datetime] NULL,
	[NomeAgente] [varchar](100) NULL,
	[CpfAgente] [varchar](100) NULL,
	[AbordadoPeloOrgao] [varchar](100) NULL,
	[AbordadoPeloIdOrgao] [varchar](100) NULL,
	[AbordadoPeloOrgaoCompetencia] [varchar](100) NULL,
	[IdTipoInfracao] [varchar](100) NULL,
	[ArtigoCodigo] [varchar](100) NULL,
	[ArtigoDesdobramento] [varchar](100) NULL,
	[ArtigoCompetencia] [varchar](100) NULL,
	[ArtigoNatureza] [varchar](100) NULL,
	[Artigo] [varchar](100) NULL,
	[ArtigoInfrator] [varchar](100) NULL,
	[ArtigoDescricao] [varchar](500) NULL,
	[ArtigoDescricaoCompleta] [varchar](1000) NULL,
	[ArtigoApreensaoVeiculo] [bit] NULL,
	[ArtigoSuspenderCNH] [bit] NULL,
	[ArtigoApresentaCondutor] [bit] NULL,
	[ArtigoRemoverVeiculo] [bit] NULL,
	[ArtigoConfiscatePlates] [bit] NULL,
	[ArtigoCargaExcessiva] [bit] NULL,
	[ArtigoRequerDocumento] [bit] NULL,
	[ArtigoRequerEquipamento] [bit] NULL,
	[ArtigoEmVigencia] [bit] NULL,
	[ArtigoInicioVigencia] [datetime] NULL,
	[ArtigoFinalVigencia] [datetime] NULL,
	[ArtigoPontos] [int] NULL,
	[ArtigoEquipamento] [int] NULL,
	[ArtigoValorMulta] [money] NULL,
 CONSTRAINT [PK_Inf_InfracaoPessoa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


