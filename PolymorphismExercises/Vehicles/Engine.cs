using System;
using System.Linq;
using System.Text;

namespace Vehicles
{
    public class Engine
    {
        public void Run()
        {
            string[] carInfo = Console.ReadLine()
                .Split()
                .ToArray();

            string[] truckInfo = Console.ReadLine()
                .Split()
                .ToArray();

            string[] busInfo = Console.ReadLine()
                .Split()
                .ToArray();

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carFuelConsumption = double.Parse(carInfo[2]);
            double carTankCapacity = double.Parse(carInfo[3]);

            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckFuelConsumption = double.Parse(truckInfo[2]);
            double truckTankCapacity = double.Parse(truckInfo[3]);

            double busFuelQuantity = double.Parse(busInfo[1]);
            double busFuelConsumption = double.Parse(busInfo[2]);
            double busTankCapacity = double.Parse(busInfo[3]);

            var car = new Car(carFuelQuantity, carFuelConsumption, carTankCapacity);
            var truck = new Truck(truckFuelQuantity, truckFuelConsumption, truckTankCapacity);
            var bus = new Bus(busFuelQuantity, busFuelConsumption, busTankCapacity);

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                try
                {
                    string[] input = Console.ReadLine()
                .Split()
                .ToArray();

                    string command = input[0];
                    string vehicleType = input[1];
                    double value = double.Parse(input[2]);

                    if (command == "Drive")
                    {
                        if (vehicleType == "Car")
                        {
                            DriveVehicle(car, value);
                        }
                        else if (vehicleType == "Truck")
                        {
                            DriveVehicle(truck, value);
                        }
                        else if (vehicleType == "Bus")
                        {
                            bus.IsEmpty = false;

                            DriveVehicle(bus, value);
                        }
                    }
                    else if (command == "DriveEmpty")
                    {
                        bus.IsEmpty = true;

                        DriveVehicle(bus, value);
                    }
                    else if (command == "Refuel")
                    {
                        if (vehicleType == "Car")
                        {
                            car.Refuel(value);
                            
                        }
                        else if (vehicleType == "Truck")
                        {
                            truck.Refuel(value);
                        }
                        else if (vehicleType == "Bus")
                        {
                            bus.Refuel(value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine($"Car: {Math.Round(car.FuelQuantity, 2, MidpointRounding.AwayFromZero):F2}");
            Console.WriteLine($"Truck: {Math.Round(truck.FuelQuantity, 2, MidpointRounding.AwayFromZero):F2}");
            Console.WriteLine($"Bus: {Math.Round(bus.FuelQuantity, 2, MidpointRounding.AwayFromZero):F2}");

        }

        void DriveVehicle(Vehicle vehicle, double value)
        {
            bool canTravel = vehicle.Drive(value);

            string result = !canTravel 
                ? $"{vehicle.GetType().Name} needs refueling"
                : $"{vehicle.GetType().Name} travelled {value} km";


            Console.WriteLine(result);
        }
    }
}
