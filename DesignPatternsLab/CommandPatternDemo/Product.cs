using System;

namespace CommandPatternDemo
{
    public class Product
    {

        public Product(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }
        
        public string Name { get; set; }
        public int Price { get; set; }

        public void IncreasePrice(int amount)
        {
            this.Price += amount;
            Console.WriteLine($"The price for {this.Name} has been increased by {amount}$.");
        }

        public void DecreasePrice(int amount)
        {
            if (amount < Price)
            {
                this.Price -= amount;
                Console.WriteLine($"The price for {this.Name} has been decreased by{amount}$.");
            }
        }

        public override string ToString()
        {
            return $"Current price for {this.Name} product is {this.Price}$.";
        }
    }
}
