using System;
using System.Collections.Generic;
using System.Linq;

using WildFarm.Core.Interfaces;
using WildFarm.Factories;
using WildFarm.Models.Animals;
using WildFarm.Models.Foods;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly FoodFactory foodFactory;
        private readonly AnimalFactory animalFactory;

        public Engine()
        {
            foodFactory = new FoodFactory();
            animalFactory = new AnimalFactory();
        }

        public void Run()
        {
            List<Animal> animals = new List<Animal>();
            
            Animal currentAnimal = null;

            while (true)
            {
                if (currentAnimal != null)
                {
                    animals.Add(currentAnimal);
                }
                
                try
                {
                    string input = Console.ReadLine();

                    if (input == "End")
                    {
                        break;
                    }

                    string[] animalDetails = input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                    currentAnimal = animalFactory.CreateAnimal(animalDetails);

                    input = Console.ReadLine();

                    string[] foodDetails = input
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                    string foodType = foodDetails[0];
                    int quantity = int.Parse(foodDetails[1]);

                    Food currentFood = foodFactory.CreateFood(foodType, quantity);

                    Console.WriteLine(currentAnimal.ProduceSound());
                    currentAnimal.Feed(currentFood);
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }
}
