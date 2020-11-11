using System;
using System.Collections.Generic;
using WildFarm.Models.Foods.Interfaces;

namespace WildFarm.Models.Animals.Interfaces
{
    public interface IFeedable
    {
        void Feed(IFood food);

        public int FoodEaten { get; }

        public double WeightMultiplier { get; }

        ICollection<Type> PrefferedFoods { get; }
        
    }
}
