using NUnit.Framework;
using StreamCompanion.Contract;
using StreamCompanion.ItemViewModel;

namespace StreamCompanion.Tests.ItemViewModel
{
    [TestFixture]
    public class ProgessUnitTest
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void CanReturnTheCurrentEpisode(int currentEpisode, int expectedEpisode) 
        {
            IProgress progress = new Progress(currentEpisode, 2, 1);

            Assert.AreEqual(expectedEpisode, progress.CurrentEpisode);
        }

        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        public void CanReturnTheLastEpisode(int lastEpisode, int expectedEpisode) 
        {
            IProgress progress = new Progress(1, lastEpisode, 1);

            Assert.AreEqual(expectedEpisode, progress.LastEpisode);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(null, null)]
        public void CanReturnTheSeason(int? season, int? expectedSeason)
        {
            IProgress progress = new Progress(1, 2, season);

            Assert.AreEqual(expectedSeason, progress.Season);
        }

        [TestCase(1, 1, "1/1")]
        [TestCase(0, 2, "0/2")]
        [TestCase(2, 3, "2/3")]
        [TestCase(4, 0, "4/0")]
        public void CanReturnTheCorrectEpisodeDisplay(int currentEpisode, int lastEpisode, string expectedEpisodeDisplay)
        {
            IProgress progress = new Progress(currentEpisode, lastEpisode, 1);

            Assert.AreEqual(expectedEpisodeDisplay, progress.EpisodeDisplay);
        }
    }
}
