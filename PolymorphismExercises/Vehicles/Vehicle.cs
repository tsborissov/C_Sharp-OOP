using System;

namespace Vehicles
{
    public abstract class Vehicle
    {
        private double fuelQuantity;
        private double fuelConsumption;
        private double tankCapacity;

        protected Vehicle()
        {
        }

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }

        // public bool IsEmpty { get; set; }

        public double FuelQuantity
        {
            get { return this.fuelQuantity; }
            protected set
            {
                if (value <= this.tankCapacity)
                {
                    this.fuelQuantity = value;
                }
                else
                {
                    this.fuelQuantity = 0;
                }
            }
        }


        public double FuelConsumption
        {
            get { return this.fuelConsumption; }
            protected set { this.fuelConsumption = value; }
        }

        public double TankCapacity
        {
            get { return tankCapacity; }
            private set { tankCapacity = value; }
        }

        public virtual bool Drive(double distance)
        {
            bool canDrive = this.FuelQuantity - this.fuelConsumption * distance >= 0;

            if (canDrive)
            {
                this.fuelQuantity -= this.FuelConsumption * distance;

                return true;
            }

            return false;
        }

        public virtual void Refuel(double fuel)
        {
            ValidateFuel(fuel);

            this.FuelQuantity += fuel;
        }

        protected void ValidateFuel(double fuel)
        {
            if (fuel <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }

            bool canRefuel = fuel + this.FuelQuantity <= this.TankCapacity;

            if (!canRefuel)
            {
                throw new ArgumentException($"Cannot fit {fuel} fuel in the tank");
            }
        }
    }
}
