namespace Computers.Tests
{
    using System;
    using NUnit.Framework;

    public class ComputerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_SetCorrectNameProperty()
        {
            Computer computer = new Computer("TestComputer");

            string expectedName = "TestComputer";
            string actualName = computer.Name;

            Assert.AreEqual(actualName, expectedName);
        }

        [Test]
        public void Constructor_PartsCollectionIsEmpty()
        {
            Computer computer = new Computer("TestComputer");
            
            Assert.IsEmpty(computer.Parts);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void NameProperty_EmptyOrNullValueShouldThrowException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Computer(name), "Name cannot be null or empty!");
        }

        [Test]
        public void AddPart_IfSuccessfullPartsCountShouldIncrease()
        {
            Part part = new Part("TestPart", 100);
            Computer computer = new Computer("TestComputer");

            int expectedPartsCount = computer.Parts.Count + 1;

            computer.AddPart(part);

            int actualPartsCount = computer.Parts.Count;

            Assert.AreEqual(actualPartsCount, expectedPartsCount);
        }
        
        [Test]
        public void AddPart_NullPartShouldThrowException()
        {
            Part part = null;
            Computer computer = new Computer("TestComputer");

            Assert.Throws<InvalidOperationException>(() => computer.AddPart(part), "Cannot add null!");
        }

        [Test]
        [TestCase(0.01)]
        [TestCase(100.11)]

        public void TotalPrice_ShouldCalculateAllPartsPricesSum(decimal testPrice)
        {
            int testPartsCount = 10;
            Computer computer = new Computer("TestComputer");

            for (int i = 1; i <= testPartsCount; i++)
            {
                computer.AddPart(new Part($"TestPart_{i}", testPrice));
            }

            decimal expectedTotalPrice = testPartsCount * testPrice;
            decimal actualTotalPrice = computer.TotalPrice;

            Assert.AreEqual(actualTotalPrice, expectedTotalPrice);
        }

        [Test]
        public void RemovePart_ShouldDecreasePartsCount()
        {
            Part part = new Part("TestPart", 100);
            Computer computer = new Computer("TestComputer");
            computer.AddPart(part);

            int expectedCount = computer.Parts.Count - 1;

            computer.RemovePart(part);

            int actualCount = computer.Parts.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void RemovePart_SuccessfullShouldReturnTrue()
        {
            Part part = new Part("TestPart", 100);
            Computer computer = new Computer("TestComputer");
            computer.AddPart(part);

            Assert.IsTrue(computer.RemovePart(part));
        }

        [Test]
        public void RemovePart_UnsuccessfullShouldReturnFalse()
        {
            Part part = new Part("TestPart", 100);
            Computer computer = new Computer("TestComputer");
            computer.AddPart(part);
            computer.RemovePart(part);

            Assert.IsFalse(computer.RemovePart(part));
        }

        [Test]
        public void GetPart_IfSuccessfullShouldReturnCorrectPart()
        {
            Part part = new Part("TestPart", 100);
            Computer computer = new Computer("TestComputer");
            computer.AddPart(part);

            Part expectedPart = part;
            Part actualPart = computer.GetPart(part.Name);

            Assert.AreEqual(actualPart, expectedPart);
        }

        [Test]
        public void GetPart_IfNotFoundShouldReturnNull()
        {
            Part part = new Part("TestPart", 100);
            Part anotherPart = new Part("AnotherPart", 100);
            Computer computer = new Computer("TestComputer");
            computer.AddPart(part);

            Assert.IsNull(computer.GetPart(anotherPart.Name));
        }
    }
}