using NUnit.Framework;
using Rhino.Mocks;
using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Model;
using StreamCompanion.Controller;
using StreamCompanion.ItemViewModel;

namespace StreamCompanion.Tests.Controller
{
    [TestFixture]
    public class MainControllerUnitTest
    {
        IController mainController;
        [SetUp]
        public void SetUp() 
        {
            IStepModel stepModelMock = MockRepository.GenerateMock<IStepModel>();
            mainController = new MainController(stepModelMock);
        }

        [Test]
        public void CanGenerateNextEpisodeStream() 
        {
            const string expectedGeneratedStream = "https://www.google.com/#q=Test+stream&safe=off";
            ISerie serieMock = MockRepository.GenerateMock<ISerie>();
            serieMock.Stub(x => x.Title).Return("Test");
            serieMock.Stub(x => x.Progress).Return(new Progress(0, 1, 1));
            serieMock.Stub(x => x.Type).Return("Movie");

            string actualGeneratedStream = mainController.GenerateNextEpisodeStream(serieMock);

            Assert.AreEqual(expectedGeneratedStream, actualGeneratedStream);
        }
    }
}
