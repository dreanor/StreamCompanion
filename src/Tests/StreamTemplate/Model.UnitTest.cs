using NUnit.Framework;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.StreamTemplate;
using System.Collections.Generic;

namespace StreamCompanion.Tests.StreamTemplate
{
    [TestFixture]
    public class ModelUnitTest
    {
        [Test]
        public void CanReturnTheCorrectStreams() 
        {
            List<StreamItem> streams = new List<StreamItem>();
            streams.Add(new StreamItem("", ""));

            IModel modelSUT = new StreamCompanion.StreamTemplate.Model(streams);

            Assert.AreEqual(streams, modelSUT.Streams);
        }
    }
}
