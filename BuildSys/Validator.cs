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

        public static Boolean isPassword(String textVal)
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

    }

}
