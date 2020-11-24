using NUnit.Framework;
using System;

namespace Tests
{
    public class DatabaseTests
    {
        private Database database;
        private readonly int[] initialData = new int[] { 1, 2 };
        private const int DATABASE_CAPACITY = 16;

        [SetUp]
        public void Setup()
        {
            this.database = new Database(initialData);
        }

        [Test]
        [TestCase(new int[] {})]
        [TestCase(new int[] { 1, 2, 3})]
        
        public void TestIfConstructorWorksCorrectly(int[] data)
        {
            this.database = new Database(data);

            int expectedCount = data.Length;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ConstructorShouldThrowExceptionWithBiggerCollection()
        {
            int[] data = new int[DATABASE_CAPACITY + 1];

            Assert.Throws<InvalidOperationException>(() => this.database = new Database(data));
        }

        [Test]
        public void AddShouldIncreaseCountWhenAddedSuccessfully()
        {
            this.database.Add(3);

            int expectedCount = 3;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddShouldThrowExceptionWhenDatabaseFull()
        {
            int[] data = new int[DATABASE_CAPACITY];

            this.database = new Database(data);

            Assert.Throws<InvalidOperationException>(() => database.Add(17));
        }

        [Test]
        public void RemoveShouldDecreaseCountWhenSuccess()
        {
            this.database.Remove();

            int expectedCount = 1;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveShouldThrowExceptionWhenEmpty()
        {
            this.database.Remove();
            this.database.Remove();

            Assert.Throws<InvalidOperationException>(() => this.database.Remove());
        }

        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { int.MinValue, 0, int.MaxValue })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16})]
        public void FetchShouldReturnCopyOfData(int[] expectedData)
        {
            this.database = new Database(expectedData);

            int[] actualData = this.database.Fetch();

            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}