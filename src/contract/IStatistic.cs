using LiveCharts;

namespace StreamCompanion.Contract
{
    public interface IStatistic
    {
        int SerieCount { get; }

        int EpisodeCount { get; }

        double MeanScore { get; }

        SeriesCollection ScoreDistribution { get; }

        string[] ScoreRatings { get; }
    }
}
