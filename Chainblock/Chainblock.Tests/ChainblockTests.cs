using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;


using Chainblock.Contracts;
using Chainblock.Models;
using Chainblock.Common;

namespace Chainblock.Tests
{

    [TestFixture]
    public class ChainblockTests
    {
        private const int TEST_ID = 1;
        private const TransactionStatus TEST_STATUS = TransactionStatus.Successfull;
        private const string TEST_SENDER = "Sender";
        private const string TEST_RECEPIENT = "Recepient";
        private const double TEST_AMOUNT = 15;

        private IChainblock chainblock;
        private ITransaction transaction;

        [SetUp]
        public void SetUp()
        {
            this.chainblock = new Core.Chainblock();
            this.transaction = new Transaction(TEST_ID, TEST_STATUS, TEST_SENDER, TEST_RECEPIENT, TEST_AMOUNT);
        }

        [Test]
        public void TestChainblockConstructor()
        {
            int expectedInitialCount = 0;

            IChainblock chainblock = new Core.Chainblock();

            Assert.AreEqual(expectedInitialCount, chainblock.Count);
        }

        [Test]
        public void AddShouldIncreaseCountWhenSuccessfull()
        {
            int expectedCount = 1;

            this.chainblock.Add(transaction);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void AddingSameTransactionWithDifferentIdShouldPass()
        {
            int expectedCount = 2;

            this.chainblock.Add(transaction);

            this.transaction = new Transaction(2, TEST_STATUS, TEST_SENDER, TEST_RECEPIENT, TEST_AMOUNT);

            this.chainblock.Add(transaction);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void AddingExistingTransactionShouldThrowException()
        {
            this.chainblock.Add(transaction);

            Assert.Throws<InvalidOperationException>(() =>
                this.chainblock.Add(transaction),
                ExceptionMessages.AddingExistingTransactionMessage);
        }

        [Test]
        public void ContainsByTransactionShouldReturnTrueIfTransactionFound()
        {
            chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsByTransactionShouldReturnFalseWithNonExistingTransaction()
        {
            Assert.IsFalse(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsByIdShouldReturnTrueIfTransactionFound()
        {
            chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(TEST_ID));
        }

        [Test]
        [TestCase(2)]
        [TestCase(200)]
        public void ContainsByIdShouldReturnFalseIfTransactionNotFound(int id)
        {
            chainblock.Add(transaction);

            Assert.IsFalse(this.chainblock.Contains(id));
        }

        [Test]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Unauthorized)]
        [TestCase(TransactionStatus.Successfull)]

        public void ChangeTransactionStatusOfExistingTransactionShouldSucceed(TransactionStatus newStatus)
        {
            this.chainblock.Add(transaction);

            this.chainblock.ChangeTransactionStatus(TEST_ID, newStatus);

            Assert.AreEqual(newStatus, this.transaction.Status);
        }


        [Test]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Unauthorized)]
        [TestCase(TransactionStatus.Successfull)]

        public void ChangingTransactionStatusOfNonExistingTransactionShouldThrowException(TransactionStatus newStatus)
        {
            Assert.Throws<ArgumentException>(() =>
                this.chainblock.ChangeTransactionStatus(TEST_ID, newStatus),
                ExceptionMessages.NonExistingTransactionStatusChangeMessage);
        }

        [Test]
        public void RemoveExistingTransactionByIdShouldDecreaseCount()
        {
            this.chainblock.Add(transaction);
            int targetTransactionId = transaction.Id;

            int expectedCount = 0;

            this.chainblock.RemoveTransactionById(targetTransactionId);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void RemoveExistingTransactionByIdShouldRemoveIt()
        {
            this.chainblock.Add(transaction);
            int targetTransactionId = transaction.Id;

            this.chainblock.RemoveTransactionById(targetTransactionId);

            Assert.Throws<InvalidOperationException>(() =>
                this.chainblock.RemoveTransactionById(targetTransactionId),
                ExceptionMessages.NonExistingTransactionRemoveMessage);
        }

        [Test]
        public void RemoveNonExistingTransactionShouldThrowException()
        {
            int targetTransactionId = transaction.Id;

            Assert.Throws<InvalidOperationException>(() =>
                this.chainblock.RemoveTransactionById(targetTransactionId),
                ExceptionMessages.NonExistingTransactionRemoveMessage);
        }

        [Test]
        public void GetByIdExistingTransactionShouldSucceed()
        {
            this.chainblock.Add(transaction);

            int expectedId = TEST_ID;
            int actualId = transaction.Id;

            Assert.AreEqual(expectedId, actualId);
        }

        [Test]
        public void GetByIdNonExistingTransactionShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                this.chainblock.GetById(TEST_ID),
                ExceptionMessages.GettingNonExistentTransactionByIdMessage);
        }

        [Test]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Successfull)]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Unauthorized)]
        public void GetByTransactionStatusShouldReturnOrderedCollection(TransactionStatus testStatus)
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);

                if (status == testStatus)
                {
                    expectedTransactions.Add(currentTransaction);
                }
            }

            expectedTransactions = expectedTransactions.OrderByDescending(tr => tr.Amount).ToList();
            actualTransactions = chainblock.GetByTransactionStatus(testStatus).ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Successfull)]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Unauthorized)]
        public void GetByTransactionStatusWithNonExistingStatusShouldThrowException(TransactionStatus testStatus)
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                if (status != testStatus)
                {
                    chainblock.Add(currentTransaction);
                }
            }

            Assert.Throws<InvalidOperationException>(() =>
            chainblock.GetByTransactionStatus(testStatus),
            ExceptionMessages.GetByTransactionStatusWithNonExistingStatus);
        }

        [Test]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Successfull)]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Unauthorized)]
        public void GetAllSendersWithTransactionStatusShouldReturnOrderedSendersCollection(TransactionStatus transactionStatus)
        {
            ICollection<ITransaction> targetTransactions = new List<ITransaction>();
            IEnumerable<string> expectedSendersByStatus = new List<string>();
            IEnumerable<string> actualSendersByStatus = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);

                if (status == transactionStatus)
                {
                    targetTransactions.Add(currentTransaction);
                }
            }

            expectedSendersByStatus = targetTransactions
                .OrderByDescending(tr => tr.Amount)
                .Select(tr => tr.From);

            actualSendersByStatus = chainblock.GetAllSendersWithTransactionStatus(transactionStatus);

            CollectionAssert.AreEqual(expectedSendersByStatus, actualSendersByStatus);
        }

        [Test]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Successfull)]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Unauthorized)]
        public void GetAllSendersWithTransactionStatusShouldThrowExceptionIfNoTransaction(TransactionStatus transactionStatus)
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                if (status != transactionStatus)
                {
                    chainblock.Add(currentTransaction);
                }
            }
                Assert.Throws<InvalidOperationException>(() =>
                chainblock.GetAllSendersWithTransactionStatus(transactionStatus),
                ExceptionMessages.GetAllSendersWithTransactionStatusNotExistingStatus);
        }

        [Test]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Successfull)]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Unauthorized)]
        public void GetAllReceiversWithTransactionStatusShouldReturnOrderedSendersCollection(TransactionStatus transactionStatus)
        {
            ICollection<ITransaction> targetTransactions = new List<ITransaction>();
            IEnumerable<string> expectedReceiversByStatus = new List<string>();
            IEnumerable<string> actualReceiversByStatus = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);

                if (status == transactionStatus)
                {
                    targetTransactions.Add(currentTransaction);
                }
            }

            expectedReceiversByStatus = targetTransactions
                .OrderByDescending(tr => tr.Amount)
                .Select(tr => tr.To);

            actualReceiversByStatus = chainblock.GetAllReceiversWithTransactionStatus(transactionStatus);

            CollectionAssert.AreEqual(expectedReceiversByStatus, actualReceiversByStatus);
        }

        [Test]
        [TestCase(TransactionStatus.Failed)]
        [TestCase(TransactionStatus.Successfull)]
        [TestCase(TransactionStatus.Aborted)]
        [TestCase(TransactionStatus.Unauthorized)]
        public void GetAllReceiversWithTransactionStatusShouldThrowExceptionIfNoTransaction(TransactionStatus transactionStatus)
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                if (status != transactionStatus)
                {
                    chainblock.Add(currentTransaction);
                }
            }
            Assert.Throws<InvalidOperationException>(() =>
            chainblock.GetAllReceiversWithTransactionStatus(transactionStatus),
            ExceptionMessages.GetAllReceiversWithTransactionStatusNotExistingStatus);
        }

        [Test]
        public void GetAllOrderedByAmountDescendingThenByIdShouldReturnTransactionsCollection()
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> expectedTransactions = chainblock
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id);

            IEnumerable<ITransaction> actualTransactions = chainblock.GetAllOrderedByAmountDescendingThenById();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }
    }
}
