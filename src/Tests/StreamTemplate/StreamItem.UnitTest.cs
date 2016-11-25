using NUnit.Framework;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.StreamTemplate;

namespace StreamCompanion.Tests.StreamTemplate
{
    [TestFixture]
    public class StreamItemUnitTest
    {
        [Test]
        public void CanReturnTheCorrectWebsite() 
        {
            const string website = "website";
            IStreamItem streamItemSUT = new StreamItem(website, "replacement");
            Assert.AreEqual(website, streamItemSUT.Website);
        }

        [Test]
        public void CanReturnTheCorrectWhitespaceReplacement()
        {
            const string replacement = "replacement";

            IStreamItem streamItemSUT = new StreamItem("website", replacement);
            Assert.AreEqual(replacement, streamItemSUT.WhitespaceReplacement);
        }

        [Test]
        public void CanReturnTheCorrectUsedOnTypes()
        {
            IStreamItem streamItemSUT = new StreamItem("website", "replacement");
            Assert.AreEqual("Mixed", streamItemSUT.UsedOnTypes);
        }

        [Test]
        public void CanReturnTheCorrectStreamLanguage()
        {
            IStreamItem streamItemSUT = new StreamItem("website", "replacement");
            Assert.AreEqual("English", streamItemSUT.StreamLanguage);
        }
    }
}
