namespace Vehicles
{
    public class Bus : Vehicle
    {
        private double airCondAdditionalConsumption = 1.4;
        private double defaultFuelConsumption;

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
            this.defaultFuelConsumption = fuelConsumption;
            this.airCondAdditionalConsumption += fuelConsumption;
        }

        public bool IsEmpty { get; set; }

        public override bool Drive(double distance)
        {
            if (!this.IsEmpty)
            {
                this.FuelConsumption = this.airCondAdditionalConsumption;
            }
            else
            {
                this.FuelConsumption = this.defaultFuelConsumption;
            }

            return base.Drive(distance);
        }
    }
}
