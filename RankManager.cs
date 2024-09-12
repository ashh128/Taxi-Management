
using System.Collections.Generic;
namespace TaxiManagement{
public class RankManager
{
    private readonly Dictionary<int, Rank> _ranks;

    public RankManager()
    {
        _ranks = new Dictionary<int, Rank>()
        {
            { 1, new Rank(1, 5) },
            { 2, new Rank(2, 2) }, 
            { 3, new Rank(3, 4) }, 
        };
    }

    public Rank FindRank(int rankId)
    {
        return _ranks.ContainsKey(rankId) ? _ranks[rankId] : null;
    }

    public bool AddTaxiToRank(Taxi taxi, int rankId)
    {
        if (taxi.Rank != null || !string.IsNullOrEmpty(taxi.Destination))
            {
            return false;
        }

        if (!_ranks.ContainsKey(rankId))
        {
            return false;
        }

        var rank = _ranks[rankId];
        return rank.AddTaxi(taxi); 
    }

    public Taxi FrontTaxiInRankTakesFare(int rankId, string destination, double fare)
    {
        if (!_ranks.ContainsKey(rankId))
        {
            return null;
        }

        var rank = _ranks[rankId];
        return rank.FrontTaxiTakesFare(destination, fare); 
    }
}

}

