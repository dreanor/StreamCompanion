using LiveCharts;
using StreamCompanion.Contract;

namespace StreamCompanion.ItemViewModel
{
    public class Statistic : IStatistic
    {
        public Statistic(int episodeCount, double meanScore, SeriesCollection scoreDistribution, string[] scoreRatings, int serieCount)
        {
            EpisodeCount = episodeCount;
            MeanScore = meanScore;
            ScoreDistribution = scoreDistribution;
            SerieCount = serieCount;
            ScoreRatings = scoreRatings;
        }

        public int EpisodeCount { get; private set; }

        public double MeanScore { get; private set; }

        public SeriesCollection ScoreDistribution { get; private set; }

        public string[] ScoreRatings { get; private set; }

        public int SerieCount { get; private set; }
    }
}
