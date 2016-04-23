using System.Collections.Generic;

namespace StreamCompanion.Contract
{
    public interface IStatistic
    {
        int SerieCount { get; }

        int EpisodeCount { get; }

        double MeanScore { get; }

        Dictionary<int, int> ScoreDistribution { get; }
    }
}
