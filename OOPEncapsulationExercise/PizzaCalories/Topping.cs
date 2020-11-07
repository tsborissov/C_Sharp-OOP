using System;

namespace PizzaCalories
{
    public class Topping
    {
        private string name;
        private double weight;

        public Topping(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name
        { 
            get
            {
                return this.name;
            }
            private set
            {
                if (!Enum.TryParse(typeof(ToppingModifiers), value, true, out _))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                this.name = value;
            }
        }
        public double Weight
        {
            get
            {
                return this.weight;
            }
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.Name} weight should be in the range [1..50].");
                }

                this.weight = value;
            }
        }

        public double CalculateToppingCallories()
        {
            double toppingModifier = (int)Enum.Parse(typeof(ToppingModifiers), this.Name, true) * 1.0 / 10;

            return 2 * this.Weight * toppingModifier;
        }

        private enum ToppingModifiers
        {
            Veggies = 8,
            Sauce = 9,
            Cheese = 11,
            Meat = 12
        }
    }
}
