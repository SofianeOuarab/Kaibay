using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace DemoKaibay.Extensions
{
    public static class StringExtensionMethods
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public static String AnonymizeEmail(this string str)
        {
            var email = new MailAddress(str);

            return AnonymizeString(email.User);

            string AnonymizeString(string str)
            {
                var tmpBuilder = new StringBuilder(str);

                for (int i = 1; i < tmpBuilder.Length; i++)
                {
                    if (tmpBuilder[i] != '.')
                    {
                        tmpBuilder[i] = '*';
                    }
                }

                return tmpBuilder.ToString();
            }
        }
    }
}
