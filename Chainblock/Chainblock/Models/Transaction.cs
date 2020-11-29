using Chainblock.Common;
using Chainblock.Contracts;
using System;

namespace Chainblock.Models
{

    public class Transaction : ITransaction
    {
        public Transaction(int id, TransactionStatus status, string from, string to, double amount)
        {
            this.Id = id;
            this.Status = status;
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }

        private int id;
        private string from;
        private string to;
        private double amount;


        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidIdMessage);
                }

                this.id = value;
            }
        }

        public TransactionStatus Status { get; set; }

        public string From
        {
            get => this.from;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidSenderUsernameMessage);
                }

                this.from = value;
            }
        }
        public string To
        {
            get => this.to;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRecepientUsernameMessage);
                }

                this.to = value;
            }
        }
        public double Amount 
        {
            get => this.amount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTransactionAmountMessage);
                }

                this.amount = value;
            }
        }
    }
}
