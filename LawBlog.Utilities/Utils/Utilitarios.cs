using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Utils
{
    public static class Utilitarios
    {
        public static bool IsCPF(string cpf, bool required)
        {
            if (IsNull(cpf) && required == true) return false;
            if (!IsNull(cpf))
            {
                string valor = cpf.Replace(".", "").Replace("-", "");
                if (valor.Length != 11)
                    return false;
                bool igual = true;
                for (int i = 1; i < 11 && igual; i++)
                    if (valor[i] != valor[0])
                        igual = false;
                if (igual || valor == "12345678909")
                    return false;
                int[] numeros = new int[11];
                for (int i = 0; i < 11; i++)
                    numeros[i] = int.Parse(
                                          valor[i].ToString());
                int soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += (10 - i) * numeros[i];
                int resultado = soma % 11;
                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[9] != 0)
                        return false;
                }
                else if (numeros[9] != 11 - resultado)
                    return false;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += (11 - i) * numeros[i];
                resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[10] != 0)
                        return false;
                }
                else
                    if (numeros[10] != 11 - resultado)
                    return false;
                return true;
            }
            else
                return true;

        }

        public static bool IsCNPJ(string cnpj)
        {
            if (IsNull(cnpj)) return false;

            cnpj = Regex.Replace(cnpj, @"/[^\d] +/ g", "");

            if (IsNull(cnpj)) return false;

            // Elimina CNPJs invalidos conhecidos
            if (cnpj.Length != 14 || cnpj == "00000000000000" || cnpj == "11111111111111" || cnpj == "22222222222222" || cnpj == "33333333333333" || cnpj == "44444444444444" || cnpj == "55555555555555" || cnpj == "66666666666666" || cnpj == "77777777777777" || cnpj == "88888888888888" || cnpj == "99999999999999")
                return false;

            // Valida DVs
            var tamanho = cnpj.Length - 2;

            var numeros = cnpj.Substring(0, tamanho);
            var digitos = cnpj.Substring(tamanho);
            var soma = 0;
            var pos = tamanho - 7;
            for (var i = tamanho; i >= 1; i--)
            {
                soma += numeros[tamanho - i] * pos--;
                if (pos < 2)
                    pos = 9;
            }
            var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos[0])
                return false;

            tamanho = tamanho + 1;
            numeros = cnpj.Substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (var i = tamanho; i >= 1; i--)
            {
                soma += numeros[tamanho - i] * pos--;
                if (pos < 2)
                    pos = 9;
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != digitos[1])
                return false;

            return true;

        }

        public static bool IsGuid(string guid)
        {
            Guid valida = new Guid();
            if (string.IsNullOrWhiteSpace(guid)) return false;
            if (Guid.TryParse(guid, out valida))
            {
                return true;
            }
            else
                return false;
        }

        public static bool IsNull(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsText(string text, bool required)
        {
            if (IsNull(text) && required == true) return false;
            if (!IsNull(text))
            {
                text = text.Replace("-", "").Replace("!", "").Replace("#", "").Replace("%", "").Replace("&", "").Replace("*", "")
                    .Replace("(", "").Replace(")", "").Replace("+", "");
                return System.Text.RegularExpressions.Regex.IsMatch(text, @"[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ0-9]+$");
            }
            else
                return true;

        }

        public static bool IsEmail(string email, bool required)
        {
            if (IsNull(email) && required == true) return false;
            if (!IsNull(email))
            {
                return Regex.IsMatch(email, (@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"));
            }
            else
                return true;
        }

        public static bool IsPhone(string phone, bool required)
        {
            if (IsNull(phone) && required == true) return false;
            if (!IsNull(phone))
            {
                return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\([1-9]{2}\)[2-9][0-9]{3,4}\-[0-9]{4}$");
            }
            else
                return true;
        }

        public static bool IsDate(string date, bool required)
        {
            if (IsNull(date) && required == true) return false;
            if ((!IsNull(date)) && (date != "01/01/0001 00:00:00"))
            {

                date = date.Substring(0, 10);
                var Valid = System.Text.RegularExpressions.Regex.IsMatch(date, @"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
                if (Valid)
                {
                    int day = Convert.ToInt32(date.Substring(0, 2));
                    int month = Convert.ToInt32(date.Substring(3, 2));
                    int year = Convert.ToInt32(date.Substring(6, 4));
                    if (year < 1900) return Valid = false;
                    if ((month < 1) || (month > 12)) return Valid = false;
                    if ((day < 1) || (day > 31)) return Valid = false;
                }
                return Valid;
            }
            else
                return true;
        }

        public static DateTime? toDate(string date)
        {
            DateTime? ret = null;
            DateTime outputDateTimeValue = new DateTime();

            if (!string.IsNullOrEmpty(date))
            {
                date = date.Replace("/", "-").Trim();

                if (date.Any(c => char.IsNumber(c)))
                {
                    var d = int.Parse(date.Split('-')[0]).ToString("D2");
                    var m = int.Parse(date.Split('-')[1]).ToString("D2");
                    var y = date.Split('-')[2];
                    date = d + "-" + m + "-" + y;

                    DateTime.TryParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out outputDateTimeValue);
                    DateTime newdate = new DateTime();
                    if (outputDateTimeValue.Date != newdate.Date)
                        ret = outputDateTimeValue;
                }
            }
            return ret;
        }

        public static bool IsDate(DateTime? date, bool required)
        {
            return IsDate(date.ToString(), required);
        }

        public static bool IsCurrency(string value, bool required)
        {
            if (IsNull(value) && required == true) return false;
            if (!IsNull(value))
            {
                return System.Text.RegularExpressions.Regex.IsMatch(value.Trim(), (@"^\d{1,3}(?:\.\d{3})*,\d{2}$"));
            }
            else
                return true;
        }

        public static bool IsCurrency(decimal? value, bool required)
        {
            try
            {
                if (IsNull(value.ToString()) && required == true) return false;
                else return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDropDown(int? number, bool required)
        {
            try
            {
                int value = Convert.ToInt32(number);
                if (value < 0) return false;
                if (value == 0 && required) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool IsNumber(string number, bool required)
        {
            if (IsNull(number) && required == true) return false;
            else
                return Regex.IsMatch(number, @"^[1-9-]*$");
        }

        public static bool IsNumber(int? number, bool required)
        {
            if (IsNull(number.ToString()) && required == true) return false;
            else
                return Regex.IsMatch(number.ToString(), @"^[0-9-]*$");
        }

        public static bool IsEnum<TEntity>(TEntity enumItem, bool required)
        {
            if ((enumItem.ToString() == "0") && (required == false)) { return true; }
            if ((enumItem == null) && required == true) return false;
            if (Enum.IsDefined(typeof(TEntity), enumItem) == true)
                return true;
            else
                return false;
        }

        public static double ToDouble(String str)
        {
            double ret = 0;

            if (string.IsNullOrEmpty(str))
                return ret;

            if (double.Parse(str).ToString() == str)
                ret = double.Parse(str);
            else
                double.TryParse(str.Replace(".", ","), out ret);

            return ret;
        }

        public static string GetDescriptionFromValue<T>(this T source)
        {
            try
            {
                FieldInfo fi = source.GetType().GetField(source.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0) return attributes[0].Description;
                else return source.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static short GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            var Fields = type.GetFields();
            foreach (var field in Fields)
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (short)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (short)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string RemoveCaracteresEspeciais(string palavra)
        {
            var retorno = Regex.Replace(palavra, @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]", "");
            return retorno;
        }

        public static Dictionary<string, object> GetXmlData(XElement xml)
        {
            var attr = xml.Attributes().ToDictionary(d => d.Name.LocalName, d => (object)d.Value);
            if (xml.HasElements) attr.Add("_value", xml.Elements().Select(e => GetXmlData(e)));
            else if (!xml.IsEmpty) attr.Add("_value", xml.Value);

            return new Dictionary<string, object> { { xml.Name.LocalName, attr } };
        }

        public static int RetornaIdade(DateTime datanascimento)
        {
            var birthdate = datanascimento;
            var today = DateTime.Now;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }

        public static bool CompararStrings(string num01, string num02)
        {
            if (num01.Equals(num02))
                return true;
            return false;
        }

        public static string RetornarDataPorTempo(DateTime dt)
        {
            var today = DateTime.Now;
            if (today.Day == dt.Day && today.Month == dt.Month && today.Year == dt.Year) return dt.ToShortTimeString();
            if (today.Day > dt.Day && today.Month == dt.Month && today.Year == dt.Year) return (today.Day - dt.Day).ToString() + " dias atrás";
            if (today.Day > dt.Day && today.Month > dt.Month && today.Year == dt.Year) return (today.Month - dt.Month).ToString() + " meses atrás";
            if (today.Day > dt.Day && today.Month > dt.Month && today.Year > dt.Year) return (today.Year - dt.Year).ToString() + " anos atrás";
            else return dt.ToShortTimeString();
        }

        public static string RetornarDataFormat(string formato, DateTime? dt)
        {
            if (dt.HasValue) return String.Format("{0:" + formato + "}", dt);
            return null;
        }

        public static int RetornarCargaInteiro(string carga)
        {
            var v = new string[0];
            if (carga.Contains('+')) v = carga.Split('+');
            if (v.Count() > 0) return Convert.ToInt32(v[0]);
            else return Convert.ToInt32(carga);
        }
    }
}
