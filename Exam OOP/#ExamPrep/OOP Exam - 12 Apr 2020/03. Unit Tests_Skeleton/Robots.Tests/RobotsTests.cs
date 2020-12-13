namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RobotsTests
    {

        [Test]
        public void RobotConstructor_Test()
        {
            string name = "TestName";
            int battery = 100;
            Robot robot = new Robot(name, battery);

            Assert.AreEqual(robot.Name, name);
            Assert.AreEqual(robot.Battery, battery);
            Assert.AreEqual(robot.MaximumBattery, battery);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void RobotManagerConstructor_WithValidCapacityShouldSetCapacity(int testCapacity)
        {
            RobotManager manager = new RobotManager(testCapacity);

            Assert.AreEqual(manager.Capacity, testCapacity);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void RobotManagerConstructor_WithValidCapacityShouldSetInitialCount(int testCapacity)
        {
            RobotManager manager = new RobotManager(testCapacity);

            int expectedCount = 0;

            Assert.AreEqual(manager.Count, expectedCount);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-100)]
        public void RobotManagerConstructor_InvalidCapacityShouldThrowException(int testCapacity)
        {
            Assert.Throws<ArgumentException>(() => new RobotManager(testCapacity), "Invalid capacity!");
        }

        [Test]
        public void AddMethod_IfSuccessfullShouldIncreaseCount()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);

            manager.Add(robot);

            int expectedCount = 1;
            int actualCount = manager.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void AddMethod_AddingExistingRobotShouldThrowException()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);

            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Add(robot), $"There is already a robot with name {robot.Name}!");
        }

        [Test]
        public void AddMethod_UnsufficientCapacityShouldThrowException()
        {
            RobotManager manager = new RobotManager(1);
            Robot robot = new Robot("TestName", 100);

            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Add(robot), "Not enough capacity!");
        }

        [Test]
        public void RemoveMethod_IfSuccessfullShouldDecreaseCount()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);

            manager.Add(robot);

            manager.Remove(robot.Name);

            int expectedCount = 0;
            int actualCount = manager.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void RemoveMethod_NotExistingRobotShouldThrowException()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);
            string nameToRemove = "SomeName";

            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Remove(nameToRemove), $"Robot with the name {nameToRemove} doesn't exist!");
        }

        [Test]
        public void WorkMethod_IfSuccessfullShouldDecreaseBattery()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);
            int batteryUsage = 10;
            manager.Add(robot);

            int expectedBattery = robot.Battery - batteryUsage;

            manager.Work(robot.Name, "TestJob", batteryUsage);

            int actualBattery = robot.Battery;

            Assert.AreEqual(actualBattery, expectedBattery);
        }

        [Test]
        public void WorkMethod_NotExistingRobotShouldThrowException()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);
            int batteryUsage = 10;
            string targetName = "SomeName";
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Work(targetName, "TestJob", batteryUsage), $"Robot with the name {targetName} doesn't exist!");
        }

        [Test]
        public void WorkMethod_NotEnoughBatteryShouldThrowException()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 5);
            int batteryUsage = 10;
            manager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => manager.Work(robot.Name, "TestJob", batteryUsage), $"{robot.Name} doesn't have enough battery!");
        }

        [Test]
        public void ChargeMethod_IfSuccessfullBatteryShouldCharge()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);
            int batteryUsage = 10;

            manager.Add(robot);
            manager.Work(robot.Name, "TestJob", batteryUsage);
            manager.Work(robot.Name, "TestJob", batteryUsage);

            manager.Charge(robot.Name);

            Assert.AreEqual(robot.Battery, robot.MaximumBattery);
        }

        [Test]
        public void ChargeMethod_NotExistingRobotShouldThrowException()
        {
            RobotManager manager = new RobotManager(10);
            Robot robot = new Robot("TestName", 100);
            int batteryUsage = 10;
            string targetName = "SomeName";

            manager.Add(robot);
            manager.Work(robot.Name, "TestJob", batteryUsage);

            Assert.Throws<InvalidOperationException>(() => manager.Charge(targetName), $"Robot with the name {targetName} doesn't exist!");
        }

    }
}
