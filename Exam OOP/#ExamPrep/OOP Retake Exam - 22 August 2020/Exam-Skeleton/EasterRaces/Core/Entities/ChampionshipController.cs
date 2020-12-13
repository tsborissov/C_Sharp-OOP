using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using EasterRaces.Utilities.Messages;
using EasterRaces.Core.Contracts;
using EasterRaces.Repositories.Entities;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Core.Enums;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Contracts;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private readonly IRepository<IDriver> driverRepository;
        private readonly IRepository<ICar> carRepository;
        private readonly IRepository<IRace> raceRepository;

        public ChampionshipController()
        {
            this.driverRepository = new DriverRepository();
            this.carRepository = new CarRepository();
            this.raceRepository = new RaceRepository();
        }
        
        public string AddCarToDriver(string driverName, string carModel)
        {
            IDriver targetDriver = this.driverRepository.GetByName(driverName);

            if (targetDriver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            ICar targetCar = this.carRepository.GetByName(carModel);

            if (targetCar == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarNotFound, carModel));
            }

            targetDriver.AddCar(targetCar);

            //this.carRepository.Remove(targetCar); // TODO : ??? Should the car be removed from repository?

            return string.Format(OutputMessages.CarAdded, driverName, carModel);
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            IRace targetRace = this.raceRepository.GetByName(raceName);

            if (targetRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            IDriver targetDriver = this.driverRepository.GetByName(driverName);

            if (targetDriver == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.DriverNotFound, driverName));
            }

            targetRace.AddDriver(targetDriver);

            //this.driverRepository.Remove(targetDriver); // TODO : ??? Should the driver be removed from repository?

            return string.Format(OutputMessages.DriverAdded, driverName, raceName);
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            if (this.carRepository.GetAll().Any(x => x.Model == model))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model));
            }

            ICar currentCar = null;

            if (Enum.TryParse(type, out CarEnum carTypeEnum))
            {
                switch (carTypeEnum)
                {
                    case CarEnum.Muscle:
                        currentCar = new MuscleCar(model, horsePower);
                        break;
                    case CarEnum.Sports:
                        currentCar = new SportsCar(model, horsePower);
                        break;
                }
            }

            this.carRepository.Add(currentCar);

            return string.Format(OutputMessages.CarCreated, currentCar.GetType().Name, model);
        }

        public string CreateDriver(string driverName)
        {
            if (this.driverRepository.GetAll().Any(x => x.Name == driverName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriversExists, driverName));
            }

            this.driverRepository.Add(new Driver(driverName));

            return string.Format(OutputMessages.DriverCreated, driverName);
        }

        public string CreateRace(string name, int laps)
        {
            if (this.raceRepository.GetAll().Any(x => x.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            this.raceRepository.Add(new Race(name, laps));

            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string StartRace(string raceName)
        {
            IRace targetRace = this.raceRepository.GetByName(raceName);

            if (targetRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (targetRace.Drivers.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            int laps = targetRace.Laps;

            //List<KeyValuePair<string, double>> driversRanking = new List<KeyValuePair<string, double>>();

            //foreach (var driver in targetRace.Drivers)
            //{
            //    double points = driver.Car.CalculateRacePoints(laps);
            //    string driverName = driver.Name;

            //    driversRanking.Add(new KeyValuePair<string, double>(driverName, points));
            //}

            //driversRanking = driversRanking.OrderByDescending(x => x.Value).ToList();

            var driversRanking = targetRace.Drivers
                .OrderByDescending(x => x.Car.CalculateRacePoints(laps))
                .ToList();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format(OutputMessages.DriverFirstPosition, driversRanking[0].Name, targetRace.Name));
                //(string.Format(OutputMessages.DriverFirstPosition, driversRanking[0].Key, targetRace.Name));
            sb.AppendLine(string.Format(OutputMessages.DriverSecondPosition, driversRanking[1].Name, targetRace.Name));
                //(string.Format(OutputMessages.DriverSecondPosition, driversRanking[1].Key, targetRace.Name));
            sb.AppendLine(string.Format(OutputMessages.DriverThirdPosition, driversRanking[2].Name, targetRace.Name));
            //(string.Format(OutputMessages.DriverThirdPosition, driversRanking[2].Key, targetRace.Name));

            this.raceRepository.Remove(targetRace);

            return sb.ToString().TrimEnd();
        }
    }
}
