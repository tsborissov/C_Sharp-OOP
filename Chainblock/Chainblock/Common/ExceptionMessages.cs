using System;
using System.Collections.Generic;
using System.Text;

namespace Chainblock.Common
{
    public static class ExceptionMessages
    {
        public static string InvalidIdMessage = "ID cannot be zero or negative!";

        public static string InvalidSenderUsernameMessage = "Sender cannot be empty or white space!";

        public static string InvalidRecepientUsernameMessage = "Receiver cannot be empty or white space!";

        public static string InvalidTransactionAmountMessage = "Ammount cannot be zero or negative!";

        public static string AddingExistingTransactionMessage = "Transaction already exists!";

        public static string NonExistingTransactionStatusChangeMessage = "Cannot change status of non existent transaction!";

        public static string NonExistingTransactionRemoveMessage = "Cannot remove non existent transaction!";

        public static string GettingNonExistentTransactionByIdMessage = "No such transaction exists!";

        public static string GetByTransactionStatusWithNonExistingStatus = "No transactions with provided status!";

        public static string GetAllSendersWithTransactionStatusNotExistingStatus = "No transactions with provided status!";

        public static string GetAllReceiversWithTransactionStatusNotExistingStatus = "No transactions with provided status!";
        
        public static string EmptyGetBySenderOrderedByAmountDescendingMessage = "No transactions for the given sender found!";
        
        public static string EmptyGetByReceiverOrderedByAmountThenByIdMessage = "No transactions for the given receiver found!";
        
        public static string EmptyGetBySenderAndMinimumAmountDescendingMessage = "No transactions found!";
        
        public static string EmptyGetByReceiverAndAmountRangeMessage = "No transactions found!";



    }
}
