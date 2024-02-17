using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace Cards.WEB.Extensions
{
    public static class StringExtensions
    {
        private static readonly HashSet<char> _base64Characters = new HashSet<char>()
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/',
            '='
        };

        public static bool StringIsAllNumeric(this string s)
        {
            var result = default(bool);

            if (!string.IsNullOrWhiteSpace(s))
            {
                s = s.Trim();

                result = !s.ToCharArray().Any(c => Char.IsLetter(c) || Char.IsWhiteSpace(c) || Char.IsSymbol(c) || Char.IsSeparator(c) || Char.IsPunctuation(c) || Char.IsControl(c));
            }

            return result;
        }

        public static string ToTitleCase(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
#if !SILVERLIGHT
                var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

                str = textInfo.ToTitleCase(str.Trim().ToLower());
#endif
                return str;
            }
            else return string.Empty;
        }

        public static string Limit(this string str, int characterCount)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (str.Length <= characterCount)
                    return str.PadRight(characterCount, ' ');

                else return str.Substring(0, characterCount).PadRight(characterCount, ' ');
            }
            else return string.Empty;
        }

        public static string StripPunctuation(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;
            else
                return new string(s.ToArray().Where(c => !char.IsPunctuation(c)).ToArray()).TrimStart(new char[] { '\r', '\n' }).TrimEnd(new char[] { '\r', '\n' })
                    .Replace('\n', ' ')
                    .Replace('\r', ' ')
                    .Replace('\\', ' ')
                    .Replace('/', ' ')
                    .Replace(':', ' ')
                    .Replace('*', ' ')
                    .Replace('?', ' ')
                    .Replace('<', ' ')
                    .Replace('>', ' ')
                    .Replace('|', ' ')
                    .Replace('%', ' ')
                    .Replace('^', ' ')
                    .Replace('[', ' ')
                    .Replace(']', ' ')
                    .Replace('_', ' ')
                    .Replace('\'', ' ');
        }

        public static string ReplaceNumbers(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return Regex.Replace(input, "[0-9]", "X");
            }
            else return input;
        }

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static string ExtractInitials(this string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                text = regex.Replace(text, " "); // ensure single spancing! 

                var result = text.Trim().Split(new char[] { ' ' }).Select(y => y[0]);

                return String.Join<Char>(".", result);
            }
            else return text;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(toCheck))
            {
                return source.IndexOf(toCheck, comp) >= 0;
            }
            else return false;
        }

        public static string Left(this string str, int length)
        {
            str = (str ?? string.Empty);

            return str.Substring(0, Math.Min(length, str.Length));
        }

        public static string Right(this string str, int length)
        {
            str = (str ?? string.Empty);

            return (str.Length >= length) ? str.Substring(str.Length - length, length) : str;
        }

        public static string SanitizePatIndexInput(this string str)
        {
            str = (str ?? string.Empty);

            if (Regex.IsMatch(str, "(^%(.*)%$)", RegexOptions.IgnoreCase))
            {
                return str;
            }
            else if (str.Contains("%") || str.Contains("_") || str.Contains("[") || str.Contains("]") || str.Contains("^"))
            {
                return str;
            }
            else return $"%{str}%";
        }

        public static string Encrypt(this string plainText)
        {
            if (!string.IsNullOrWhiteSpace(plainText) && !plainText.IsBase64String())
            {
                //encrypt data
                var data = Encoding.Unicode.GetBytes(plainText);
                byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);

                //return as base64 string
                return Convert.ToBase64String(encrypted);
            }
            else return plainText;
        }

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public static string Decrypt(this string cipher)
        {
            if (!string.IsNullOrWhiteSpace(cipher) && cipher.IsBase64String())
            {
                //parse base64 string
                byte[] data = Convert.FromBase64String(cipher);

                //decrypt data
                byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);

                return Encoding.Unicode.GetString(decrypted);
            }
            else return cipher;
        }

        public static bool IsBase64String(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else if (value.Any(c => !_base64Characters.Contains(c)))
            {
                return false;
            }
            else if (value.Length < 32)
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(value);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string MD5Hash(this string input)
        {
            StringBuilder hash = new StringBuilder();

            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();

            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string TransactionReferenceNumber()
        {
            var newGuid = Guid.NewGuid().ToString();

            var splitGuid = newGuid.Split('-');

            return splitGuid[0].ToString().ToUpper();
        }

        public static string ExpungeUnicodeCharacters(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return Regex.Replace(input, "[^\x0d\x0a\x20-\x7e\t]", string.Empty);
            }
            else return input;
        }

        public static string ToUnderScoreNamingConvention(this string s, bool toLowercase)
        {
            if (toLowercase)
            {
                var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

                return r.Replace(s, "_").ToLower();
            }
            else
                return s;
        }

        public static string GetHashSha256(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            SHA256Managed hashstring = new SHA256Managed();

            byte[] hash = hashstring.ComputeHash(bytes);

            string hashString = string.Empty;

            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }

        public static string GetHashBase64(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            string hashString = Convert.ToBase64String(bytes);

            return hashString;
        }
    }
}
