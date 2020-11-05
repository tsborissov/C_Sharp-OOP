using System;

namespace PizzaCalories
{
    public class Topping
    {
        private string type;
        private double weight;

        public Topping(string type, double weight)
        {
            this.Type = type;
            this.Weight = weight;
        }

        public string Type
        { 
            get
            {
                return this.type;
            }
            private set
            {
                if (!Enum.TryParse(typeof(ToppingModifiers), value, true, out _))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                this.type = value;
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
                    throw new ArgumentException($"{this.Type} weight should be in the range[1..50].");
                }

                this.weight = value;
            }
        }

        public double CalculateToppingCallories()
        {
            double toppingModifier = (int)Enum.Parse(typeof(ToppingModifiers), this.Type, true) * 1.0 / 10;

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
