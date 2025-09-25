using System.Text.RegularExpressions;

namespace Talonario.Api.Server.Application.Extensions
{
    public static class CPFExtensions
    {
        #region Public Methods

        public static bool Has11DigitsWithoutMask(this string cpf)
        {
            string _cpf = cpf.RemoveMask();
            return _cpf.Length != 11 ? false : true;
        }

        public static bool IsValidCpf(this string cpf)
        {
            cpf = cpf.RemoveMask();

            if (!cpf.Has11DigitsWithoutMask())
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            int firstDigit = 11 - (sum % 11);
            if (firstDigit >= 10)
                firstDigit = 0;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            int secondDigit = 11 - (sum % 11);
            if (secondDigit >= 10)
                secondDigit = 0;

            return cpf[9] == char.Parse(firstDigit.ToString()) && cpf[10] == char.Parse(secondDigit.ToString());
        }

        public static string RemoveMask(this string cpf)
        {
            return Regex.Replace(cpf, @"[^\d]", "");
        }

        #endregion Public Methods
    }
}