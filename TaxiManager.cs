using System;
using System.Collections.Generic;

namespace TaxiManagement
{

    public class TaxiManager
    {
        private readonly SortedDictionary<int, Taxi> _taxis;

        public TaxiManager()
        {
            _taxis = new SortedDictionary<int, Taxi>();
        }

        public SortedDictionary<int, Taxi> GetAllTaxis()
        {
            return _taxis;
        }

        public Taxi FindTaxi(int taxiNumber)
        {
            return _taxis.ContainsKey(taxiNumber) ? _taxis[taxiNumber] : null;
        }

        public Taxi CreateTaxi(int taxiNumber)
        {
            if (_taxis.ContainsKey(taxiNumber))
            {
                return _taxis[taxiNumber]; 
            }

            var newTaxi = new Taxi(taxiNumber);
            _taxis.Add(taxiNumber, newTaxi);
            return newTaxi;
        }
    }

}