using NUnit.Framework;
using System;

namespace Tests
{
    public class CarTests
    {
        private Car car;
        private const string TEST_MAKE = "Audi";
        private const string TEST_MODEL = "A6 3.0 tdi quattro";
        private const double FUEL_CONSUMPTION = 10;
        private const double FUEL_CAPACITY = 100;

        [SetUp]
        public void Setup()
        {
            car = new Car(TEST_MAKE, TEST_MODEL, FUEL_CONSUMPTION, FUEL_CAPACITY);
        }

        [Test]
        public void ConstructorTest()
        {
            string expectedMake = TEST_MAKE;
            string expectedModel = TEST_MODEL;
            double expectedFuelConsumption = FUEL_CONSUMPTION;
            double expectedFuelCapacity = FUEL_CAPACITY;

            string actualMake = this.car.Make;
            string actualModel = this.car.Model;
            double actualFuelConsumption = this.car.FuelConsumption;
            double actualFuelCapacity = this.car.FuelCapacity;

            Assert.AreEqual(actualMake, expectedMake);
            Assert.AreEqual(actualModel, expectedModel);
            Assert.AreEqual(actualFuelConsumption, expectedFuelConsumption);
            Assert.AreEqual(actualFuelCapacity, expectedFuelCapacity);
            
            Assert.Pass();
        }

        [Test]
        public void MakePropertyGetterTest()
        {
            string expectedMake = TEST_MAKE;
            string actualMake = this.car.Make;

            Assert.AreEqual(actualMake, expectedMake);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void SettingMakePropertyWithNullOrEmptyShouldTrowException(string make)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, TEST_MODEL, FUEL_CONSUMPTION, FUEL_CAPACITY));
        }

        [Test]
        public void ModelPropertyGetterTest()
        {
            string expectedModel = TEST_MODEL;
            string actualModel = this.car.Model;

            Assert.AreEqual(actualModel, expectedModel);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void SettingModelPropertyWithNullOrEmptyShouldTrowException(string model)
        {
            Assert.Throws<ArgumentException>(() => new Car(TEST_MAKE, model, FUEL_CONSUMPTION, FUEL_CAPACITY));
        }

        [Test]
        public void FuelConsumptionPropertyGetterTest()
        {
            double expectedFuelConsumption = FUEL_CONSUMPTION;
            double actualFuelConsumption = this.car.FuelConsumption;

            Assert.AreEqual(actualFuelConsumption, expectedFuelConsumption);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void SettingFuelConsumptionPropertyWithZeroOrNegativeShouldTrowException(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => new Car(TEST_MAKE, TEST_MODEL, fuelConsumption, FUEL_CAPACITY));
        }

        [Test]
        public void FuelCapacityPropertyGetterTest()
        {
            double expectedFuelCapacity = FUEL_CAPACITY;
            double actualFuelCapacity = this.car.FuelCapacity;

            Assert.AreEqual(actualFuelCapacity, expectedFuelCapacity);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void SettingFuelCapacityPropertyWithZeroOrNegativeShouldTrowException(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => new Car(TEST_MAKE, TEST_MODEL, FUEL_CONSUMPTION, fuelCapacity));
        }

        [Test]
        public void FuelAmountPropertyGetterTest()
        {
            double expectedFuelAmount = 0;
            double actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(actualFuelAmount, expectedFuelAmount);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void RefuelWithZeroOrNegativeAmmountShouldThrowException(double fuel)
        {
            Assert.Throws<ArgumentException>(() => this.car.Refuel(fuel));
        }

        [Test]
        [TestCase(10)]
        [TestCase(FUEL_CAPACITY)]
        public void RefuelLessOrEqualToFuelCapacity(double fuel)
        {
            this.car.Refuel(fuel);

            double expectedFuelAmount = fuel;
            double actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(actualFuelAmount, expectedFuelAmount);
        }

        [Test]
        [TestCase(FUEL_CAPACITY + 1)]
        public void RefuelMoreThanFuelCapacity(double fuel)
        {
            this.car.Refuel(fuel);

            double expectedFuelAmount = FUEL_CAPACITY;
            double actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(actualFuelAmount, expectedFuelAmount);
        }

        [Test]
        [TestCase(100)]
        [TestCase(1000)]
        public void DriveWithEnoughFuel(double distance)
        {
            this.car.Refuel(FUEL_CAPACITY);

            this.car.Drive(distance);

            double expectedFuelAmount = FUEL_CAPACITY - (distance / 100) * FUEL_CONSUMPTION;
            double actualFuelAmount = this.car.FuelAmount;

            Assert.AreEqual(actualFuelAmount, expectedFuelAmount);
        }

        [Test]
        [TestCase(100)]
        public void DrivingWithLessThanNeededFuelAmountShouldThrowException(double distance)
        {
            Assert.Throws<InvalidOperationException>(() => this.car.Drive(distance));
        }


    }
}