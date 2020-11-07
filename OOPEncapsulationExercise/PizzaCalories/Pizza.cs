using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaCalories
{
    public class Pizza
    {

        private string name;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            this.name = name;
            this.Dough = dough;
            toppings = new List<Topping>();
        }


        public string Name 
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                
                this.name = value;
            }
        }

        public Dough Dough { get; }

        public int ToppingsCount
            => this.toppings.Count;


        public double TotalCalories()
        {
            return this.Dough.GetTotalCalories() + this.toppings.Sum(x => x.CalculateToppingCallories());
        }

        public void AddTopping(Topping topping)
        {
            if (this.ToppingsCount == 10)
            {
                throw new Exception("Number of toppings should be in range [0..10].");
            }

            this.toppings.Add(topping);
        }
    }
}
