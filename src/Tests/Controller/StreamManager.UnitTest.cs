using NUnit.Framework;
using StreamCompanion.Contract;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.Controller;
using StreamCompanion.ItemViewModel;
using StreamCompanion.StreamTemplate;
using System.Collections.Generic;

namespace Tests.Controller
{
    [TestFixture]
    public class StreamManagerUnitTest
    {
        private StreamManager streamManagerSUT;

        [SetUp]
        public void SetUp()
        {
             streamManagerSUT = new StreamManager();
        }

        [TestCase(SerieType.Movie, "https://www.google.com/#q=TestMovie+stream&safe=off", 0)]
        [TestCase(SerieType.Mixed, "https://www.google.com/#q=TestMovie+episode+1+stream&safe=off", null)]
        [TestCase(SerieType.Mixed, "https://www.google.com/#q=TestMovie+season+1+episode+1+stream&safe=off", 1)]
        public void CanCorrectlyGenerateNextEpisodesStreamWithoutCachedStreams(SerieType serieType, string expected, int? season) 
        {
            ISerie serie = new Serie(0, "TestMovie", new Progress(0, 1, season), 0, serieType.ToString(), string.Empty, string.Empty);

            string actual = streamManagerSUT.GenerateNextEpisodeStream(serie, new List<IStreamItem>());

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "Scrubs", 1, "http://kinox.to/Stream/{0}-1.html,s{1}e{2}", "http://kinox.to/Stream/Scrubs-1.html,s1e1")]
        [TestCase(1, "Scrubs", 1, "http://kinox.to/Stream/{0}-1.html,s{1}e{2}", "http://kinox.to/Stream/Scrubs-1.html,s1e1")]
        [TestCase(1, "Scrubs", 1, "http://kinox.to/Stream/{0}-1.html,s{1}", "http://kinox.to/Stream/Scrubs-1.html,s1")]
        [TestCase(1, "Scrubs", 1, "http://kinox.to/Stream/{0}-1.html", "http://kinox.to/Stream/Scrubs-1.html")]
        [TestCase(1, "Scrubs", 1, "http://kinox.to/Stream/-1.html", "https://www.google.com/#q=Scrubs+season+1+episode+1+stream&safe=off")]
        [TestCase(1, "Scrubs", 1, "http://kinox.to/Stream/{0}-1.html,s1e{2}", "http://kinox.to/Stream/Scrubs-1.html,s1e1")]
        public void CanCorrectlyGenerateNextEpisodesForStreamWithCachedStreams(int? season, string title, int currentEpisode, string pattern, string expected)
        {
            List<IStreamItem> streams = new List<IStreamItem>();
            streams.Add(new StreamItem(pattern, "_"));

            ISerie serie = new Serie(0, title, new Progress(currentEpisode, 1, season), 0, SerieType.Mixed.ToString(), string.Empty, string.Empty);

            string actual = streamManagerSUT.GenerateNextEpisodeStream(serie, streams);

            Assert.AreEqual(expected, actual);
        }
    }
}
