using System;
using System.Globalization;

namespace Domain.Util.Extensions {
    public static class DateExtensions {

        public static string ToJsonDateString(this DateTime? date) {
            if (date == null) {
                return null;
            } else {
                return date.Value.ToString("yyyy-MM-dd");
            }
        }
        public static string ToJsonDateTimeString(this DateTime? date) {
            if (date == null) {
                return null;
            } else {
                return date.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            }
        }

        public static string ToString(this DateTime? date, string defaultValue = null) {
            if (date == null) {
                return defaultValue;
            } else {
                return date.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            }
        }

        public static DateTime? ToDate(this string str, DateTime? defaultValue = null) {
            if (string.IsNullOrWhiteSpace(str)) {
                return defaultValue;
            } else {
                return DateTime.ParseExact(str, "yyyy-MM-dd", new CultureInfo("en-US"));
            }
        }

        public static DateTime? ToDateTime(this string str, DateTime? defaultValue = null) {
            if (string.IsNullOrWhiteSpace(str)) {
                return defaultValue;
            } else {
                return DateTime.ParseExact(str, "yyyy-MM-ddTHH:mm:ss", new CultureInfo("en-US"));
            }
        }

        public static DateTime ToMinDateTime(this DateTime date) {
            date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            return date;
        }

        public static DateTime ToMaxDateTime(this DateTime date) {
            date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            return date;
        }

        /// <summary>
        /// Retorna o último dia do mês para a data informada
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToLastDayOfMonth(this DateTime date) {
            var lastDay = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            return lastDay.Date;
        }

        /// <summary>
        /// Retorna o primeiro dia do mês para a data informada
        /// </summary>
        /// <param name="date"></param>
        /// <param name="notLessThanDate">data resultando não pode ser menor aque a data recebida</param>
        /// <returns></returns>
        public static DateTime ToFirstDayOfMonth(this DateTime date, bool notLessThanDate = false) {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            if (notLessThanDate && (date.Date > firstDay.Date)) {
                return firstDay.AddMonths(1);
            }
            return firstDay.Date;
        }

    }
}
