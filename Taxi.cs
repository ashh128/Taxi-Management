using System;

namespace TaxiManagement
{
    
    public class Taxi
    {
        public const string ON_ROAD = "on the road";
        public const string IN_RANK = "in rank";

        public double CurrentFare { get; private set; } = 0;
        public string Destination { get; private set; } = "";

        public string Location { get; private set; } = ON_ROAD;
        public int Number { get; }
        public double TotalMoneyPaid { get; private set; }

        private Rank _rank;
  

        public Rank Rank
        {
            get => _rank;
            set
            {
                if (value == null)
                {
                    throw new Exception("Rank cannot be null");
                }

                if (!string.IsNullOrEmpty(Destination)) 
                {
                    throw new Exception("Cannot join rank if fare has not been dropped");
                }
                _rank = value;
                Location = IN_RANK;
                
                
            }
        }

        public Taxi(int taxiNum)
        {
            Number = taxiNum;
            CurrentFare = 0;

        }

        public void AddFare(string destination, double agreedPrice)

        {
            if (!string.IsNullOrEmpty(Destination))
            {
                throw new Exception("Cannot add fare if previous fare has not been dropped");
            }
            Destination = destination;
            CurrentFare = agreedPrice;
            _rank = null;
            
        }

        public void DropFare(bool priceWasPaid)
        {
            if (priceWasPaid && CurrentFare > 0) 
            {
                TotalMoneyPaid += CurrentFare;
            }
            CurrentFare = 0;
            Destination = "";

        }
    }
}   
