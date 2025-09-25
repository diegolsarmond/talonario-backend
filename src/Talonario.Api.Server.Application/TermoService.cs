using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Talonario.Api.Server.Application.Entities;
using Talonario.Api.Server.Application.Interfaces.Repositories;
using Talonario.Api.Server.Application.Interfaces.Services;
using Talonario.Api.Server.Application.ViewModels;

namespace Talonario.Api.Server.Application
{
    public class TermoService : ITermoService
    {
        private readonly ITermoRepository _repository;
        private readonly ILogger<TermoService> _logger;

        public TermoService(ITermoRepository repository, ILogger<TermoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public TermoConstatacao CadastrarTermo(TermoConstatacao termo)
        {
            if (string.IsNullOrWhiteSpace(termo.CpfCondutor))
                throw new ArgumentException("CPF do condutor é obrigatório");

            if (string.IsNullOrWhiteSpace(termo.PlacaVeiculo))
                throw new ArgumentException("Placa do veículo é obrigatória");

            try
            {
                termo.DataHoraInclusao = DateTime.Now;
                termo.CodigoVerificador = GerarCodigoVerificador();
                return _repository.CadastrarTermoConstatacao(termo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar termo");
                throw;
            }
        }

        public TermoConstatacao CadastrarTermoFromMobile(TermoConstatacaoInputDto termoInput)
        {
            try
            {
                _logger.LogInformation("Iniciando cadastro de termo a partir do mobile");

                if (string.IsNullOrWhiteSpace(termoInput.aplicadoPorMatricula))
                    throw new ArgumentException("Matrícula do agente é obrigatória");

                termoInput.aplicadoPorMatricula = Regex.Replace(termoInput.aplicadoPorMatricula, @"[^\d]", "");
                if (termoInput.aplicadoPorMatricula.Length > 8)
                    termoInput.aplicadoPorMatricula = termoInput.aplicadoPorMatricula.Substring(0, 8);

                termoInput.termoConstatacao.matriculaTestemunha1 = ValidarCpfOuMatricula(termoInput.termoConstatacao?.matriculaTestemunha1);
                termoInput.termoConstatacao.matriculaTestemunha2 = ValidarCpfOuMatricula(termoInput.termoConstatacao?.matriculaTestemunha2);

                if (string.IsNullOrWhiteSpace(termoInput.veiculoPlaca))
                    throw new ArgumentException("Placa do veículo é obrigatória");

                termoInput.veiculoPlaca = FormatadorPlacaVeicular(termoInput.veiculoPlaca);

                if (!ValidarCpf(termoInput.condutorDocumentoNumero))
                    throw new ArgumentException("CPF do condutor inválido");

                var termo = new TermoConstatacao
                {
                    DataHoraAssinou = termoInput.dataAssinatura,
                    NumeroTermoConstatacaoTalonario = termoInput.id,
                    MatriculaAssinou = termoInput.aplicadoPorMatricula,
                    NomeCondutor = termoInput.condutorNome,
                    CpfCondutor = termoInput.condutorDocumentoNumero,
                    RgCondutor = termoInput.condutorDocumentoNumero,
                    PlacaVeiculo = termoInput.veiculoPlaca,
                    Situacao = "ASSINADO",
                    CepCondutor = termoInput.localCEP,
                    EnderecoCondutor = termoInput.localRua,
                    MunicipioUfCondutor = $"{termoInput.localCidade}/{termoInput.localEstado}",
                    PaisVeiculo = termoInput.veiculoPais,
                    MunicipioVeiculo = termoInput.veiculoEmplCidade,
                    UfVeiculo = termoInput.veiculoEmplEstado,
                    MarcaModeloVeiculo = termoInput.veiculoModelo,
                    EspecieVeiculo = termoInput.veiculoEspecie,
                    CorVeiculo = termoInput.veiculoCor,
                    CepLocalInfracao = termoInput.localCEP,
                    EnderecoLocalInfracao = termoInput.localRua,
                    MunicipioUfLocalInfracao = $"{termoInput.localCidade}/{termoInput.localEstado}",
                    DataHoraLocalInfracao = DateTime.TryParse(termoInput.abordadoEm, out var data) ? data : DateTime.Now,
                    Observacoes = termoInput.observacoes,
                    //MatriculaAgente = termoInput.aplicadoPorMatricula,
                    MatriculaTestemunha1 = termoInput.termoConstatacao?.matriculaTestemunha1,
                    MatriculaTestemunha2 = termoInput.termoConstatacao?.matriculaTestemunha2,
                    UsuarioInclusao = termoInput.aplicadoPor,
                    CondicaoCondutor = termoInput.equipamentoUsado,
                    SubstanciaIdentificada = int.TryParse(termoInput.idTipoInfracao, out var substancia) ? substancia : 0,
                    TestesOferecidos = termoInput.equipamentoUsado,
                    NumeroCondutor = termoInput.localNumero,
                    BairroCondutor = null,
                    BairroLocalInfracao = termoInput.localBairro,
                    NumeroLocalInfracao = termoInput.localNumero,
                    Chassi = termoInput.veiculoChassi ?? "NÃO INFORMADO",
                    VeiculoEmplacado = "S",
                    Observacao = termoInput.observacao,
                    RelatosCondutor = new List<RelatoCondutor>(),
                    AutosInfracao = new List<AutoInfracao>
                    {
                        new AutoInfracao
                        {
                            Numero = termoInput.id,
                            Tipo = "TRAN"
                        }
                    },

                    AvaliacaoCondutor = termoInput.termoConstatacao?.avaliacoes?.Select(a => new AvaliacaoCondutor
                    {
                        Descricao = a.nome,
                        Tipo = ConverterTipoAvaliacao(a.tipo)
                    }).ToList()
                };

                return CadastrarTermo(termo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar termo do mobile");
                throw;
            }
        }

        private string ValidarCpfOuMatricula(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return null;

            valor = Regex.Replace(valor, @"[^\d]", "");

            if (valor.Length == 11)
            {
                if (!ValidarCpf(valor))
                    throw new ArgumentException("CPF da testemunha inválido");
                return valor;
            }

            return valor.Length > 8 ? valor.Substring(0, 8) : valor;
        }

        private bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = Regex.Replace(cpf, @"[^\d]", "");
            return cpf.Length == 11;
        }

        private string FormatadorPlacaVeicular(string placa)
        {
            placa = Regex.Replace(placa?.ToUpper() ?? "", @"[^A-Z0-9]", "");

            return placa.Length > 7 ? placa.Substring(0, 7) : placa;
        }

        private int ConverterTipoAvaliacao(string tipoMobile)
        {
            switch (tipoMobile.ToLower())
            {
                case "aparencia": return 2;
                case "descricao": return 1;
                case "capacidade": return 3;
                case "atitude": return 4;
                case "memoria": return 5;
                case "orientacao": return 6;
                case "influencia": return 7;
                default: return 0;
            }
        }

        private string GerarCodigoVerificador()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public TermoConstatacao ConsultarTermo(string numeroTc)
        {
            if (string.IsNullOrWhiteSpace(numeroTc))
                throw new ArgumentException("Número do termo é obrigatório");

            var termo = _repository.ConsultarTermoConstatacao(numeroTc);
            return termo ?? throw new KeyNotFoundException($"Termo {numeroTc} não encontrado");
        }

        public IEnumerable<TermoConstatacao> ListarTermos()
        {
            try
            {
                return _repository.ListarTermoConstatacao();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar termos");
                throw;
            }
        }

        public IEnumerable<TermoConstatacao> PesquisarTermos(string pesquisa)
        {
            if (string.IsNullOrWhiteSpace(pesquisa) || pesquisa.Length < 3)
                throw new ArgumentException("Termo de pesquisa deve ter pelo menos 3 caracteres");

            try
            {
                return _repository.PesquisarTermoConstatacao(pesquisa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao pesquisar termos");
                throw;
            }
        }

        public bool AssinarTermo(string numeroTc, string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("Matrícula é obrigatória");

            var termo = ConsultarTermo(numeroTc);
            termo.DataHoraAssinou = DateTime.Now;
            termo.MatriculaAssinou = matricula;

            try
            {
                return _repository.AssinarTermoConstatacao(termo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao assinar termo {numeroTc}");
                throw;
            }
        }

        public bool AdicionarObservacao(string numeroTc, string observacao)
        {
            if (string.IsNullOrWhiteSpace(observacao))
                throw new ArgumentException("Observação não pode ser vazia");

            var termo = ConsultarTermo(numeroTc);
            termo.ObservacoesAdicionais = observacao;

            try
            {
                return _repository.AdicionarObservacaoTermo(termo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar observação ao termo {numeroTc}");
                throw;
            }
        }

        public TermoConstatacao EditarTermo(TermoConstatacao termo)
        {
            if (termo?.Id == null)
                throw new ArgumentException("Termo inválido para edição");

            try
            {
                return _repository.EditarTermoConstatacao(termo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao editar termo {termo.Id}");
                throw;
            }
        }

        public bool ExcluirTermo(string numeroTc)
        {
            var termo = ConsultarTermo(numeroTc);

            try
            {
                return _repository.ExcluirTermoConstatacao(numeroTc);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir termo {numeroTc}");
                throw;
            }
        }

        public bool CancelarTermo(string numeroTc, string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("Matrícula é obrigatória");

            var termo = ConsultarTermo(numeroTc);
            termo.DataHoraCancelou = DateTime.Now;
            termo.MatriculaCancelou = matricula;

            try
            {
                return _repository.CancelarTermoConstatacao(termo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao cancelar termo {numeroTc}");
                throw;
            }
        }

        public int UploadComprovante(int idTermo, int comprovante)
        {
            if (idTermo <= 0)
                throw new ArgumentException("ID do termo inválido");

            try
            {
                return _repository.UploadComprovanteTermo(idTermo, comprovante);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao upload comprovante para termo {idTermo}");
                throw;
            }
        }

        public bool RemoverComprovante(int idTermo)
        {
            if (idTermo <= 0)
                throw new ArgumentException("ID do termo inválido");

            try
            {
                return _repository.RemoverComprovanteTermo(idTermo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover comprovante do termo {idTermo}");
                throw;
            }
        }

        public TermoConstatacao ObterComprovante(int idTermo)
        {
            if (idTermo <= 0)
                throw new ArgumentException("ID do termo inválido");

            try
            {
                return _repository.ObterComprovanteTermo(idTermo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter comprovante do termo {idTermo}");
                throw;
            }
        }
    }
}