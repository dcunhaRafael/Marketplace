using System;
using System.Globalization;

namespace Domain.Util.Extensions {
    public static class FormatExtensions {
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        private static string FormataCPF(string cpf) {
            try {
                return string.Format("{0}.{1}.{2}-{3}", cpf.Remove(cpf.Length - 8), cpf.Substring(cpf.Length - 8, 3), cpf.Substring(cpf.Length - 5, 3), cpf.Substring(cpf.Length - 2, 2));
            } catch {
                return cpf;
            }
        }

        private static string FormataCNPJ(string cnpj) {
            if (!string.IsNullOrWhiteSpace(cnpj)) {
                cnpj = long.Parse(cnpj).ToString("D14");
                cnpj = string.Format("{0}.{1}.{2}/{3}-{4}", cnpj.Remove(cnpj.Length - 12), cnpj.Substring(cnpj.Length - 12, 3), cnpj.Substring(cnpj.Length - 9, 3), cnpj.Substring(cnpj.Length - 6, 4), cnpj.Substring(cnpj.Length - 2));
            }
            return cnpj;
        }

        public static string FormatCpfCnpj(this long value) {
            if (value.ToString().Length <= 11) {
                string unformattedCpf = value.ToString("D11");
                return FormataCPF(unformattedCpf);
            } else {
                string unformattedCnpj = value.ToString("D14");
                return FormataCNPJ(unformattedCnpj);
            }
        }

        public static string FormatCpfCnpj(this string value) {
            var cpfCnpj = long.Parse(value.KeepNumbersOnly());
            if (cpfCnpj.ToString().Length <= 11) {
                string unformattedCpf = cpfCnpj.ToString("D11");
                return FormataCPF(unformattedCpf);
            } else {
                string unformattedCnpj = cpfCnpj.ToString("D14");
                return FormataCNPJ(unformattedCnpj);
            }
        }

        public static string FormatDate(this DateTime value) {
            if (value == DateTime.MinValue)
                return string.Empty;

            return value.ToString("dd/MM/yyyy");
        }

        public static string FormatDateTime(this DateTime value) {
            if (value == DateTime.MinValue)
                return string.Empty;

            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string FormatDecimal(this decimal value) {
            string newValue = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:#,##0.00}", value);
            return newValue;
        }

        public static string FormatCurrency(this decimal value) {
            return value.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
        }

        private static string FormatZipCode(this string value) {
            return string.Format("{0}-{1}", value.Substring(0, 5), value.Remove(0, 5));
        }
        public static string FormatZipCode(this int value) {
            string unformattedCpf = value.ToString("D8");
            return FormatZipCode(unformattedCpf);
        }

        public static string Truncate(this string source, int length, string sufix) {
            if (!string.IsNullOrWhiteSpace(source)) {
                if (source.Length > length) {
                    source = source.Substring(0, length) + sufix;
                }
            }
            return source;
        }

        public static string ToJsonDecimal(this decimal? value) {
            return value?.ToString("N2", CultureInfo.GetCultureInfo("en-US"));
        }

        public static string ToJsonDecimal(this decimal value) {
            return value.ToString("N2", CultureInfo.GetCultureInfo("en-US"));
        }

        public static string FormatBytes(this long value, int decimalPlaces = 1) {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + FormatBytes(-value, decimalPlaces); }
            if (value == 0) { return "0 bytes"; }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000) {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public static string FormatCompetency(this string value) {
            if (!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value)) {
                var values = value.Split("/");
                if (values.Length == 2) {
                    return $"{values[0]}/{values[1].PadLeft(2, char.Parse("0"))}";
                }
            }
            return string.Empty;
        }
    }
}
