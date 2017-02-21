using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSys
{
    class Validator
    {
        // Default error messages
        public const String ERROR_IS_EMPTY = "Field cannot be empty";
        public const String ERROR_IS_MIN_LENGTH = "Text entered is too short";
        public const String ERROR_IS_MAX_LENGTH = "Text entered is too long";
        public const String ERROR_IS_NUMERIC = "Please enter a valid number";
        public const String ERROR_IS_PRICE = "Please enter a valid price";
        public const String ERROR_IS_ALPHA_NUMERIC = "Please enter Alpha-Numeric values only";
        public const String ERROR_IS_ALPHA = "Please enter alphabetic values only";
        public const String ERROR_IS_EMAIL = "Please enter a valid email address";
        public const String ERROR_IS_VAT_NUM = "Please enter a valid VAT Number";

        public static Boolean isEmpty (String textVal)
        {
            return textVal.Length == 0;
        }

        public static Boolean isMinLength (String textVal, int minLength)
        {
            return textVal.Length >= minLength;
        }

        public static Boolean isMaxLength(String textVal, int maxLength)
        {
            return textVal.Length <= maxLength;
        }

        public static Boolean isNumeric (String textVal)
        {
            int output;
            return int.TryParse(textVal, out output);
        }

        public static Boolean isPrice(String textVal)
        {
            return Regex.Match(textVal, @"^[$€£]?[0-9]+(?:\.[0-9]{0,2})?$").Success;

        }

        public static Boolean isAlphaNumeric(String textVal)
        {
            return Regex.Match(textVal, @"^[a-zA-Z0-9\s,]*$").Success;
        }

        public static Boolean isAlpha(String textVal)
        {
            return Regex.Match(textVal, @"^[a-zA-Z]+$").Success;
        }

        public static Boolean isEmail(String textVal)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(textVal);
                return addr.Address == textVal;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean isVatNum(String textVal)
        {
            Boolean valid = false;
            int numDigits = 0;
            int numLetters = 0;

            if (textVal.Length == 8 && textVal.ToUpper().Substring(0,2) == "IE" && (Char.IsLetter(textVal[textVal.Length - 1]) || Char.IsLetter(textVal[textVal.Length - 2])))
            {
                for (int i = 0; i < textVal.Length; i++)
                {
                    if (Char.IsNumber(textVal[i]))
                    {
                        numDigits++;
                    }
                    else if (Char.IsLetter(textVal[i]))
                    {
                        numLetters++;
                    }
                }

                if ((numDigits == 7 && numLetters == 1) || (numDigits == 6 && numLetters == 2))
                {
                    valid = true;
                }
            }

            return valid;
        }

    }

}
