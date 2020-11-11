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
            
            int i = 0;

            Animal currentAnimal = null;

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] cmdArgs = command
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                try
                {
                    if (i % 2 == 0)
                    {
                        currentAnimal = animalFactory.CreateAnimal(cmdArgs);
                    }
                    else
                    {
                        string foodType = cmdArgs[0];
                        int quantity = int.Parse(cmdArgs[1]);

                        Food currentFood = foodFactory.CreateFood(foodType, quantity);

                        Console.WriteLine(currentAnimal.ProduceSound());
                        currentAnimal.Feed(currentFood);
                        animals.Add(currentAnimal);
                    }
                }
                catch (Exception ex)
                {
                    animals.Add(currentAnimal);
                    Console.WriteLine(ex.Message);
                }

                i++;
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }
}
