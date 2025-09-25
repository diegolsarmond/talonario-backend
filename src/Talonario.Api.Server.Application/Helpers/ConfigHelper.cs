using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Talonario.Api.Server.Application.Helpers
{
    public static class ConfigHelper
    {
        #region Public Methods

        public static IConfiguration Load()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);

            if (configuration == null)
            {
                configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }

            return configuration.Build();
        }

        public static class CodigoVerificador
        {
            private static readonly Random _random = new Random();

            public static string Gerar(int tamanho)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, tamanho)
                    .Select(s => s[_random.Next(s.Length)]).ToArray());
            }
        }

        #endregion Public Methods
    }
}