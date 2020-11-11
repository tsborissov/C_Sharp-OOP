using System;

using WildFarm.Models.Foods;

namespace WildFarm.Factories
{
    public class FoodFactory
    {
        public FoodFactory()
        {

        }

        public Food CreateFood(string type, int quantity)
        {
            Food food;

            switch (type)
            {
                case "Vegetable":
                    food = new Vegetable(quantity);
                    break;
                case "Fruit":
                    food = new Fruit(quantity);
                    break;
                case "Meat":
                    food = new Meat(quantity);
                    break;
                case "Seeds":
                    food = new Seeds(quantity);
                    break;
                default:
                    throw new InvalidOperationException("Invalid Food!");
            }

            return food;
        }
    }
}
