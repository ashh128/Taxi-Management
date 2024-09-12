using System;
using System.Collections.Generic;

namespace TaxiManagement
{

    public class TransactionManager
    {
        private readonly List<Transaction> _transactions;

        public TransactionManager()
        {
            _transactions = new List<Transaction>();
        }

        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly(); 

        public void RecordJoin(int taxiNum, int rankId)
        {
            var transaction = new JoinTransaction(DateTime.Now, taxiNum, rankId);
            _transactions.Add(transaction);
        }

        public void RecordLeave(DateTime transactionDatetime, Taxi taxi, int rankId)
        {
            if (!string.IsNullOrEmpty(taxi.Destination))
            {
                taxi.DropFare(true);
            }

            var transaction = new LeaveTransaction(transactionDatetime, rankId, taxi);
            _transactions.Add(transaction);
        }


        public void RecordDrop(int taxiNum, bool priceWasPaid)
        {
            var transaction = new DropTransaction(DateTime.Now, taxiNum, priceWasPaid);
            _transactions.Add(transaction);
        }

    }
}