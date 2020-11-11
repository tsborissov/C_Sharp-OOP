using WildFarm.Models.Foods.Interfaces;

namespace WildFarm.Models.Foods
{
    
    public abstract class Food : IFood
    {
        public Food(int quantity)
        {
            this.Quantity = quantity;
        }

        public int Quantity { get; }
    }
}
