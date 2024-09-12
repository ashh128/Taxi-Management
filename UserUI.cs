using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace TaxiManagement
{

    public class UserUI
    {
        private RankManager _rankManager;
        private TaxiManager _taxiManager;
        private TransactionManager _transactionManager;


        public UserUI(RankManager rankManager, TaxiManager taxiManager, TransactionManager transactionManager)
        {
            _rankManager = rankManager;
            _taxiManager = taxiManager;
            _transactionManager = transactionManager;
        }

        public List<string> TaxiDropsFare(int taxiNumber, bool priceWasPaid)
        {
            List<string> messages = new List<string>();

            Taxi taxi = _taxiManager.FindTaxi(taxiNumber);
            if (taxi == null)
            {
                messages.Add($"Taxi {taxiNumber} does not exist.");
                return messages;
            }

            if (string.IsNullOrEmpty(taxi.Destination))
            {
                messages.Add($"Taxi {taxiNumber} has not dropped its fare.");
                return messages;
            }

            taxi.DropFare(priceWasPaid);
            _transactionManager.RecordDrop(taxi.Number, priceWasPaid);

            if (priceWasPaid)
            {
                messages.Add($"Taxi {taxiNumber} has dropped its fare and the price was paid.");
            }
            else
            {
                messages.Add($"Taxi {taxiNumber} has dropped its fare and the price was not paid.");
            }

            return messages;
        }


        public List<string> TaxiJoinsRank(int taxiNumber, int rankId)
        {
            Taxi taxi = _taxiManager.FindTaxi(taxiNumber);
            if (taxi == null)
            {
                taxi = _taxiManager.CreateTaxi(taxiNumber);
            }

            bool joined = _rankManager.AddTaxiToRank(taxi, rankId);

            List<string> messages = new List<string>();
            if (joined)
            {
                messages.Add($"Taxi {taxiNumber} has joined rank {rankId}.");
                _transactionManager.RecordJoin(taxi.Number, rankId);
            }
            else
            {
                messages.Add($"Taxi {taxiNumber} has not joined rank {rankId}.");
            }

            return messages;
        }

        public List<string> TaxiLeavesRank(int rankId, string destination, double agreedPrice)
        {
            List<string> messages = new List<string>();

            Rank rank = _rankManager.FindRank(rankId);
            if (rank == null)
            {
                messages.Add($"Rank {rankId} does not exist.");
                return messages;
            }

            Taxi taxi = rank.FrontTaxiTakesFare(destination, agreedPrice);
            if (taxi == null)
            {
                messages.Add($"Taxi has not left rank {rankId}.");
                return messages;
            }

            //_transactionManager.RecordLeave(DateTime.Now, rankId, taxi);

            messages.Add(
                $"Taxi {taxi.Number} has left rank {rankId} to take a fare to {destination} for £{agreedPrice:F2}.");
            return messages;
        }

        public List<string> ViewFinancialReport()
        {
            List<string> messages = new List<string>
            {
                "Financial report",
                "================"
            };

            SortedDictionary<int, Taxi> taxiDictionary = _taxiManager.GetAllTaxis();
            List<Taxi> taxis = taxiDictionary.Values.ToList();
            if (taxis.Count == 0)
            {
                messages.Add("No taxis, so no money taken");
                return messages;
            }

            double totalMoneyTaken = 0;
            foreach (Taxi taxi in taxis)
            {
                messages.Add($"Taxi {taxi.Number}      {taxi.TotalMoneyPaid.ToString("F2")}");
                totalMoneyTaken += taxi.TotalMoneyPaid;
            }

            messages.Add("           ======");
            messages.Add($"Total:       {totalMoneyTaken.ToString("F2")}");
            messages.Add("           ======");

            return messages;
        }


        public List<string> ViewTaxiLocations()
        {
            List<string> messages = new List<string>
            {
                "Taxi locations",
                "=============="
            };

            SortedDictionary<int, Taxi> taxiDictionary = _taxiManager.GetAllTaxis();
            List<Taxi> taxis = taxiDictionary.Values.ToList();
            if (taxis.Count == 0)
            {
                messages.Add("No taxis");
                return messages;
            }

            foreach (Taxi taxi in taxis)
            {
                if (!string.IsNullOrEmpty(taxi.Destination))
                {
                    messages.Add($"Taxi {taxi.Number} is on the road to {taxi.Destination}");
                }
                else if (taxi.Rank != null)
                {
                    messages.Add($"Taxi {taxi.Number} is in rank {taxi.Rank.Id}");
                }
                else
                {
                    messages.Add($"Taxi {taxi.Number} is on the road");
                }
            }

            return messages;
        }



        public List<string> ViewTransactionLog()
        {
            List<string> transactionLog = new List<string>
            {
                "Transaction report",
                "=================="
            };

            if (_transactionManager.Transactions.Count == 0)
            {
                transactionLog.Add("No transactions");
            }
            else
            {
                var sortedTransactions = _transactionManager.Transactions
                    .OrderByDescending(transaction => transaction.TransactionDatetime)
                    .ToList();

                foreach (var transaction in sortedTransactions)
                {
                    transactionLog.Add(transaction.ToString());
                }
            }

            return transactionLog;
        }

    }

}



