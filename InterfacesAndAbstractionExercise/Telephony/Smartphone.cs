using System;
using System.Text.RegularExpressions;

namespace Telephony
{
    public class Smartphone : IBrowsable, ICallable
    {
        public void Browse(string site)
        {
            if (ValidateSite(site))
            {
                System.Console.WriteLine($"Browsing: {site}!");
            }
        }


        public void Dial(string number)
        {
            if (ValidateNumber(number))
            {
                System.Console.WriteLine($"Calling... {number}");
            }
        }

        private bool ValidateSite(string site)
        {
            string invalidPattern = @"[0-9]{1,}";

            Regex regex = new Regex(invalidPattern);

            if (regex.IsMatch(site))
            {
                Console.WriteLine("Invalid URL!");

                return false;
            }

            return true;
        }

        private bool ValidateNumber(string number)
        {
            string pattern = @"\b[0-9]{10}\b";

            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(number))
            {
                Console.WriteLine("Invalid number!");

                return false;
            }

            return true;
        }

        
    }
}
