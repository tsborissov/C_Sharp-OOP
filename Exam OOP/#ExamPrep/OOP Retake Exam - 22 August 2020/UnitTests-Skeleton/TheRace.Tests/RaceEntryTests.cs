using System;
using NUnit.Framework;
using TheRace;

namespace TheRace.Tests
{
    [TestFixture]
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_IfSuccessfullCounterShouldBeZero()
        {
            var race = new RaceEntry();

            var expectedCounter = 0;
            var actualCounter = race.Counter;
            
            Assert.AreEqual(actualCounter, expectedCounter);
        }

        [Test]
        public void AddDriver_IfSuccessfullCounterShouldIncrease()
        {
            var car = new UnitCar("TestModel", 100, 2000);
            var driver = new UnitDriver("TestDriver", car);
            var race = new RaceEntry();

            race.AddDriver(driver);

            int expectedCount = 1;
            int actualCount = race.Counter;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void AddDriver_IfSuccessfullSuccessMessageShouldBeReturned()
        {
            var car = new UnitCar("TestModel", 100, 2000);
            var driver = new UnitDriver("TestDriver", car);
            var race = new RaceEntry();

            string expectedSuccessMessage = $"Driver {driver.Name} added in race.";
            string actualSuccessMessage = race.AddDriver(driver);

            Assert.AreEqual(actualSuccessMessage, expectedSuccessMessage);
        }

        [Test]
        public void AddDriver_NullDriverShouldThrowException()
        {
            UnitDriver driver = null;
            RaceEntry race = new RaceEntry();

            Assert.Throws<InvalidOperationException>(() => race.AddDriver(driver), "Driver cannot be null.");
        }

        [Test]
        public void AddDriver_AddingExistingDriverShouldThrowException()
        {
            var car = new UnitCar("TestModel", 100, 2000);
            var driver = new UnitDriver("TestDriver", car);
            var race = new RaceEntry();

            race.AddDriver(driver);

            Assert.Throws<InvalidOperationException>(() => race.AddDriver(driver), $"Driver {driver.Name} is already added.");
        }

        [Test]
        public void CalculateAverageHorsePower_IfSuccessfullCalculatedAverageHorsePowerShouldBeReturned()
        {
            var firstCar = new UnitCar("FirstTestModel", 100, 2000);
            var secondCar = new UnitCar("SecondTestModel", 300, 3000);
            var firstDriver = new UnitDriver("FirstTestDriver", firstCar);
            var secondDriver = new UnitDriver("SecondTestDriver", secondCar);
            var race = new RaceEntry();
            race.AddDriver(firstDriver);
            race.AddDriver(secondDriver);

            double expectedAverageHorsePower = (firstCar.HorsePower + secondCar.HorsePower) / 2;
            double actualAverageHorsePower = race.CalculateAverageHorsePower();

            Assert.AreEqual(actualAverageHorsePower, expectedAverageHorsePower);
        }

        [Test]
        public void CalculateAverageHorsePower_NotEnoughDriversShouldThrowException()
        {
            int MinParticipants = 2;
            string RaceInvalid = "The race cannot start with less than {0} participants.";

            var firstCar = new UnitCar("FirstTestModel", 100, 2000);
            var firstDriver = new UnitDriver("FirstTestDriver", firstCar);
            var race = new RaceEntry();
            race.AddDriver(firstDriver);

            Assert.Throws<InvalidOperationException>(() => race.CalculateAverageHorsePower(), string.Format(RaceInvalid, MinParticipants));
        }
    }
}