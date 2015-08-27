using NUnit.Framework;
using Rhino.Mocks;
using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Model;
using StreamCompanion.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCompanion.Tests.Controller
{
    [TestFixture]
    public class MainControllerUnitTest
    {
        [Test]
        public void CanGenerateNextEpisodeStream() 
        {
            IStepModel stepModel = MockRepository.GenerateMock<IStepModel>();
            IController mainController = new MainController(stepModel);
        }
    }
}
