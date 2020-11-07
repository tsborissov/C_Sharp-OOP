using System;
using System.Collections.Generic;
using System.Linq;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IPerson> buyers = new List<IPerson>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine()
                    .Split();

                string name = input[0];
                int age = int.Parse(input[1]);

                if (input.Length == 4)
                {
                    string id = input[2];
                    string birthdate = input[3];

                    buyers.Add(new Citizen(name, age, id, birthdate));
                }
                else
                {
                    string group = input[2];

                    buyers.Add(new Rebel(name, age, group));
                }
            }


            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                {
                    break;
                }

                IPerson currentPerson = buyers.FirstOrDefault(x => x.Name == input);

                if (currentPerson != null)
                {
                    currentPerson.BuyFood();
                }

            }

            int foodTotal = buyers.Sum(x => x.Food);

            Console.WriteLine(foodTotal);
            
        }
    }
}
