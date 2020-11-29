using Chainblock.Contracts;
using Chainblock.Models;
using Chainblock.Common;
using NUnit.Framework;
using System;

namespace Chainblock.Tests
{
    [TestFixture]
    class TransactionTests
    {
        private const int TEST_ID = 1;
        private const TransactionStatus TEST_STATUS = TransactionStatus.Successfull;
        private const string TEST_SENDER = "Sender";
        private const string TEST_RECEPIENT = "Recepient";
        private const double TEST_AMOUNT = 15;

        
        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            ITransaction transaction = new Transaction(TEST_ID, TEST_STATUS, TEST_SENDER, TEST_RECEPIENT, TEST_AMOUNT);

            Assert.AreEqual(TEST_ID, transaction.Id);
            Assert.AreEqual(TEST_STATUS, transaction.Status);
            Assert.AreEqual(TEST_SENDER, transaction.From);
            Assert.AreEqual(TEST_RECEPIENT, transaction.To);
            Assert.AreEqual(TEST_AMOUNT, transaction.Amount);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void InvalidIdShouldThrowException(int id)
        {
            //Assert.That(() =>
            //{
            //    ITransaction transaction = new Transaction(id, status, sender, recepient, amount);
            //}, Throws.ArgumentException.With.Message.EqualTo(ExceptionMessages.InvalidIdMessage));

            Assert.Throws<ArgumentException>(() => 
                new Transaction(id, TEST_STATUS, TEST_SENDER, TEST_RECEPIENT, TEST_AMOUNT),
                ExceptionMessages.InvalidIdMessage);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        public void InvalidSenderShouldThrowException(string sender)
        {
            Assert.Throws<ArgumentException>(() =>
                new Transaction(TEST_ID, TEST_STATUS, sender, TEST_RECEPIENT, TEST_AMOUNT),
                ExceptionMessages.InvalidSenderUsernameMessage);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("         ")]
        public void InvalidRecepientShouldThrowException(string recepient)
        {
            Assert.Throws<ArgumentException>(() =>
                new Transaction(TEST_ID, TEST_STATUS, TEST_SENDER, recepient, TEST_AMOUNT),
                ExceptionMessages.InvalidRecepientUsernameMessage);
        }

        [Test]
        [TestCase(0.0)]
        [TestCase(-0.00000001)]
        [TestCase(-1.0)]
        public void InvalidAmountShouldThrowException(double amount)
        {
            Assert.Throws<ArgumentException>(() =>
                new Transaction(TEST_ID, TEST_STATUS, TEST_SENDER, TEST_RECEPIENT, amount),
                ExceptionMessages.InvalidTransactionAmountMessage);
        }
    }
}
