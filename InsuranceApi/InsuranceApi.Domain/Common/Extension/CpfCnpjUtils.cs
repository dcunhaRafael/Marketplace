using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Validations;
using System.Text.RegularExpressions;
namespace InsuranceApi.Domain.Common.Extension {
    public static class CpfCnpjUtils {
        public static bool IsValid(string cpfCnpj) {
            return (IsCpf(Extends.ApenasNumericos(cpfCnpj)) || IsCnpj(Extends.ApenasNumericos(cpfCnpj)));
        }

        public  static bool IsCpf(string cpf) {


            cpf = cpf.PadLeft(11, '0');
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            //cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            cpf = Regex.Replace(cpf, "[^0-9]", string.Empty);
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool IsCnpj(string cnpj) {

            cnpj = cnpj.PadLeft(14, '0');
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            // cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            cnpj = Regex.Replace(cnpj, "[^0-9]", string.Empty);
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static string RetornarDocSemFormatacao(this string value) {
            if (string.IsNullOrEmpty(value))
                return value;

            else {
                return value.Replace(".", "").Replace("-", "").Replace("/", "");
            }
        }

        /// <summary>
        /// Formata o número do CPF 92074286520 para 920.742.865-20
        /// </summary>
        /// <param name="cpf">Sequencia numérica de 11 dígitos. Exemplo: 00000000000</param>
        /// <returns>CPF formatado</returns>
        public static string FormataCPF(string cpf) {
            try {
                return string.Format("{0}.{1}.{2}-{3}", cpf.Remove(cpf.Length - 8), cpf.Substring(cpf.Length - 8, 3), cpf.Substring(cpf.Length - 5, 3), cpf.Substring(cpf.Length - 2, 2));
            } catch {
                return cpf;
            }
        }
        public static string FormataCPF(this long cpf) {
            string unformatted = cpf.ToString("D11");
            return FormataCPF(unformatted);
        }

        /// <summary>
        /// Formata o CNPJ. Exemplo 00.316.449/0001-63
        /// </summary>
        /// <param name="cnpj">Sequencia numérica de 14 dígitos. Exemplo: 00000000000000</param>
        /// <returns>CNPJ formatado</returns>
        public static string FormataCNPJ(string cnpj) {
            if (!string.IsNullOrWhiteSpace(cnpj)) {
                cnpj = long.Parse(cnpj).ToString("D14");
                cnpj = string.Format("{0}.{1}.{2}/{3}-{4}", cnpj.Remove(cnpj.Length - 12), cnpj.Substring(cnpj.Length - 12, 3), cnpj.Substring(cnpj.Length - 9, 3), cnpj.Substring(cnpj.Length - 6, 4), cnpj.Substring(cnpj.Length - 2));
            }
            return cnpj;
        }
        public static string FormataCNPJ(this long cnpj) {
            string unformatted = cnpj.ToString("D14");
            return FormataCNPJ(unformatted);
        }

        /// <summary>
        /// Formata o valor informado para CPF ou CNPJ identificando o tipo da pessoa.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="tipoPessoa"></param>
        /// <returns></returns>
        public static string FormatCpfCnpj(this string value, TipoPessoaEnum tipoPessoa) {
            switch (tipoPessoa) {
                case TipoPessoaEnum.PessoaFisica:
                case TipoPessoaEnum.PFSemDomicilioPais:
                    return FormataCPF(value);
                case TipoPessoaEnum.ConsorcioEmpresas:
                case TipoPessoaEnum.OrgãoPublico:
                case TipoPessoaEnum.PessoaJuridica:
                case TipoPessoaEnum.PJSemDomicilioPais:
                    return FormataCNPJ(value);
                default:
                    return value;
            }
        }
        public static string FormatCpfCnpj(this string value) {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value.Length <= 11) {
                return FormatLongToCpfCnpj(long.Parse(value), TipoPessoaEnum.PessoaFisica);
            } else {
                return FormatLongToCpfCnpj(long.Parse(value), TipoPessoaEnum.PessoaJuridica);
            }
        }
        public static string FormatLongToCpfCnpj(this long value, TipoPessoaEnum tipoPessoa) {
            switch (tipoPessoa) {
                case TipoPessoaEnum.PessoaFisica:
                case TipoPessoaEnum.PFSemDomicilioPais:
                    string unformattedCpf = value.ToString("D11");
                    return FormataCPF(unformattedCpf);
                case TipoPessoaEnum.ConsorcioEmpresas:
                case TipoPessoaEnum.OrgãoPublico:
                case TipoPessoaEnum.PessoaJuridica:
                case TipoPessoaEnum.PJSemDomicilioPais:
                    string unformattedCnpj = value.ToString("D14");
                    return FormataCNPJ(unformattedCnpj);
                default:
                    return value.ToString();
            }
        }

        public static string FormatLongToCpfCnpj(this long value) {
            if (value.ToString().Length <= 11) {
                return FormatLongToCpfCnpj(value, TipoPessoaEnum.PessoaFisica);
            } else {
                return FormatLongToCpfCnpj(value, TipoPessoaEnum.PessoaJuridica);
            }
        }

        public static string FormatCpfCnpjLongToString(this long? value) {
            if (value == null)
                return string.Empty;

            if (value.ToString().Length <= 11) {
                return string.Format("{0:D11}", value);
            } else {
                return string.Format("{0:D14}", value);
            }
        }
        public static string FormatCpfCnpjToString(this long? value) {
            if (value == null)
                return string.Empty;
            if (value.ToString().Length <= 11) {
                return string.Format("{0:D11}", value);
            } else {
                return string.Format("{0:D14}", value);
            }
        }


    }
}
