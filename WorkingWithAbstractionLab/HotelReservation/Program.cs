using System;
using System.Linq;

namespace HotelReservation
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split().ToArray();

            //MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);

            var pricePerDay = decimal.Parse(input[0]);
            var days = int.Parse(input[1]);
            var season = (Season)Enum.Parse(typeof(Season), input[2]);
            var discount = Discount.None;

            if (input.Length == 4)
            {
                discount = (Discount)Enum.Parse(typeof(Discount), input[3]);
            }
            
            var totalPrice = PriceCalculator.GetTotalPrice(pricePerDay, days, season, discount);

            Console.WriteLine(totalPrice.ToString("F2"));
        }
    }
}
