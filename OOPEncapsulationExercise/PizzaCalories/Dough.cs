using System;
using System.Collections.Generic;

namespace PizzaCalories
{
    public class Dough
    {
        private string flourType;
        private string backingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            this.FlourType = flourType;
            this.BackingTechnique = bakingTechnique;
            this.Weight = weight;
        }

        public string FlourType
        {
            get 
            {
                return this.flourType;
            }
            set 
            {
                if (!Enum.TryParse(typeof(FlourModifiers), value, true, out _))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                this.flourType = value;
            }
        }

        public string BackingTechnique
        {
            get
            { 
                return this.backingTechnique; 
            }
            set 
            { 
                if (!Enum.TryParse(typeof(BackingModifiers), value, true, out _))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                
                this.backingTechnique = value;
            }
        }

        public double Weight
        {
            get 
            {
                return this.weight; 
            }
            set 
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }

                this.weight = value; 
            }
        }


        public double GetTotalCalories()
        {
            double flourModifier = (int)Enum.Parse(typeof(FlourModifiers), this.FlourType, true) * 1.0 / 10;
            double backingModifier = (int)Enum.Parse(typeof(BackingModifiers), this.BackingTechnique, true) * 1.0 / 10;

            return 2 * this.Weight * flourModifier * backingModifier;
        }

       
        private enum FlourModifiers
        {
            White = 15,
            Wholegrain = 10
        }

        private enum BackingModifiers
        {
            Crispy = 9,
            Chewy = 11,
            Homemade = 10
        }

    }
}
