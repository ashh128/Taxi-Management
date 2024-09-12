using System;
using System.Collections.Generic;

namespace TaxiManagement
{

    public abstract class Transaction
    {
        public DateTime TransactionDatetime { get; private set; }
        public abstract string TransactionType { get; }

        public Transaction(DateTime transactionDatetime)
        {
            TransactionDatetime = transactionDatetime;
        }

        public override string ToString()
        {
            return TransactionDatetime.ToString("dd/MM/yyyy HH:mm");
        }
    }

}

