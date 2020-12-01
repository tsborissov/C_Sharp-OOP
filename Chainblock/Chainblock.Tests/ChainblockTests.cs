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

                expectedTransactions.Add(currentTransaction);
                chainblock.Add(currentTransaction);
            }

            expectedTransactions = expectedTransactions.OrderByDescending(tr => tr.Amount).ThenBy(tr => tr.Id).ToList();

            actualTransactions = chainblock.GetAllOrderedByAmountDescendingThenById().ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void TestGetBySenderOrderedByAmountDescending()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 3; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 3; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);
            }

            expectedTransactions = expectedTransactions.OrderByDescending(tr => tr.Amount).ToList();
            actualTransactions = this.chainblock.GetBySenderOrderedByAmountDescending(TEST_SENDER).ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void EmptyGetBySenderOrderedByAmountDescendingShouldThrowException()
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.Throws<InvalidOperationException>(() =>
            this.chainblock.GetBySenderOrderedByAmountDescending(TEST_SENDER),
            ExceptionMessages.EmptyGetBySenderOrderedByAmountDescendingMessage);
        }

        [Test]
        public void TestGetByReceiverOrderedByAmountThenByIdShouldReturnOrderedCollection()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 3; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 3; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);
            }

            expectedTransactions = expectedTransactions.OrderByDescending(tr => tr.Amount).ThenBy(tr => tr.Id).ToList();
            actualTransactions = this.chainblock.GetByReceiverOrderedByAmountThenById(TEST_RECEPIENT).ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void EmptyGetByReceiverOrderedByAmountThenByIdShouldThrowException()
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.Throws<InvalidOperationException>(() =>
            this.chainblock.GetByReceiverOrderedByAmountThenById(TEST_RECEPIENT),
            ExceptionMessages.EmptyGetByReceiverOrderedByAmountThenByIdMessage);
        }

        [Test]
        [TestCase(TransactionStatus.Failed, 0)]
        [TestCase(TransactionStatus.Failed, 19)]
        [TestCase(TransactionStatus.Failed, 21)]
        [TestCase(TransactionStatus.Failed, 30)]
        [TestCase(TransactionStatus.Successfull, 0)]
        [TestCase(TransactionStatus.Successfull, 19)]
        [TestCase(TransactionStatus.Successfull, 21)]
        [TestCase(TransactionStatus.Successfull, 30)]
        [TestCase(TransactionStatus.Aborted, 0)]
        [TestCase(TransactionStatus.Aborted, 19)]
        [TestCase(TransactionStatus.Aborted, 21)]
        [TestCase(TransactionStatus.Aborted, 30)]
        [TestCase(TransactionStatus.Unauthorized, 0)]
        [TestCase(TransactionStatus.Unauthorized, 19)]
        [TestCase(TransactionStatus.Unauthorized, 21)]
        [TestCase(TransactionStatus.Unauthorized, 30)]
        public void TestGetByTransactionStatusAndMaximumAmountShouldReturnOrderedByAmountCollection(TransactionStatus transactionStatus, double targetAmount)
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 3; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = transactionStatus;
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 3; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                if (status == transactionStatus)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                chainblock.Add(currentTransaction);
            }

            expectedTransactions = expectedTransactions
                .Where(tr => tr.Status == transactionStatus)
                .Where(tr => tr.Amount <= targetAmount)
                .OrderByDescending(tr => tr.Amount)
                .ToList();

            actualTransactions = this.chainblock
                .GetByTransactionStatusAndMaximumAmount(transactionStatus, targetAmount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void TestGetBySenderAndMinimumAmountDescendingShouldReturnOrderedCollection(double targetAmount)
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 3; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);
                
                if (amount > targetAmount)
                {
                    expectedTransactions.Add(currentTransaction);
                }
                
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 3; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);
            }

            expectedTransactions = expectedTransactions
                .Where(tr => tr.Amount > targetAmount)
                .OrderByDescending(tr => tr.Amount)
                .ToList();


            actualTransactions = this.chainblock
                .GetBySenderAndMinimumAmountDescending(TEST_SENDER, targetAmount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void EmptyGetBySenderAndMinimumAmountDescendingNoSuchSenderShouldThrowException()
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.Throws<InvalidOperationException>(() =>
            this.chainblock.GetBySenderAndMinimumAmountDescending(TEST_SENDER, TEST_AMOUNT),
            ExceptionMessages.EmptyGetBySenderAndMinimumAmountDescendingMessage);
        }

        [Test]
        [TestCase(TEST_AMOUNT + 8)]
        public void EmptyGetBySenderAndMinimumAmountDescendingNoSuchAmountShouldThrowException(double targetAmount)
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.Throws<InvalidOperationException>(() =>
            this.chainblock.GetBySenderAndMinimumAmountDescending(TEST_SENDER, targetAmount),
            ExceptionMessages.EmptyGetBySenderAndMinimumAmountDescendingMessage);
        }

        [Test]
        [TestCase(TEST_RECEPIENT, 0, 20)]
        [TestCase(TEST_RECEPIENT, 15, 17)]
        public void TestGetByReceiverAndAmountRangeShouldReturnCollection(string receiver, double lo, double hi)
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 3; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 3; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                chainblock.Add(currentTransaction);
            }

            expectedTransactions = expectedTransactions
                .Where(tr => tr.Amount >= lo)
                .Where(tr => tr.Amount < hi)
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id)
                .ToList();


            actualTransactions = this.chainblock
                .GetByReceiverAndAmountRange(TEST_RECEPIENT, lo, hi)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        [TestCase(15, 18)]
        public void EmptyGetByReceiverAndAmountRangeNoSuchReceiverShouldThrowException(double lo, double hi)
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}{i}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.Throws<InvalidOperationException>(() =>
            this.chainblock.GetByReceiverAndAmountRange(TEST_RECEPIENT, lo, hi),
            ExceptionMessages.EmptyGetByReceiverAndAmountRangeMessage);
        }

        [Test]
        [TestCase(100, 200)]
        public void EmptyGetByReceiverAndAmountRangeNoSuchAmountShouldThrowException(double lo, double hi)
        {
            for (int i = 0; i < 7; i++)
            {
                int id = TEST_ID + i;
                TransactionStatus status = (TransactionStatus)(i / 2);
                string sender = $"{TEST_SENDER}{i}";
                string recepient = $"{TEST_RECEPIENT}";
                double amount = TEST_AMOUNT + i;

                Transaction currentTransaction = new Transaction(id, status, sender, recepient, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.Throws<InvalidOperationException>(() =>
            this.chainblock.GetByReceiverAndAmountRange(TEST_RECEPIENT, lo, hi),
            ExceptionMessages.EmptyGetByReceiverAndAmountRangeMessage);
        }

    }
}
