using System.Collections.ObjectModel;
using System.Linq;
using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;
using helper.mvvm.baseclasses;
using IViewModel = com.gmail.mikeundead.streamcompanion.contract.streamtemplate.IViewModel;

namespace com.gmail.mikeundead.streamcompanion.streamtemplate
{
    using System;

    public class ViewModel : ViewModelBase<IViewModel>, IViewModel
    {
        private readonly IController controller;

        private bool editMode;

        public ViewModel(IController controller)
        {
            this.controller = controller;
            this.StreamLanguages = new ObservableCollection<string>(Enum.GetNames(typeof(Language)));

            this.LoadData();

            this.SaveAndExitCmd = new ActionCommand(this.SaveAndExit);
            this.AddStreamWebsiteCmd = new ActionCommand(this.AddNewStreamWebsite);
            this.EditStreamWebsiteCmd = new ActionCommand(this.EditSelectedStreamWebsite, () => this.SelectedItem != null);

            this.MoveUpCmd = new ActionCommand(this.MoveUp);
            this.MoveDownCmd = new ActionCommand(this.MoveDown);

            this.DoneCmd = new ActionCommand(this.Done, this.NewStreamingWebsiteIsValid);
            this.CancelCmd = new ActionCommand(() => this.IsStreamingWebsiteVisible = false);
        }

        public ObservableCollection<IStreamItem> Streams
        {
            get { return this.Get(x => x.Streams); }
            private set { this.Set(x => x.Streams, value); }
        }

        public IStreamItem SelectedItem
        {
            get { return this.Get(x => x.SelectedItem); }
            set { this.Set(x => x.SelectedItem, value); }
        }

        public ActionCommand SaveAndExitCmd { get; private set; }

        public ActionCommand AddStreamWebsiteCmd { get; private set; }

        public ActionCommand EditStreamWebsiteCmd { get; private set; }

        public ActionCommand MoveUpCmd { get; private set; }

        public ActionCommand MoveDownCmd { get; private set; }

        public ActionCommand DoneCmd { get; private set; }

        public ActionCommand CancelCmd { get; private set; }

        public bool IsDone
        {
            get { return this.Get(x => x.IsDone); }
            private set { this.Set(x => x.IsDone, value); }
        }

        public bool IsStreamingWebsiteVisible
        {
            get { return this.Get(x => x.IsStreamingWebsiteVisible); }
            set { this.Set(x => x.IsStreamingWebsiteVisible, value); }
        }

        public string NewStreamingWebsite
        {
            get { return this.Get(x => x.NewStreamingWebsite); }
            set { this.Set(x => x.NewStreamingWebsite, value); }
        }

        public string WhitespaceReplacement
        {
            get { return this.Get(x => x.WhitespaceReplacement); }
            set { this.Set(x => x.WhitespaceReplacement, value); }
        }

        public string GenericUrl
        {
            get { return this.Get(x => x.GenericUrl); }
            set { this.Set(x => x.GenericUrl, value); }
        }

        public string UsedOnTypes
        {
            get { return this.Get(x => x.UsedOnTypes); }
            set { this.Set(x => x.UsedOnTypes, value); }
        }

        public bool IsGeneral
        {
            get { return this.Get(x => x.IsGeneral); }
            set
            {
                if (value)
                {
                    this.IsTv = true;
                    this.IsAnime = true;
                    this.IsCartoon = true;
                    this.IsMovie = true;
                    this.UsedOnTypes = SerieType.Mixed.ToString();
                }
                else
                {
                    this.IsTv = false;
                    this.IsAnime = false;
                    this.IsCartoon = false;
                    this.IsMovie = false;
                    this.UsedOnTypes = string.Empty;
                }
                this.Set(x => x.IsGeneral, value);
            }
        }

        public bool IsTv
        {
            get { return this.Get(x => x.IsTv); }
            set
            {
                if (value)
                {
                    this.UsedOnTypes += SerieType.TV + ",";
                } 
                this.Set(x => x.IsTv, value);
            }
        }

        public bool IsAnime
        {
            get { return this.Get(x => x.IsAnime); }
            set
            {
                if (value)
                {
                    this.UsedOnTypes += SerieType.Anime + ",";
                } 
                this.Set(x => x.IsAnime, value);
            }
        }

        public bool IsCartoon
        {
            get { return this.Get(x => x.IsCartoon); }
            set
            {
                if (value)
                {
                    this.UsedOnTypes += SerieType.Cartoon + ",";
                }
                this.Set(x => x.IsCartoon, value);
            }
        }

        public bool IsMovie
        {
            get { return this.Get(x => x.IsMovie); }
            set
            {
                if (value)
                {
                    this.UsedOnTypes += SerieType.Movie + ",";
                }
                this.Set(x => x.IsMovie, value);
            }
        }

        public string SelectedStreamLanguage
        {
            get { return this.Get(x => x.SelectedStreamLanguage); }
            set { this.Set(x => x.SelectedStreamLanguage, value); }
        }

        public ObservableCollection<string> StreamLanguages
        {
            get { return this.Get(x => x.StreamLanguages); }
            private set { this.Set(x => x.StreamLanguages, value); }
        }

        private void LoadData()
        {
            this.Streams = new ObservableCollection<IStreamItem>(this.controller.LoadStreamTemplates());
        }

        private void SaveAndExit()
        {
            this.controller.SaveStreamTemplates(new Model(this.Streams.Cast<StreamItem>().ToList()));
            this.IsDone = true;
        }

        private void MoveUp()
        {
            var currentIndex = this.Streams.IndexOf(this.SelectedItem);

            if (currentIndex != 0)
            {
                this.Streams.Move(currentIndex, currentIndex - 1);
            }
        }

        private void MoveDown()
        {
            var currentIndex = this.Streams.IndexOf(this.SelectedItem);

            if (currentIndex != this.Streams.Count - 1)
            {
                this.Streams.Move(currentIndex, currentIndex + 1);
            }
        }

        private bool NewStreamingWebsiteIsValid()
        {
            return !string.IsNullOrWhiteSpace(this.NewStreamingWebsite) 
                   && !string.IsNullOrWhiteSpace(this.WhitespaceReplacement)
                   && !string.IsNullOrWhiteSpace(this.GenericUrl);
        }

        private void AddNewStreamWebsite()
        {
            this.NewStreamingWebsite = string.Empty;
            this.GenericUrl = string.Empty;
            this.WhitespaceReplacement = string.Empty;
            this.IsGeneral = false;
            this.IsStreamingWebsiteVisible = true;
            this.SelectedStreamLanguage = Language.English.ToString();
        }
       
        private void EditSelectedStreamWebsite()
        {
            var site = this.SelectedItem.Website;

            this.NewStreamingWebsite = site.Substring(0, site.LastIndexOf('/'));
            this.GenericUrl = site.Substring(site.LastIndexOf('/') + 1);
            this.WhitespaceReplacement = this.SelectedItem.WhitespaceReplacement;
            this.SelectedStreamLanguage = this.SelectedItem.StreamLanguage;
            var usedTypes = this.SelectedItem.UsedOnTypes;

            if (usedTypes == SerieType.Mixed.ToString())
            {
                this.IsGeneral = true;
            }
            else
            {
                if (usedTypes.Contains(SerieType.TV.ToString()))
                {
                    this.IsTv = true;
                }
                if (usedTypes.Contains(SerieType.Anime.ToString()))
                {
                    this.IsAnime = true;
                }
                if (usedTypes.Contains(SerieType.Cartoon.ToString()))
                {
                    this.IsCartoon = true;
                }
                if (usedTypes.Contains(SerieType.Movie.ToString()))
                {
                    this.IsMovie = true;
                }
            }

            this.editMode = true;
            this.IsStreamingWebsiteVisible = true;
        }

        private void Done()
        {
            if (!this.NewStreamingWebsite.EndsWith("/"))
            {
                this.NewStreamingWebsite += "/";
            }
            var website = string.Format("{0}{1}", this.NewStreamingWebsite, this.GenericUrl);

            var type = this.UsedOnTypes;
            if (this.UsedOnTypes.EndsWith(","))
            {
                type = this.UsedOnTypes.Substring(0, this.UsedOnTypes.Length - 1);
            }

            if (this.editMode)
            {
                this.SelectedItem.StreamLanguage = this.SelectedStreamLanguage;
                this.SelectedItem.UsedOnTypes = type;
                this.SelectedItem.Website = website;
                this.SelectedItem.WhitespaceReplacement = this.WhitespaceReplacement;
            }
            else
            {
                this.Streams.Add(new StreamItem(website, this.WhitespaceReplacement, type, this.SelectedStreamLanguage));
            }

            this.IsStreamingWebsiteVisible = false;
        }
    }
}