using System;
using System.Linq;

namespace PizzaCalories
{
    public class StartUp
    {
        static void Main(string[] args)
        {

            try
            {
                string pizzaName = Console.ReadLine().Split()[1];

                string[] doughTokens = Console.ReadLine().Split().ToArray();
                
                string flourType = doughTokens[1];
                string backingTechnique = doughTokens[2];
                double doughWeight = double.Parse(doughTokens[3]);

                Dough dough = new Dough(flourType, backingTechnique, doughWeight);

                Pizza pizza = new Pizza(pizzaName, dough);

                while (true)
                {
                    string input = Console.ReadLine();

                    if (input == "END")
                    {
                        break;
                    }

                    string[] toppingTokens = input.Split().ToArray();

                    string type = toppingTokens[1];
                    double weight = double.Parse(toppingTokens[2]);

                    Topping topping = new Topping(type, weight);

                    pizza.AddTopping(topping);
                }

                Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories():F2} Calories.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
