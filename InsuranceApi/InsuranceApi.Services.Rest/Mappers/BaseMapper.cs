using System;
using System.Configuration;
using System.Globalization;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class BaseMapper {
               

        public static int? StringToInt(string str, int? defaultValue = null) {
            int result = 0;
            if (int.TryParse(str, out result)) {
                return result;
            } else {
                return defaultValue;
            }
        }

        public static long? StringToLong(string str, long? defaultValue = null) {
            long result = 0;
            if (long.TryParse(str, out result)) {
                return result;
            } else {
                return defaultValue;
            }
        }

        public static decimal? StringToDecimal(string str, decimal? defaultValue = null) {
            if (string.IsNullOrWhiteSpace(str)) {
                return defaultValue;
            } else {
                return Convert.ToDecimal(str, new CultureInfo("en-US"));
            }
        }

        public static string IntToString(int? number, string defaultValue = null) {
            if (number == null)
                return defaultValue;
            else
                return number.Value.ToString();
        }

        public static string LongToString(long? number, string defaultValue = null) {
            if (number == null)
                return defaultValue;
            else
                return number.Value.ToString();
        }

        public static string DecimalToString(decimal? number, string defaultValue = null) {
            if (number == null)
                return defaultValue;
            else
                return number.Value.ToString(new CultureInfo("en-US"));
        }

        public static string DateToString(DateTime? date, string defaultValue = null) {
            if (date == null) {
                return defaultValue;
            } else {
                return date.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            }
        }

        public static DateTime? StringToDate(string str, DateTime? defaultValue = null) {
            if (string.IsNullOrWhiteSpace(str)) {
                return defaultValue;
            } else {
                return DateTime.ParseExact(str, "yyyy-MM-dd", new CultureInfo("en-US"));
            }
        }

        public static string BooleanToString(bool? bln, string defaultValue = null) {
            if (bln == null) {
                return defaultValue;
            } else {
                return (bln.Value ? "true" : "false");
            }
        }

    }
}
