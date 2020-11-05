using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingSpree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            try
            {
                char[] delimiters = new char[] { '=', ';' };

                string[] peopleInput = Console.ReadLine()
                    .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                List<Person> people = new List<Person>();

                for (int i = 0; i < peopleInput.Length; i += 2)
                {
                    string personName = peopleInput[i];
                    decimal personMoney = int.Parse(peopleInput[i + 1]);

                    people.Add(new Person(personName, personMoney));
                }

                string[] productsInput = Console.ReadLine()
                    .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                List<Product> products = new List<Product>();

                for (int i = 0; i < productsInput.Length; i += 2)
                {
                    string productName = productsInput[i];
                    decimal productCost = int.Parse(productsInput[i + 1]);

                    products.Add(new Product(productName, productCost));
                }

                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "END")
                    {
                        break;
                    }

                    string[] tokens = input.Split().ToArray();

                    string personName = tokens[0];
                    string productName = tokens[1];

                    Product productToAdd = products.FirstOrDefault(p => p.Name == productName);

                    people.FirstOrDefault(p => p.Name == personName).AddProduct(productToAdd);
                }

                foreach (var person in people)
                {
                    Console.WriteLine(person.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }
    }
}
