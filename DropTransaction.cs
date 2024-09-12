using System;

namespace TaxiManagement
{

    public class DropTransaction : Transaction
    {
        public int TaxiNum { get; private set; }
        public bool PriceWasPaid { get; private set; }

        public DropTransaction(DateTime transactionDatetime, int taxiNum, bool priceWasPaid)
            : base(transactionDatetime)
        {
            TaxiNum = taxiNum;
            PriceWasPaid = priceWasPaid;
        }

        public override string TransactionType => "Drop fare";

        public override string ToString()
        {
            return base.ToString() + " " + TransactionType + " - Taxi " + TaxiNum + ", price was " + (PriceWasPaid ? "paid" : "not paid");
        }
    }
}