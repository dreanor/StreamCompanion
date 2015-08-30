using NUnit.Framework;
using StreamCompanion.Contract;
using StreamCompanion.ItemViewModel;
using System.Collections.ObjectModel;

namespace StreamCompanion.Tests.ItemViewModel
{
    [TestFixture]
    public class SerieUnitTest
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void CanReturnTheCorrectNumber(int number, int expectedNumber) 
        {
            ISerie serie = new Serie(number, string.Empty, null, 0, string.Empty, string.Empty, string.Empty);

            Assert.AreEqual(expectedNumber, serie.Number);
        }

        [TestCase("a", "a")]
        [TestCase("b", "b")]
        [TestCase("c", "c")]
        public void CanReturnTheCorrectTitle(string title, string expectedTitle)
        {
            ISerie serie = new Serie(0, title, null, 0, string.Empty, string.Empty, string.Empty);

            Assert.AreEqual(expectedTitle, serie.Title);
        }

        [TestCase(1, 1, 1)]
        [TestCase(0, 0, null)]
        public void CanReturnTheCorrectProgress(int currentEpisode, int lastEpisode, int? season)
        {
            Progress expectedProgress = season.HasValue ? new Progress(currentEpisode, lastEpisode, season) : null;
            ISerie serie = new Serie(0, string.Empty, expectedProgress, 0, string.Empty, string.Empty, string.Empty);

            Assert.AreEqual(expectedProgress, serie.Progress);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void CanReturnTheCorrectRating(int rating, int expectedRating)
        {
            ISerie serie = new Serie(0, string.Empty, null, rating, string.Empty, string.Empty, string.Empty);

            Assert.AreEqual(expectedRating, serie.Rating);
        }

        [TestCase("a", "a")]
        [TestCase("b", "b")]
        [TestCase("c", "c")]
        public void CanReturnTheCorrectType(string type, string expectedType)
        {
            ISerie serie = new Serie(0, string.Empty, null, 0, type, string.Empty, string.Empty);

            Assert.AreEqual(expectedType, serie.Type);
        }

        [TestCase("a", "a")]
        [TestCase("b", "b")]
        [TestCase("c", "c")]
        public void CanReturnTheCorrectComment(string comment, string expectedComment)
        {
            ISerie serie = new Serie(0, string.Empty, null, 0, string.Empty, comment, string.Empty);

            Assert.AreEqual(expectedComment, serie.Comment);
        }

        [TestCase("a", "a")]
        [TestCase("b", "b")]
        [TestCase("c", "c")]
        public void CanReturnTheCorrectStream(string stream, string expectedStream)
        {
            ISerie serie = new Serie(0, string.Empty, null, 0, string.Empty, string.Empty, stream);

            Assert.AreEqual(expectedStream, serie.Stream);
        }

        [Test]
        public void CanReturnTheCorrectTypes() 
        {
            ObservableCollection<string> expectedTypes = new ObservableCollection<string> 
            {
                "TV",
                "Anime",
                "Cartoon",
                "Movie",
                "OVA",
                "Special",
                "Mixed"
            };

            ISerie serie = new Serie(0, string.Empty, null, 0, string.Empty, string.Empty, string.Empty);

            Assert.AreEqual(expectedTypes, serie.Types);
        }
    }
}
