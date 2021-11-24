using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain.Util.Extensions {
    public static class CastExtensions {

        public static int? ToInt(this string str, int? defaultValue = null) {
            str = str.KeepNumbersOnly();
            if (int.TryParse(str, out int result)) {
                return result;
            } else {
                return defaultValue;
            }
        }

        public static long? ToLong(this string str, long? defaultValue = null) {
            str = str.KeepNumbersOnly();
            if (long.TryParse(str, out long result)) {
                return result;
            } else {
                return defaultValue;
            }
        }

        public static decimal? ToDecimal(this string str, decimal? defaultValue = null) {
            if (string.IsNullOrWhiteSpace(str)) {
                return defaultValue;
            }

            // 1° remove não numéricos
            str = Regex.Replace(str, "[^0-9.,-]", "");

            // 2° verifica a existência de "." e "," para checar qual o caracter separados de casas decimais para fazer o correto formato de en-US
            var lastIndexPoint = str.LastIndexOf(".");
            var lastIndexComma = str.LastIndexOf(",");

            if (lastIndexPoint != -1 && lastIndexComma != -1) {
                if (lastIndexComma > lastIndexPoint) {
                    str = str.Replace(".", "");
                } else {
                    str = str.Replace(",", "");
                }
            }
            str = str.Replace(",", ".");

            // 3° converte
            return Convert.ToDecimal(str, new CultureInfo("en-US"));
        }

        public static string ToString(this int? number, string defaultValue = null) {
            if (number == null)
                return defaultValue;
            else
                return number.Value.ToString();
        }

        public static string ToString(this long? number, string defaultValue = null) {
            if (number == null)
                return defaultValue;
            else
                return number.Value.ToString();
        }

        public static string ToString(this decimal? number, string cultureName, string defaultValue = null) {
            if (number == null)
                return defaultValue;
            else
                return number.Value.ToString(new CultureInfo(cultureName));
        }

        public static string ToString(this bool? bln, string defaultValue = null) {
            if (bln == null) {
                return defaultValue;
            } else {
                return (bln.Value ? "true" : "false");
            }
        }

        public static string KeepNumbersOnly(this string str) {
            var numberOnlyRegex = new Regex(@"[^\d]");
            return numberOnlyRegex.Replace(str, "");
        }

        public static int? ZeroToNull(this int number) {
            if (0 == number) {
                return null;
            }
            return number;
        }
        public static long? ZeroToNull(this long number) {
            if (0 == number) {
                return null;
            }
            return number;
        }
        public static decimal? ZeroToNull(this decimal number) {
            if (decimal.Zero == number) {
                return null;
            }
            return number;
        }

    }
}
