using System;
using NUnit.Framework;
using ExtendedDatabase;

namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private Person person;
        private Person[] persons;
        private ExtendedDatabase.ExtendedDatabase database;
        private const int DATABASE_CAPACITY = 16;

        
        [SetUp]
        public void Setup()
        {
            this.persons = new Person[DATABASE_CAPACITY];

            for (int i = 0; i < DATABASE_CAPACITY; i++)
            {
                this.persons[i] = new Person(i + 1, $"username{i + 1}");
            }

            this.database = new ExtendedDatabase.ExtendedDatabase(persons);
        }

        [Test]
        public void TestPersonConstructorWithValidData()
        {
            long expectedId = 11111111;
            string expectedUsername = "username";

            this.person = new Person(expectedId, expectedUsername);

            long actualId = person.Id;
            string actualUsername = person.UserName;

            Assert.AreEqual(actualId, expectedId);
            Assert.AreEqual(actualUsername, expectedUsername);
        }

        [Test]
        public void TestDatabaseConstructorWithValidData()
        {
            int expectedCount = DATABASE_CAPACITY;
            int actualCount = this.database.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void TestDatabaseConstructorWithEmptyData()
        {
            this.database = new ExtendedDatabase.ExtendedDatabase();

            int expectedCount = 0;
            int actualCount = this.database.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void AddShouldIncreaseCountWhenAddedSuccessfully()
        {
            this.database = new ExtendedDatabase.ExtendedDatabase();
            this.database.Add(new Person(1, "user1"));

            int expectedCount = 1;
            int actualCount = this.database.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void AddShouldThrowExceptionWhenDatabaseFull()
        {
            Assert.Throws<InvalidOperationException>(() => this.database.Add(new Person(17, "username17")));
        }

        [Test]
        public void AddUserWithSameUsernameShouldThrowException()
        {
            this.database = new ExtendedDatabase.ExtendedDatabase();
            this.database.Add(new Person(1, "username"));

            Assert.Throws<InvalidOperationException>(() => this.database.Add(new Person(2, "username")));
        }

        [Test]
        public void AddUserWithSameIdShouldThrowException()
        {
            this.database = new ExtendedDatabase.ExtendedDatabase();
            this.database.Add(new Person(1, "username"));

            Assert.Throws<InvalidOperationException>(() => this.database.Add(new Person(1, "anotherUsername")));
        }

        [Test]
        public void RemoveShouldDecreaseCountWhenSuccess()
        {
            this.database.Remove();

            int expectedCount = DATABASE_CAPACITY - 1;
            int actualCount = this.database.Count;

            Assert.AreEqual(actualCount, expectedCount);
        }

        [Test]
        public void SearchingNonExistentUsernameShouldThrowException()
        {
            string targetUsername = "anotherUsername";

            Assert.Throws<InvalidOperationException>(() => this.database.FindByUsername(targetUsername));
        }

        [Test]
        public void SearchingNullUsernameShouldThrowException()
        {
            string targetUsername = null;

            Assert.Throws<ArgumentNullException>(() => this.database.FindByUsername(targetUsername));
        }

        [Test]
        public void FindValidUserByUsername()
        {
            string expectedUsername = "username1";
            string actualUsername = this.database.FindByUsername(expectedUsername).UserName;

            Assert.AreEqual(actualUsername, expectedUsername);
        }

        [Test]
        public void FindUserById()
        {
            long expectedId = 1;
            long actualId = this.database.FindById(expectedId).Id;

            Assert.AreEqual(actualId, expectedId);
        }

        [Test]
        public void SearchingNonExistentIdShouldThrowException()
        {
            long targetId = 17;

            Assert.Throws<InvalidOperationException>(() => this.database.FindById(targetId));
        }

        [Test]
        public void SearchingNegativeIdShouldThrowException()
        {
            long targetId = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => this.database.FindById(targetId));
        }
    }
}