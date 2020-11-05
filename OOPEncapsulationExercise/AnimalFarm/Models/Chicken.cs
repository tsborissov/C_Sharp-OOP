﻿using System;

namespace AnimalFarm.Models
{
    public class Chicken
    {
        private const int MinAge = 0;
        private const int MaxAge = 15;

        private string name;
        private int age;

        public Chicken(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(this.Name)} cannot be empty.");
                }
                
                this.name = value;
            }
        }

        public int Age
        {
            get => this.age;
            private set
            {
                if (value < 0 || value > 15)
                {
                    throw new ArgumentException($"{nameof(this.Age)} should be between {MinAge} and {MaxAge}.");
                }
                
                this.age = value;
            }
        }

        public double ProductPerDay
            => this.CalculateProductPerDay();

        private double CalculateProductPerDay()
        {
            if (this.age <= 3)
            {
                return 1.5;
            }
            else if (this.age <= 7)
            {
                return 2;
            }
            else if (this.age <= 11)
            {
                return 1;
            }

                return 0.75;
        }
    }
}
