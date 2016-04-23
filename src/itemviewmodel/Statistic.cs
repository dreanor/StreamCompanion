using StreamCompanion.Contract;
using System;
using System.Collections.Generic;

namespace StreamCompanion.ItemViewModel
{
    public class Statistic : IStatistic
    {
        public Statistic(int episodeCount, double meanScore, Dictionary<int, int> scoreDistribution, int serieCount)
        {
            EpisodeCount = episodeCount;
            MeanScore = meanScore;
            ScoreDistribution = scoreDistribution;
            SerieCount = serieCount;
        }

        public int EpisodeCount { get; private set; }

        public double MeanScore { get; private set; }

        public Dictionary<int, int> ScoreDistribution { get; private set; }

        public int SerieCount { get; private set; }
    }
}
