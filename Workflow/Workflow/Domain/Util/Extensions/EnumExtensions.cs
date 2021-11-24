using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Domain.Util.Extensions {
    public static class EnumExtensions {

        public static T ParseEnum<T>(this string value) where T : struct {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }
        public static T FromDefaultValue<T>(this string value) where T : struct {
            try {
                var list = System.Enum.GetValues(typeof(T));
                foreach (var item in list) {
                    var enumType = item.GetType();
                    var field = enumType.GetField(item.ToString());
                    var attributes = field.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                    if (((DefaultValueAttribute)attributes[0]).Value.Equals(value)) {
                        return (T)item;
                    }
                }
                return default(T);
            } catch {
                return default(T);
            }
        }

        public static string GetDescription(this System.Enum value) {
            try {
                var enumType = value.GetType();
                var field = enumType.GetField(value.ToString());
                var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length == 0 ? value.ToString() : ((DescriptionAttribute)attributes[0]).Description;
            } catch {
                return string.Empty;
            }
        }

        public static string GetDefaultValue(this System.Enum value) {
            try {
                var enumType = value.GetType();
                var field = enumType.GetField(value.ToString());
                var attributes = field.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                return attributes.Length == 0 ? value.ToString() : ((DefaultValueAttribute)attributes[0]).Value.ToString();
            } catch {
                return string.Empty;
            }
        }

        public static IEnumerable<KeyValuePair<string, string>> ToList<T>() {
            var list = from status in System.Enum.GetValues(typeof(T)).Cast<System.Enum>()
                   select new KeyValuePair<string, string>(status.GetDefaultValue(), status.GetDescription());
            return list;
        }

        public static IEnumerable<KeyValuePair<int, string>> ToListEx<T>() {
            var list = from status in System.Enum.GetValues(typeof(T)).Cast<System.Enum>()
                   select new KeyValuePair<int, string>(Convert.ToInt32(status), status.GetDescription());
            return list;
        }

    }
}
