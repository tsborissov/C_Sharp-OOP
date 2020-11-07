using System;
using System.Linq;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] phoneNumbers = Console.ReadLine().Split().ToArray();
            string[] sites = Console.ReadLine().Split().ToArray();

            Smartphone smartphone = new Smartphone();
            StationaryPhone stationaryPhone = new StationaryPhone();

            for (int i = 0; i < phoneNumbers.Length; i++)
            {
                string currentPhoneNumber = phoneNumbers[i];

                if (currentPhoneNumber.Length == 10)
                {
                    smartphone.Dial(currentPhoneNumber);
                }
                else
                {
                    stationaryPhone.Dial(currentPhoneNumber);
                }
            }

            for (int i = 0; i < sites.Length; i++)
            {
                string currentSite = sites[i];

                smartphone.Browse(currentSite);
            }
        }
    }
}
