using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LawBlog.Utilities.Extensions
{
    public static class EnumExtensions
    {
        #region Public Methods

        public static string ToDescription(this Enum value)
        {
            return EnumExtensions.GetDescription(value);
        }
        public static IList<EnumItem> ToList(this Enum value)
        {
            return EnumExtensions.GetEnumDescriptions(value, true);
        }
        public static IList<EnumItem> ToList(this Enum value, bool sort)
        {
            return EnumExtensions.GetEnumDescriptions(value, sort);
        }
        public static IList<EnumItem> ToList<T>(bool sort)
        {
            List<EnumItem> result = null;

            Type enumType = typeof(T);
            Array names = null;

            if (isTipoEnum(enumType))
            {
                names = Enum.GetNames(enumType);

                if (names.Length > 0)
                {
                    result = new List<EnumItem>(names.Length);

                    foreach (string item in names)
                    {
                        var itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                        var enumItem1 = itemEnumType.GetField(item);

                        var intValue = (short)enumItem1.GetValue(itemEnumType);
                        var stringValue = enumItem1.GetValue(itemEnumType).ToString();

                        T itemInstance = (T)Enum.ToObject(enumType, intValue);

                        var description = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(DescriptionAttribute), false);

                        if (description != null && description.Any())
                            result.Add(new EnumItem()
                            {
                                ID = intValue,
                                Value = stringValue,
                                Description = ((DescriptionAttribute)description.First()).Description
                            });
                        else
                            result.Add(new EnumItem() { ID = intValue, Value = stringValue, Description = item });
                    }
                }

                if (sort)
                    result.Sort((enum1, enum2) => { return enum1.Description.CompareTo(enum2.Description); });

                return result;
            }
            else
                throw new InvalidOperationException();
        }
        private static bool isTipoEnum(Type enumType)
        {
            return enumType.BaseType == typeof(Enum);
        }
        public static T ParseToEnum<T>(this Enum value, string valueToConvert)
        {
            return (T)Enum.Parse(typeof(T), valueToConvert);
        }
        public static string EnumForString(this Enum @string)
        {
            string value = @string.ToString();
            return value[0].ToString();
        }
        public static IEnumerable<T> Values<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).OfType<T>();
        }
        #endregion

        #region Private Members

        private const char ENUM_SEPARATOR_CHARACTER = ',';
        private static string GetDescription(Enum value)
        {
            // Check for Enum that is marked with FlagAttribute

            var entries = value.ToString().Split(ENUM_SEPARATOR_CHARACTER);
            var description = new string[entries.Length];

            for (var i = 0; i < entries.Length; i++)
            {
                var fieldInfo = value.GetType().GetField(entries[i].Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = (attributes.Length > 0) ? attributes[0].Description : entries[i].Trim();
            }

            return string.Join(", ", description);
        }
        private static IList<EnumItem> GetEnumDescriptions<T>(T enumerator, bool sort)
        {
            List<EnumItem> result = null;

            Type enumType = enumerator.GetType();
            Array names = null;

            if (enumType == null || !enumType.IsEnum)
                return result;

            object obj = enumType.Assembly.CreateInstance(enumType.FullName);
            names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                result = new List<EnumItem>(names.Length);

                foreach (string item in names)
                {
                    Type itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                    FieldInfo enumItem1 = itemEnumType.GetField(item);

                    int intValue = (int)enumItem1.GetValue(itemEnumType);
                    string stringValue = enumItem1.GetValue(itemEnumType).ToString();

                    T itemInstance = (T)Enum.ToObject(enumType, intValue);

                    // Get instance of the attribute.
                    object[] description = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (description != null && description.Count() > 0)
                    {
                        result.Add(new EnumItem() { ID = intValue, Value = stringValue, Description = ((DescriptionAttribute)description.First()).Description });
                    }
                    else
                    {
                        result.Add(new EnumItem() { ID = intValue, Value = stringValue, Description = item });
                    }
                }
            }

            if (sort)
            {
                // Cria um método anônimo para fazer a comparação entre os itens da lista e ordená-la pela descrição
                result.Sort((enum1, enum2) => { return enum1.Description.CompareTo(enum2.Description); });
            }

            return result;
        }
        private static IList<EnumItem> GetEnumString<T>(T enumerator, bool sort)
        {
            List<EnumItem> result = null;

            Type enumType = enumerator.GetType();
            Array names = null;

            if (enumType == null || !enumType.IsEnum)
                return result;

            object obj = enumType.Assembly.CreateInstance(enumType.FullName);
            names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                result = new List<EnumItem>(names.Length);

                foreach (string item in names)
                {
                    Type itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                    FieldInfo enumItem1 = itemEnumType.GetField(item);

                    int intValue = (int)enumItem1.GetValue(itemEnumType);
                    string stringValue = enumItem1.GetValue(itemEnumType).ToString();

                    T itemInstance = (T)Enum.ToObject(enumType, intValue);

                    result.Add(new EnumItem() { ID = intValue, Value = stringValue, Description = item });
                }
            }

            if (sort)
            {
                // Cria um método anônimo para fazer a comparação entre os itens da lista e ordená-la pela descrição
                result.Sort((enum1, enum2) => {
                    return enum1.Description.CompareTo(enum2.Description);
                });
            }
            return result;
        }

        #endregion
    }
}