using NUnit.Framework;
using Rhino.Mocks;
using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Shell;
using StreamCompanion.Contract.ShellBase.Uic.Step;
using StreamCompanion.ShellViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
