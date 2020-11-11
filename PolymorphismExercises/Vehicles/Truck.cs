namespace Vehicles
{
    public class Truck : Vehicle
    {
        private const double AirCondAdditionalConsumption = 1.6;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
            this.FuelConsumption += AirCondAdditionalConsumption;
        }

        public override void Refuel(double fuel)
        {
            ValidateFuel(fuel);
            
            fuel *= 0.95;

            this.FuelQuantity += fuel;
        }
    }
}




