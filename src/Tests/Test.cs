using NUnit.Framework;
using StreamCompanion.Contract;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.Controller;
using StreamCompanion.StreamTemplate;

namespace Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestMeh() 
        {
            IStreamItem m = new StreamItem("a", "b");
        }
    }
}
