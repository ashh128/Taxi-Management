using System;
using System.Collections.Generic;

namespace TaxiManagement
{
    public class Rank
    {
        public int Id { get; }

        private int numberOfTaxiSpaces;
        private List<Taxi> taxis;

        public Rank(int id, int numberOfTaxiSpaces)
        {
            Id = id;
            this.numberOfTaxiSpaces = numberOfTaxiSpaces;
            taxis = new List<Taxi>(numberOfTaxiSpaces);
        }

        public bool AddTaxi(Taxi t)
        {
            if (taxis.Count < numberOfTaxiSpaces)
            {
                taxis.Add(t);
                t.Rank = this;
                return true;
            }
            return false;
        }

        public Taxi FrontTaxiTakesFare(string destination, double agreedPrice)
        {
            try
            {
                Taxi t = taxis[0];
                t.AddFare(destination, agreedPrice);
                taxis.RemoveAt(0);
                return t;
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
           
        }
    }
}
