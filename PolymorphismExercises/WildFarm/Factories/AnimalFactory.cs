using System;
using WildFarm.Models.Animals;

namespace WildFarm.Factories
{
    public class AnimalFactory
    {
        public AnimalFactory()
        {

        }

        public Animal CreateAnimal(string[] arguments)
        {
            Animal animal;

            string type = arguments[0];
            string name = arguments[1];
            double weight = double.Parse(arguments[2]);
            string livingRegion;
            string breed;
            double wingSize;

            switch (type)
            {
                case "Owl":
                    wingSize = double.Parse(arguments[3]);
                    animal = new Owl(name, weight, wingSize);
                    break;
                case "Hen":
                    wingSize = double.Parse(arguments[3]);
                    animal = new Hen(name, weight, wingSize);
                    break;
                case "Mouse":
                    livingRegion = arguments[3];
                    animal = new Mouse(name, weight, livingRegion);
                    break;
                case "Dog":
                    livingRegion = arguments[3];
                    animal = new Dog(name, weight, livingRegion);
                    break;
                case "Cat":
                    livingRegion = arguments[3];
                    breed = arguments[4];
                    animal = new Cat(name, weight, livingRegion, breed);
                    break;
                case "Tiger":
                    livingRegion = arguments[3];
                    breed = arguments[4];
                    animal = new Tiger(name, weight, livingRegion, breed);
                    break;
                default:
                    throw new InvalidOperationException("Invalid Animal!");
            }

            return animal;
        }
    }
}
