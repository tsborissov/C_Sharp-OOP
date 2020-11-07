using System;
using System.Text.RegularExpressions;

namespace Telephony
{
    public class StationaryPhone : ICallable
    {
        public void Dial(string number)
        {
            if (ValidateNumber(number))
            {
                System.Console.WriteLine($"Dialing... {number}");
            }
        }

        private bool ValidateNumber(string number)
        {
            string pattern = @"\b[0-9]{7}\b";
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
