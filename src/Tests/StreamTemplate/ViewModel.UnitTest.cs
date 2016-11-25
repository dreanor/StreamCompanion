using NUnit.Framework;
using Rhino.Mocks;
using StreamCompanion.Contract;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.StreamTemplate;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StreamCompanion.Tests.StreamTemplate
{
    [TestFixture]
    public class ViewModelUnitTest
    {
        private IController controllerMock;
        private StreamCompanion.Contract.StreamTemplate.IViewModel viewModelSUT;
        [SetUp]
        public void SetUp()
        {
            controllerMock = MockRepository.GenerateMock<IController>();
            controllerMock.Stub(x => x.LoadStreamTemplates()).Return(new List<IStreamItem>());
            viewModelSUT = new ViewModel(controllerMock);
        }

        [Test]
        public void CanReturnTheCorrectStreams() 
        {
            Assert.AreEqual(new ObservableCollection<IStreamItem>(), viewModelSUT.Streams);
        }

        [Test]
        public void CanReturnTheCorrectSelectedItem()
        {
            IStreamItem expectedStreamItem = new StreamItem("a", "b");
            viewModelSUT.SelectedItem = expectedStreamItem;

            Assert.AreEqual(expectedStreamItem, viewModelSUT.SelectedItem);
        }

        [Test]
        public void CanExecuteSaveAndExitCmd()
        {
            controllerMock.Expect(x => x.SaveStreamTemplates(null)).IgnoreArguments();
            viewModelSUT.SaveAndExitCmd.Execute(null);

            controllerMock.VerifyAllExpectations();
            Assert.AreEqual(true, viewModelSUT.IsDone);
        }

        [Test]
        public void CanExecuteAddStreamWebsiteCmd()
        {
            viewModelSUT.AddStreamWebsiteCmd.Execute(null);

            Assert.AreEqual(string.Empty, viewModelSUT.NewStreamingWebsite);
            Assert.AreEqual(string.Empty, viewModelSUT.GenericUrl);
            Assert.AreEqual(string.Empty, viewModelSUT.WhitespaceReplacement);
            Assert.AreEqual(false, viewModelSUT.IsGeneral);
            Assert.AreEqual(true, viewModelSUT.IsStreamingWebsiteVisible);
            Assert.AreEqual("English", viewModelSUT.SelectedStreamLanguage);
        }

        [TestCase("Mixed", true, true, true, true, true)]
        [TestCase("TV", false, true, false, false, false)]
        [TestCase("Anime", false, false, true, false, false)]
        [TestCase("Cartoon", false, false, false, true, false)]
        [TestCase("Movie", false, false, false, false, true)]
        public void CanExecuteEditStreamWebsiteCmd(string usedOnType, bool isGeneral, bool isTv, bool isAnime, bool isCartoon, bool isMovie)
        {
            viewModelSUT.SelectedItem = new StreamItem("website/123", "replacement", usedOnType, "English");
            viewModelSUT.EditStreamWebsiteCmd.Execute(null);

            Assert.AreEqual("website", viewModelSUT.NewStreamingWebsite);
            Assert.AreEqual("123", viewModelSUT.GenericUrl);
            Assert.AreEqual("replacement", viewModelSUT.WhitespaceReplacement);
            Assert.AreEqual("English", viewModelSUT.SelectedStreamLanguage);
            Assert.AreEqual(true, viewModelSUT.IsStreamingWebsiteVisible);

            Assert.AreEqual(isGeneral, viewModelSUT.IsGeneral);
            Assert.AreEqual(isTv, viewModelSUT.IsTv);
            Assert.AreEqual(isAnime, viewModelSUT.IsAnime);
            Assert.AreEqual(isCartoon, viewModelSUT.IsCartoon);
            Assert.AreEqual(isMovie, viewModelSUT.IsMovie);
        }

        [Test]
        public void CanExecuteMoveUpCmd()
        {
            IStreamItem expectedStreamItem = new StreamItem("c", "d");
            viewModelSUT.Streams.Add(new StreamItem("a", "b"));
            viewModelSUT.Streams.Add(expectedStreamItem);
            viewModelSUT.SelectedItem = viewModelSUT.Streams[1];

            viewModelSUT.MoveUpCmd.Execute(null);

            Assert.AreEqual(expectedStreamItem, viewModelSUT.Streams[0]);
        }

        [Test]
        public void CanExecuteMoveDownCmd()
        {
            IStreamItem expectedStreamItem = new StreamItem("a", "b");
            viewModelSUT.Streams.Add(expectedStreamItem);
            viewModelSUT.Streams.Add(new StreamItem("c", "d"));
            viewModelSUT.SelectedItem = viewModelSUT.Streams[0];

            viewModelSUT.MoveDownCmd.Execute(null);

            Assert.AreEqual(expectedStreamItem, viewModelSUT.Streams[1]);
        }

        [TestCase("http://kinox.to/Stream/", "Movie", true, 0)]
        [TestCase("http://kinox.to/Stream", "Movie,", false, 1)]
        public void CanExecuteDoneCmd(string newStreamingWebsite, string usedOnTypes, bool editMode, int expectedCount)
        {
            viewModelSUT.NewStreamingWebsite = newStreamingWebsite;
            viewModelSUT.WhitespaceReplacement = "_";
            viewModelSUT.GenericUrl = "{0}-1.html,s{1}e{2}";
            viewModelSUT.UsedOnTypes = usedOnTypes;
            if (editMode)
            {
                viewModelSUT.SelectedItem = new StreamItem("website/123", "replacement", "Movie", "English");
                viewModelSUT.EditStreamWebsiteCmd.Execute(null);
            }

            viewModelSUT.DoneCmd.Execute(null);

            Assert.AreEqual(expectedCount, viewModelSUT.Streams.Count);
        }

        [Test]
        public void CanExecuteCancelCmd()
        {
            viewModelSUT.IsStreamingWebsiteVisible = true;

            Assert.IsTrue(viewModelSUT.IsStreamingWebsiteVisible);

            viewModelSUT.CancelCmd.Execute(null);

            Assert.IsFalse(viewModelSUT.IsStreamingWebsiteVisible);
        }
    }
}
