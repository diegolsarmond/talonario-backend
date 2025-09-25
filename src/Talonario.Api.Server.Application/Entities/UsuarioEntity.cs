using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talonario.Api.Server.Application.Entities
{
    [Table("usuarios")]
    public class UsuarioEntity
    {
        #region Public Constructors

        public UsuarioEntity()
        {
        }

        public UsuarioEntity(
            long id,
            int idEmpresa,
            long? idSetor,
            string nome,
            string cpf,
            char tipo,
            string usuario,
            string email,
            string senha,
            bool ativo,
            DateTime? dataBloqueio,
            DateTime? dataAlteracaoSenha
        )
        {
            Id = id;
            IdEmpresa = idEmpresa;
            IdSetor = idSetor;
            Nome = nome;
            CPF = cpf;
            Tipo = tipo;
            Usuario = usuario;
            Email = email;
            Senha = senha;
            Ativo = ativo;
            DataBloqueio = dataBloqueio;
            DataAlteracaoSenha = dataAlteracaoSenha;
        }

        public UsuarioEntity(
            long id,
            string usuario,
            string cpf,
            string email,
            string senha,
            bool ativo,
            string permissoes,
            string empresa
        )
        {
            Id = id;
            Usuario = usuario;
            CPF = cpf;
            Email = email;
            Senha = senha;
            Ativo = ativo;
            Permissoes = permissoes;
            Empresa = empresa;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Ativo { get; set; }

        public string Competencia { get; set; }

        public string CPF { get; set; }

        public DateTime? DataAlteracaoSenha { get; set; }

        public DateTime? DataBloqueio { get; set; }

        public string Email { get; set; }

        public string Empresa { get; set; }

        public long Id { get; set; }

        public string IdDispositivo { get; set; }

        public int IdEmpresa { get; set; }

        public long? IdSetor { get; set; }

        public int IdTalonarioDispositivo { get; set; }

        public int Matricula { get; set; }

        public int MatriculaAgente { get; set; }

        public string Nome { get; set; }

        public string Permissoes { get; set; }

        public string Senha { get; set; }

        public string Sequencia { get; set; }

        public char Tipo { get; set; }

        public string Token { get; set; }

        public string Usuario { get; set; }

        #endregion Public Properties
    }
}