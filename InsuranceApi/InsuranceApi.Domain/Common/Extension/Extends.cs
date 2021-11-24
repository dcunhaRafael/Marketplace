using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace InsuranceApi.Domain.Common.Extension {
    public static class Extends {
   
        /// <summary>
        /// Conversão simples de DateTime para String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDateTimeDefault(this DateTime value) {
            if (value == DateTime.MinValue)
                return string.Empty;

            return Convert.ToString(value);
        }

        /// <summary>
        /// COnversão de DateTime para String formatada em padrão pt-BR
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDate(this DateTime value) {
            if (value == DateTime.MinValue)
                return string.Empty;

            return value.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Conversão de String para String formatada em padrão UTC
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDateTimeUtc(this DateTime value) {
            if (value == DateTime.MinValue)
                return string.Empty;

            return value.ToString("yyyy-MM-ddT00:00:00Z");
        }

        /// <summary>
        /// Conversão de String para DateTime
        /// </summary>
        /// <param name="value">Deve estar nos padrões yyyy/MM/dd, yyyy-MM-dd ou yyyy-MM-ddTHH:mm:ssZ</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(this string value) {
            string[] formatsDate = { "yyyy/MM/dd", "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ssZ" };
            var data = DateTime.ParseExact(value, formatsDate, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            return data;
        }

        /// <summary>
        /// COnversão de String para String formatada em padrão pt-BR
        /// </summary>
        /// <param name="value">Deve estar nos padrões yyyy-MM-dd ou yyyy-MM-ddTHH:mm:ssZ</param>
        /// <returns></returns>
        public static string FormatDateTimeString(this string value) {
            try {
                if (string.IsNullOrWhiteSpace(value))
                    return value;

                string[] formatsDate = { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ssZ" };
                var dateConvert = DateTime.ParseExact(value, formatsDate, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

                if (dateConvert == DateTime.MinValue)
                    return String.Empty;

                return dateConvert.ToString("dd/MM/yyyy");
            } catch {
                return String.Empty;
            }
        }

        /// <summary>
        /// Conversão de String para String formatada em padrão UTC
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDateTimeUtcString(this string value) {
            try {
                if (string.IsNullOrWhiteSpace(value))
                    return value;
                return Convert.ToDateTime(value).ToString("yyyy-MM-ddT00:00:00Z");
            } catch {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retorna o último dia do mês para a data informada
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(this DateTime date) {
            var lastDay = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            return lastDay.Date;
        }

        /// <summary>
        /// Retorna o primeiro dia do mês para a data informada
        /// </summary>
        /// <param name="date"></param>
        /// <param name="notLessThanDate">data resultando não pode ser menor aque a data recebida</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(this DateTime date, bool notLessThanDate = false) {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            if (notLessThanDate && (date.Date > firstDay.Date)) {
                return firstDay.AddMonths(1);
            }
            return firstDay.Date;
        }

        /// <summary>
        /// Formata o Decimal em String padrão monetário
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDecimal(this decimal value) {
            string newValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:#,##0.00}", value);
            return newValue;
        }

        /// <summary>
        /// Formata o Decimal em String padrão monetário
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatDecimal(decimal? value) {
            if (value == null)
                return string.Empty;
            string newValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:#,##0.00}", value);
            return newValue;
        }

        /// <summary>
        /// Formata o Decimal em String padrão para campos percentuais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatPercentual(this decimal value) {
            string newValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:#0.000000}%", value);
            return newValue;
        }

        /// <summary>
        /// Formata o Decimal em String padrão para campos percentuais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatPercentual2d(this decimal value) {
            string newValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:#0.00}%", value);
            return newValue;
        }

        /// <summary>
        /// Formato o CEP em 00.000-000
        /// </summary>
        /// <param name="cep">Sequencia numérica de 8 dígitos. Exemplo: 00000000</param>
        /// <returns>CEP formatado</returns>
        public static string FormatCep(this string value) {
            if (string.IsNullOrWhiteSpace(value)) {
                return string.Empty;
            } else {
                try {
                    value = Convert.ToInt64("0" + value.ApenasNumericos()).ToString("D8");
                    return string.Format("{0}-{1}", value.Substring(0, 5), value.Remove(0, 5));
                } catch {
                    return value;
                }
            }
        }
        public static string FormatCep(this int value) {
            return FormatCep(value.ToString());
        }

        /// <summary>
        /// Formata um número de telefone
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatPhoneNumber(this long number) {
            var numberStr = number.ToString();
            switch (numberStr.Length) {
                case 8:     //-- 9999-9999
                    return number.ToString("0000-0000");
                case 9:     //-- 99999-9999
                    return number.ToString("00000-0000");
                case 10:     //-- (99) 9999-9999
                    return number.ToString("(00) 0000-0000");
                case 11:     //-- (99) 99999-9999
                    return number.ToString("(00) 00000-0000");
                case 12:     //-- +99 (99) 9999-9999
                    return number.ToString("+00 (00) 0000-0000");
                case 13:     //-- +99 (99) 99999-9999
                    return number.ToString("+00 (00) 00000-0000");
                default:
                    return numberStr;
            }
        }

        public static string FormatPhoneNumber(this string number) {
            number = number?.ApenasNumericos();
            if (string.IsNullOrWhiteSpace(number)) {
                return "";
            } else {
                return long.Parse(number).FormatPhoneNumber();
            }
        }

        /// <summary>
        /// Formata um número de licitação 9999/9999
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatBiddingNumber(this int number) {
            return FormatBiddingNumber(number.ToString());
        }
        public static string FormatBiddingNumber(this string number) {
            number = number?.ApenasNumericos();
            if (string.IsNullOrWhiteSpace(number)) {
                return "";
            } else {
                number = int.Parse("0" + number.ApenasNumericos()).ToString("D8");
                return string.Format("{0}/{1}", number.Substring(0, 4), number.Remove(0, 4));
            }
        }

        public static string RemoveAccent(this string text) {
            if (string.IsNullOrEmpty(text))
                return text;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++) {
                if (text[i] > 255)
                    sb.Append(text[i]);
                else
                    sb.Append(s_Diacritics[text[i]]);
            }
            return sb.ToString();
        }

        private static readonly char[] s_Diacritics = GetDiacritics();
        private static char[] GetDiacritics() {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';

            return accents;
        }

        public static bool IsNumeric(this string s) {
            return decimal.TryParse(s, out decimal output);
        }

        /// <summary>
        /// Remove os caracteres não numéricos de uma String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ApenasNumericos(this string value) {
            var apenasDigitos = new Regex(@"[^\d]");
            return apenasDigitos.Replace(value, "");
        }

        public static bool IsNullOrEmpty(this string value) {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value) {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool HasValue(this string value) {
            var ws = string.IsNullOrWhiteSpace(value);
            var ne = string.IsNullOrEmpty(value);
            if (ws && ne)
                return false;
            else
                return true;
        }

        public static DateTime? ToNullableDateTime(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return DateTime.Parse(value);
        }

        public static DateTime? ToNullableDateTime(this string value, string format) {
            if (value.IsNullOrEmpty())
                return null;
            return DateTime.ParseExact(value, format, null);
        }

        public static DateTime ToDateTime(this long timestamp) {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp);
            return epoch.ToLocalTime();
        }

        public static DateTime ToDateTime(this string value) {
            return DateTime.Parse(value);
        }

        public static short? ToNullableShort(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return short.Parse(value);
        }

        public static short ToShort(this string value) {
            return short.Parse(value);
        }

        public static byte? ToNullableByte(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return byte.Parse(value);
        }

        public static decimal? ToNullableDecimal(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return decimal.Parse(value);
        }

        public static decimal? ToNullableDecimal(this string value, bool specialCharacter) {
            if (value.IsNullOrEmpty())
                return null;
            return decimal.Parse(value.Replace("%", ""));
        }

        public static decimal? ToNullableDecimal(this int? value) {
            if (value != null)
                return Convert.ToDecimal(value);
            return null;
        }

        public static decimal ToDecimal(this string value) {
            return decimal.Parse(value);
        }

        public static double? ToNullableDouble(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return double.Parse(value);
        }

        public static int? ToNullableInt(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return Convert.ToInt32(decimal.Parse(value));
        }

        public static int ToInt(this string value) {
            return Int32.Parse(value);
        }

        public static long? ToNullableLong(this string value) {
            if (value.IsNullOrEmpty())
                return null;
            return long.Parse(value);
        }

        public static decimal? ToNullableDecimalPercentage(this string value) {
            value = Regex.Replace(value, "[^0-9.,]", "");
            value = value.Replace(",", ".");
            if (value.IsNullOrEmpty())
                return null;
            return decimal.Parse(value, new CultureInfo("en-US"));
        }

        public static decimal? ToNullableDecimalCleanDiv100(this string value) {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            value = value.ApenasNumericos();
            if (string.IsNullOrWhiteSpace(value))
                return null;
            return decimal.Parse(value) / new decimal(100);
        }

        public static long ToLong(this string value) {
            return long.Parse(value);
        }

        public static int? ToNullableInt(this bool? value) {
            if (value != null)
                return Convert.ToInt32(value);
            return null;
        }

        public static string VerificaTamanhoCNPJ(this long? cpf) {
            if (cpf != null)
                return cpf.ToString().Length < 14 ? cpf.ToString().PadLeft(14, '0') : cpf.ToString();
            return null;
        }

        public static int? ToNullableLong(this decimal? value) {
            if (value == null)
                return null;
            return Convert.ToInt32(value);
        }

        public static int PatternCount(this string source, string pattern) {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrEmpty(pattern)) {
                return 0;
            } else {
                return (source.Length - source.Replace(pattern, "").Length) / pattern.Length;
            }
        }
    }
}
