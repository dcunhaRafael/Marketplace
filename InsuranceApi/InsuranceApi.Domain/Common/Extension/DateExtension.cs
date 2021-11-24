using System;

namespace InsuranceApi.Domain.Common.Extension {
    public static class DateExtension {

        public static string FormatStringDate(string date, string defValue = "") {
            if (string.IsNullOrWhiteSpace(date))
                return defValue;
            else
                return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
        }

        public static string FormatDate(DateTime? date, string defValue = "") {
            if (date == null)
                return defValue;
            else
                return date.Value.ToString("dd/MM/yyyy");
        }

        public static string FormatDateTimeZone(DateTime? date, string defValue = "") {
            if (date == null)
                return defValue;
            else
                return date.Value.ToString("s") + "Z";
        }

        public static string FormatDateTimeZone(string date, string defValue = "") {
            if (string.IsNullOrWhiteSpace(date))
                return defValue;
            else {
                var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                return dateTime.ToString("s") + "Z";
            }
        }

    }
}
