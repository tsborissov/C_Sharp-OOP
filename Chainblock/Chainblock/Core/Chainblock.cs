using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Chainblock.Common;
using Chainblock.Contracts;


namespace Chainblock.Core
{
    public class Chainblock : IChainblock
    {

        private ICollection<ITransaction> transactions;

        public Chainblock()
        {
            this.transactions = new List<ITransaction>();
        }
        
        public int Count => this.transactions.Count;


        public void Add(ITransaction tx)
        {
            if (this.Contains(tx))
            {
                throw new InvalidOperationException(ExceptionMessages.AddingExistingTransactionMessage);
            }

            this.transactions.Add(tx);
        }

        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            ITransaction targetTransaction = transactions.FirstOrDefault(tx => tx.Id == id);

            if (targetTransaction == null)
            {
                throw new ArgumentException(ExceptionMessages.NonExistingTransactionStatusChangeMessage);
            }

            targetTransaction.Status = newStatus;
        }

        public bool Contains(ITransaction tx)
        {
            return this.Contains(tx.Id);
        }

        public bool Contains(int id)
        {
            return this.transactions.Any(tx => tx.Id == id);
        }

        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id);

            return targetTransactions;
        }

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> receivers = this
                .GetByTransactionStatus(status)
                .Select(tr => tr.To);

            if (receivers.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.GetAllReceiversWithTransactionStatusNotExistingStatus);
            }

            return receivers;
        }

        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> senders = this
                .GetByTransactionStatus(status)
                .Select(tr => tr.From);

            if (senders.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.GetAllSendersWithTransactionStatusNotExistingStatus);
            }

            return senders;
        }

        public ITransaction GetById(int id)
        {
            ITransaction targetTransaction = this.transactions.FirstOrDefault(tx => tx.Id == id);

            if (targetTransaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.GettingNonExistentTransactionByIdMessage);
            }

            return targetTransaction;
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .Where(tr => tr.To == receiver)
                .Where(tr => tr.Amount >= lo)
                .Where(tr => tr.Amount < hi)
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id);

            if (!targetTransactions.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyGetByReceiverAndAmountRangeMessage);
            }

            return targetTransactions;
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .Where(tr => tr.To == receiver)
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id);

            if (!targetTransactions.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyGetByReceiverOrderedByAmountThenByIdMessage);
            }

            return targetTransactions;
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .Where(tr => tr.From == sender)
                .Where(tr => tr.Amount > amount)
                .OrderByDescending(tr => tr.Amount);

            if (!targetTransactions.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyGetBySenderAndMinimumAmountDescendingMessage);
            }

            return targetTransactions;
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .Where(tr => tr.From == sender)
                .OrderByDescending(tr => tr.Amount);

            if (!targetTransactions.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyGetBySenderOrderedByAmountDescendingMessage);
            }

            return targetTransactions;
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .Where(tr => tr.Status == status)
                .OrderByDescending(tr => tr.Amount);

            if (targetTransactions.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.GetByTransactionStatusWithNonExistingStatus);
            }

            return targetTransactions;
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            IEnumerable<ITransaction> targetTransactions = this.transactions
                .Where(tr => tr.Status == status)
                .Where(tr => tr.Amount <= amount)
                .OrderByDescending(tr => tr.Amount);

            return targetTransactions;
        }

        public IEnumerator<ITransaction> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void RemoveTransactionById(int id)
        {
            ITransaction targetTransaction = this.transactions.FirstOrDefault(tr => tr.Id == id);

            if (targetTransaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.NonExistingTransactionRemoveMessage);
            }

            this.transactions.Remove(targetTransaction);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
