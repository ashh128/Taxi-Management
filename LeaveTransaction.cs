using System;

namespace TaxiManagement
{

    public class LeaveTransaction : Transaction
    {
        public int RankId { get; private set; }
        public Taxi Taxi { get; private set; }
        public double Fare { get; private set; } 

        public LeaveTransaction(DateTime transactionDatetime, int rankId, Taxi taxi)
            : base(transactionDatetime)
        {
            RankId = rankId;
            Taxi = taxi;
            Fare = taxi.CurrentFare; 

        }

        public override string TransactionType => "Leave";

        public override string ToString()
        {
            return base.ToString() + " " + TransactionType + "     - Taxi " + Taxi.Number + " from rank " + RankId +
                   " to " + Taxi.Destination + " for �" + Fare.ToString("F2");
        }
    }
}