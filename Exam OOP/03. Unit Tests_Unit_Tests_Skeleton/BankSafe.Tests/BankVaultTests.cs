using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    [TestFixture]
    public class BankVaultTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_SuccessfullShouldInitializeInnerDictionaryVaultCellsCountShouldBe12()
        {
            BankVault bankVault = new BankVault();

            int expectedCount = 12;
            int actualCount = bankVault.VaultCells.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddItem_NotExistingCellShouldThrowException()
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            string targetCell = "NonExistingCell";

            Assert.Throws<ArgumentException>(() => bankVault.AddItem(targetCell, testItem), "Cell doesn't exists!");
        }

        [Test]
        [TestCase("A1")]
        [TestCase("A2")]
        [TestCase("A3")]
        [TestCase("A4")]
        [TestCase("B1")]
        [TestCase("B2")]
        [TestCase("B3")]
        [TestCase("B4")]
        [TestCase("C1")]
        [TestCase("C2")]
        [TestCase("C3")]
        [TestCase("C4")]
        public void AddItem_AddingToOccupiedCellShouldThrowException(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            Item anotherItem = new Item("AnotherOwner", "AnotherTestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(targetCell, testItem);

            Assert.Throws<ArgumentException>(() => bankVault.AddItem(targetCell, anotherItem), "Cell is already taken!");
        }

        [Test]
        [TestCase("A1", "A2")]
        [TestCase("A2", "A3")]
        [TestCase("A3", "A4")]
        [TestCase("A4", "B1")]
        [TestCase("B1", "B2")]
        [TestCase("B2", "B3")]
        [TestCase("B3", "B4")]
        [TestCase("B4", "C1")]
        [TestCase("C1", "C2")]
        [TestCase("C2", "C3")]
        [TestCase("C3", "C4")]
        [TestCase("C4", "A1")]
        public void AddItem_AddingSameItemWithSameIdToAnotherCellShouldThrowException(string firstCell, string secondCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(firstCell, testItem);

            Assert.Throws<InvalidOperationException>(() => bankVault.AddItem(secondCell, testItem), "Item is already in cell!");
        }

        [Test]
        [TestCase("A1")]
        [TestCase("A2")]
        [TestCase("A3")]
        [TestCase("A4")]
        [TestCase("B1")]
        [TestCase("B2")]
        [TestCase("B3")]
        [TestCase("B4")]
        [TestCase("C1")]
        [TestCase("C2")]
        [TestCase("C3")]
        [TestCase("C4")]
        public void AddItem_IfSuccessfullSuccessConfirmationShouldBeReturned(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            string expectedConfirmation = $"Item:{testItem.ItemId} saved successfully!";
            string actualConfirmation = bankVault.AddItem(targetCell, testItem);

            Assert.AreEqual(expectedConfirmation, actualConfirmation);
        }

        [Test]
        [TestCase("A1")]
        [TestCase("A2")]
        [TestCase("A3")]
        [TestCase("A4")]
        [TestCase("B1")]
        [TestCase("B2")]
        [TestCase("B3")]
        [TestCase("B4")]
        [TestCase("C1")]
        [TestCase("C2")]
        [TestCase("C3")]
        [TestCase("C4")]
        public void AddItem_IfSuccessfullRetrievedItemShouldBeEqualToAdded(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(targetCell, testItem);

            string expectedOwner = "TestOwner";
            string expectedID = "TestID";

            string actualOwner = bankVault.VaultCells[targetCell].Owner;
            string actualId = bankVault.VaultCells[targetCell].ItemId;

            Assert.AreEqual(expectedOwner, actualOwner);
            Assert.AreEqual(expectedID, actualId);
            Assert.AreSame(testItem, bankVault.VaultCells[targetCell]);
        }

        [Test]
        [TestCase("A1")]
        
        public void RemoveItem_RemovingFromNotExistingCellShouldThrowException(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(targetCell, testItem);

            string cellToRemoveFrom = "NotExistingCell";

            Assert.Throws<ArgumentException>(() => bankVault.RemoveItem(cellToRemoveFrom, testItem), "Cell doesn't exists!");
        }

        [Test]
        [TestCase("A1")]

        public void RemoveItem_RemovingNotExistingItemShouldThrowException(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            Item anotherItem = new Item("AnotherOwner", "AnotherTestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(targetCell, testItem);

            Assert.Throws<ArgumentException>(() => bankVault.RemoveItem(targetCell, anotherItem), $"Item in that cell doesn't exists!");
        }

        [Test]
        [TestCase("A1")]
        [TestCase("A2")]
        [TestCase("A3")]
        [TestCase("A4")]
        [TestCase("B1")]
        [TestCase("B2")]
        [TestCase("B3")]
        [TestCase("B4")]
        [TestCase("C1")]
        [TestCase("C2")]
        [TestCase("C3")]
        [TestCase("C4")]

        public void RemoveItem_IfSuccessfullTargetCellShouldBeNull(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(targetCell, testItem);
            bankVault.RemoveItem(targetCell, testItem);

            Assert.AreEqual(null, bankVault.VaultCells[targetCell]);
        }

        [Test]
        [TestCase("A1")]
       
        public void RemoveItem_IfSuccessfullConfirmationShouldBeReturned(string targetCell)
        {
            Item testItem = new Item("TestOwner", "TestID");
            BankVault bankVault = new BankVault();

            bankVault.AddItem(targetCell, testItem);

            string expectedMessage = $"Remove item:{testItem.ItemId} successfully!";
            string actualMessage = bankVault.RemoveItem(targetCell, testItem);

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}