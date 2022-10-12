using FluentValidation;

namespace ESESMT.Infra.Shared.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength = 11)
        {
            return ruleBuilder
                .NotEmpty()
                .MaximumLength(maximumLength)
                .Must(IsValidCpf).WithMessage("This '{PropertyName}' is invalid");
        }

        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength = 20, int minimumLength = 6)
        {
            return ruleBuilder
                .NotEmpty()
                .MinimumLength(minimumLength)
                .MaximumLength(maximumLength)
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("This '{PropertyName}' is invalid");
        }

        #region Private Methods

        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            int[] multiplierOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplierTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string aux;
            string digit;
            int sum, rest;

            var value = cpf.Trim();
            value = cpf.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
            {
                return false;
            }

            aux = value.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierOne[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            aux = aux + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierTwo[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            if (!value.EndsWith(digit))
                return false;

            return true;
        }

        #endregion
    }
}