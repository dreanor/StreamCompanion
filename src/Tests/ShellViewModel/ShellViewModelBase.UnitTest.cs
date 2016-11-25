using NUnit.Framework;
using Rhino.Mocks;
using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Uic.Step;
using StreamCompanion.ShellViewModel;

namespace StreamCompanion.Tests.ShellViewModel
{
    [TestFixture]
    public class ShellViewModelBaseUnitTest
    {
        private IController controller;
        private IStepUIC stepUICMock;
        private MainShellViewModel shellViewModelSUT;

        [SetUp]
        public void SetUp() 
        {
            controller = MockRepository.GenerateMock<IController>();
            stepUICMock = MockRepository.GenerateMock<IStepUIC>();
            shellViewModelSUT = new MainShellViewModel(controller, stepUICMock);
        }

        [Test]
        public void CanReturnTheCorrectSteps() 
        {
            Assert.AreEqual(stepUICMock, shellViewModelSUT.Steps[0]);
        }

        [Test]
        public void CanReturnTheCorrectSelectedItem()
        {
            Assert.AreEqual(stepUICMock, shellViewModelSUT.SelectedItem);
        }
    }
}
