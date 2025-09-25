using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talonario.Api.Server.Application.Entities.TcTama;
using Talonario.Api.Server.Application.Helpers;
using Talonario.Api.Server.Application.Interfaces.Repositories;

namespace Talonario.Api.Server.InfraStructure.Repository
{
    public class TcTamaParametrosRepository : ITcTamaParametrosRepository
    {
        #region Private Fields

        private static IConfiguration _config;
        private static SqlConnection _connection;

        #endregion Private Fields

        #region Public Constructors

        public TcTamaParametrosRepository()
        {
            _config = ConfigHelper.Load();
            _connection = new SqlConnection(_config.GetConnectionString("AtelierDataBase"));
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorMemoria>> ObterMemoria()
        {
            try
            {
                string sql = "SELECT * FROM Gen_AvaliacaoCondutorMemoria WHERE Ativo = 1";
                return await _connection.QueryAsync<TcTamaParametrosEntity.AvaliacaoCondutorMemoria>(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter memória", ex);
            }
        }

        public async Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorOrientacao>> ObterOrientacao()
        {
            try
            {
                string sql = "SELECT * FROM Gen_AvaliacaoCondutorOrientacao WHERE Ativo = 1";
                return await _connection.QueryAsync<TcTamaParametrosEntity.AvaliacaoCondutorOrientacao>(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter orientação", ex);
            }
        }

        public async Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorCapacidadeMotora>> ObterCapacidadeMotora()
        {
            try
            {
                string sql = "SELECT * FROM Gen_AvaliacaoCondutorCapacidadeMotora WHERE Ativo = 1";
                return await _connection.QueryAsync<TcTamaParametrosEntity.AvaliacaoCondutorCapacidadeMotora>(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter capacidade motora", ex);
            }
        }

        public async Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorAtitude>> ObterAtitude()
        {
            try
            {
                string sql = "SELECT * FROM Gen_AvaliacaoCondutorAtitude WHERE Ativo = 1";
                return await _connection.QueryAsync<TcTamaParametrosEntity.AvaliacaoCondutorAtitude>(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter atitude", ex);
            }
        }

        public async Task<IEnumerable<TcTamaParametrosEntity.AvaliacaoCondutorAparencia>> ObterAparencia()
        {
            try
            {
                string sql = "SELECT * FROM Gen_AvaliacaoCondutorAparencia WHERE Ativo = 1";
                return await _connection.QueryAsync<TcTamaParametrosEntity.AvaliacaoCondutorAparencia>(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter aparência", ex);
            }
        }

        public async Task<IEnumerable<TcTamaParametrosEntity.DescricaoCondutor>> ObterDescricaoCondutor()
        {
            try
            {
                string sql = "SELECT * FROM Gen_DescricaoCondutor WHERE Ativo = 1";
                return await _connection.QueryAsync<TcTamaParametrosEntity.DescricaoCondutor>(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter descrição do condutor", ex);
            }
        }

        #endregion Public Methods
    }
}