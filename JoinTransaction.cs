using System;

namespace TaxiManagement
{

    public class JoinTransaction : Transaction
    {
        public int TaxiNum { get; private set; }
        public int RankId { get; private set; }

        public JoinTransaction(DateTime transactionDatetime, int taxiNum, int rankId)
            : base(transactionDatetime)
        {
            
            TaxiNum = taxiNum;
            RankId = rankId;
        }

        public override string TransactionType => "Join";

        public override string ToString()
        {
            return base.ToString() + " " + TransactionType + "      - Taxi " + TaxiNum + " in rank " + RankId;
        }
    }
}
