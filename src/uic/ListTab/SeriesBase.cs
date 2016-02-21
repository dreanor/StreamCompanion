using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Model;
using StreamCompanion.ItemViewModel;
using helper.mvvm.baseclasses;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using helper.mvvm.commands;
using StreamCompanion.ShellViewModel;

namespace StreamCompanion.Uic.TabControlUic
{
    public class SeriesBase : ViewModelBase<IViewModel, IModel>, IViewModel
    {
        private readonly IController controller;
        private readonly IStepModel stepModel;
        private readonly Dispatcher dispatcher;
        private bool isBusyChangedBlock;
        private bool isSerieInEditMode;

        public SeriesBase(IStepModel stepModel, IModel model, IController controller)
            : base(model)
        {
            this.Series = new ObservableCollection<ISerie>();
            this.Map(x => x.Series, m => m.Series);
            this.Map(x => x.IsBusy, m => m.IsBusy);
            this.stepModel = stepModel;
            this.controller = controller;

            this.AddSerieCmd = new ActionCommand(this.OpenAddNewSerie);
            this.EditSerieCmd = new ActionCommand(this.EditCurrentSerie, () => this.SelectedItem != null);
            this.DoneCmd = new ActionCommand(this.AddNewSerie, this.ValidateNewSerie);
            this.CancelCmd = new ActionCommand(() => this.IsAddNewSerieVisible = false);

            this.ForceReloadStreamCmd = new ActionCommand(this.ForceReloadStream, () => this.SelectedItem != null);
            this.ReloadAllCmd = new ActionCommand(this.ReloadAll);
            this.SetAsCompletedCmd = new ActionCommand(this.SetAsCompleted, () => this.SelectedItem != null && this.model != this.stepModel.Completed);
            this.SetAsCurrentlyWatchingCmd = new ActionCommand(this.SetAsCurrentlyWatching, () => this.SelectedItem != null && this.model != this.stepModel.CurrentlyWatching);
            this.SetAsDroppedCmd = new ActionCommand(this.SetAsDropped, () => this.SelectedItem != null && this.model != this.stepModel.Dropped);
            this.SetAsOnHoldCmd = new ActionCommand(this.SetAsOnHold, () => this.SelectedItem != null && this.model != this.stepModel.OnHold);
            this.SetAsPlanToWatchCmd = new ActionCommand(this.SetAsPlanToWatch, () => this.SelectedItem != null && this.model != this.stepModel.PlanToWatch);

            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.PropertyChanged += this.HandleIsBusyChanged;

            this.Types = new ObservableCollection<string>(Enum.GetNames(typeof(SerieType)));

            this.SetupExistingHooks();
        }

        public ActionCommand AddSerieCmd { get; private set; }

        public ActionCommand EditSerieCmd { get; private set; }

        public ActionCommand ForceReloadStreamCmd { get; private set; }

        public ActionCommand ReloadAllCmd { get; private set; }

        public ActionCommand SetAsCurrentlyWatchingCmd { get; private set; }

        public ActionCommand SetAsCompletedCmd { get; private set; }

        public ActionCommand SetAsOnHoldCmd { get; private set; }

        public ActionCommand SetAsDroppedCmd { get; private set; }

        public ActionCommand SetAsPlanToWatchCmd { get; private set; }

        public ActionCommand IncreaseEpisodeCmd { get; private set; }

        public ActionCommand DoneCmd { get; private set; }

        public ActionCommand CancelCmd { get; private set; }

        public ISerie SelectedItem
        {
            get { return this.Get(x => x.SelectedItem); }
            set { this.Set(x => x.SelectedItem, value); }
        }

        public ObservableCollection<ISerie> Series
        {
            get { return this.Get(x => x.Series); }
            private set { this.Set(x => x.Series, value); }
        }

        public ObservableCollection<string> Types
        {
            get { return this.Get(x => x.Types); }
            private set { this.Set(x => x.Types, value); }
        }

        public bool IsBusy
        {
            get { return this.Get(x => x.IsBusy); }
            private set { this.Set(x => x.IsBusy, value); }
        }

        public bool IsAddNewSerieVisible
        {
            get { return this.Get(x => x.IsAddNewSerieVisible); }
            set { this.Set(x => x.IsAddNewSerieVisible, value); }
        }

        public string NewSerieName
        {
            get { return this.Get(x => x.NewSerieName); }
            set { this.Set(x => x.NewSerieName, value); }
        }

        public int? NewSerieSeason
        {
            get { return this.Get(x => x.NewSerieSeason); }
            set { this.Set(x => x.NewSerieSeason, value); }
        }

        public int? NewSerieCurrentEpisode
        {
            get { return this.Get(x => x.NewSerieCurrentEpisode); }
            set { this.Set(x => x.NewSerieCurrentEpisode, value); }
        }

        public int? NewSerieMaxEpisodes
        {
            get { return this.Get(x => x.NewSerieMaxEpisodes); }
            set { this.Set(x => x.NewSerieMaxEpisodes, value); }
        }

        public string NewSerieComment
        {
            get { return this.Get(x => x.NewSerieComment); }
            set { this.Set(x => x.NewSerieComment, value); }
        }

        public string NewSerieType
        {
            get { return this.Get(x => x.NewSerieType); }
            set { this.Set(x => x.NewSerieType, value); }
        }

        private void SetAsDropped()
        {
            if (this.model != this.stepModel.Dropped)
            {
                this.stepModel.Dropped.Series.Add(this.SelectedItem);
                this.Series.Remove(this.SelectedItem);
                this.SaveLocally();
            }
        }

        private void SetAsPlanToWatch()
        {
            if (this.model != this.stepModel.PlanToWatch)
            {
                this.stepModel.PlanToWatch.Series.Add(this.SelectedItem);
                this.Series.Remove(this.SelectedItem);
                this.SaveLocally();
            }
        }

        private void SetAsOnHold()
        {
            if (this.model != this.stepModel.OnHold)
            {
                this.stepModel.OnHold.Series.Add(this.SelectedItem);
                this.Series.Remove(this.SelectedItem);
                this.SaveLocally();
            }
        }

        private void SetAsCurrentlyWatching()
        {
            if (this.model != this.stepModel.CurrentlyWatching)
            {
                this.stepModel.CurrentlyWatching.Series.Add(this.SelectedItem);
                this.Series.Remove(this.SelectedItem);
                this.SaveLocally();
            }
        }

        private void SetAsCompleted()
        {
            if (this.model != this.stepModel.Completed)
            {
                this.dispatcher.Invoke(new Action(() =>
                {
                    this.stepModel.Completed.Series.Add(this.SelectedItem);
                    this.Series.Remove(this.SelectedItem);
                    this.SaveLocally();
                }));
            }
        }

        private void ForceReloadStream()
        {
            Task.Factory.StartNew(() =>
            {
                this.IsBusy = true;

                this.SelectedItem.Stream = this.controller.GenerateNextEpisodeStream(this.SelectedItem);

                this.SaveLocally();
                this.IsBusy = false;
            });  
        }

        private void ReloadAll()
        {
            Task.Factory.StartNew(() =>
            {
                this.IsBusy = true;

                foreach (ISerie serie in Series)
                {
                    this.controller.GenerateNextEpisodeStream(serie);
                }

                this.SaveLocally();
                this.IsBusy = false;
            });
        }

        private bool ValidateNewSerie()
        {
            return !string.IsNullOrWhiteSpace(this.NewSerieName)
                   && this.NewSerieMaxEpisodes > 0;
        }

        private void OpenAddNewSerie()
        {
            this.NewSerieMaxEpisodes = null;
            this.NewSerieCurrentEpisode = null;
            this.NewSerieSeason = null;
            this.NewSerieName = string.Empty;
            this.NewSerieComment = string.Empty;
            this.NewSerieType = "TV";
            this.IsAddNewSerieVisible = true;
            this.isSerieInEditMode = false;
        }

        private void AddNewSerie()
        {
            int currentEpisode = 0;
            if (this.NewSerieCurrentEpisode.HasValue)
            {
                currentEpisode = this.NewSerieCurrentEpisode.Value;
            }

            if (this.isSerieInEditMode)
            {
                this.SelectedItem.Progress.CurrentEpisode = currentEpisode;
                this.SelectedItem.Progress.LastEpisode = this.NewSerieMaxEpisodes.Value;
                this.SelectedItem.Progress.Season = this.NewSerieSeason.Value;
                this.SelectedItem.Title = this.NewSerieName;
                this.SelectedItem.Type = this.NewSerieType;
                this.SelectedItem.Comment = this.NewSerieComment;

                this.IsAddNewSerieVisible = false;
                this.isSerieInEditMode = false;
                return;
            }

            var progress = new Progress(currentEpisode, this.NewSerieMaxEpisodes.Value, this.NewSerieSeason != null ? this.NewSerieSeason : null);
            ISerie serie = new Serie(this.Series.Count + 1, this.NewSerieName, progress, 0, this.NewSerieType, this.NewSerieComment, string.Empty);
            serie.Progress.PropertyChanged += this.HandleProgressChanged;
            serie.PropertyChanged += this.HandlePropertyChanged;
            this.SelectedItem = serie;
            this.Series.Add(serie);

            this.IsAddNewSerieVisible = false;

            Task.Factory.StartNew(() =>
            {
                this.IsBusy = true;

                this.SelectedItem.Stream = this.controller.GenerateNextEpisodeStream(this.SelectedItem);

                this.IsBusy = false;
            });  
        }

        private void EditCurrentSerie()
        {
            this.IsAddNewSerieVisible = true;
            this.isSerieInEditMode = true;
            this.NewSerieCurrentEpisode = this.SelectedItem.Progress.CurrentEpisode;
            this.NewSerieMaxEpisodes = this.SelectedItem.Progress.LastEpisode;
            this.NewSerieSeason = this.SelectedItem.Progress.Season;
            this.NewSerieName = this.SelectedItem.Title;
            this.NewSerieComment = this.SelectedItem.Comment;
            this.NewSerieType = this.SelectedItem.Type;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != this.GetPropertyName(x => x.SelectedItem.Stream))
            {
                Task.Factory.StartNew(() =>
                {
                    this.IsBusy = true;
                    this.SaveLocally();
                    this.IsBusy = false;
                });  
            }
        }

        private void HandleProgressChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(x => x.SelectedItem.Progress.CurrentEpisode))
            {
                Task.Factory.StartNew(() =>
                {
                    this.IsBusy = true;
                    Thread.Sleep(500);
                    this.SelectedItem.Stream = this.controller.GenerateNextEpisodeStream(this.SelectedItem);

                    if (this.model == this.stepModel.CurrentlyWatching)
                    {
                        if (this.SelectedItem.Progress.CurrentEpisode == this.SelectedItem.Progress.LastEpisode)
                        {
                            this.SetAsCompleted();
                        }
                    }

                    this.controller.SaveHistory(new HistoryItem("Episode " + this.SelectedItem.Progress.CurrentEpisode, DateTime.Now.ToString(), this.SelectedItem.Title));
                    this.SaveLocally();
                    this.IsBusy = false;
                });  
            }
        }

        private void SetupExistingHooks()
        {
            foreach (var serie in Series)
            {
                serie.Progress.PropertyChanged += this.HandleProgressChanged;
                serie.PropertyChanged += this.HandlePropertyChanged;
            }
        }

        private void SaveLocally()
        {
            this.controller.SaveData();
        }

        private void HandleIsBusyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.GetPropertyName(x => x.IsBusy) == e.PropertyName && !this.isBusyChangedBlock)
            {
                this.isBusyChangedBlock = true;
                this.stepModel.Completed.IsBusy = this.IsBusy;
                this.stepModel.CurrentlyWatching.IsBusy = this.IsBusy;
                this.stepModel.Dropped.IsBusy = this.IsBusy;
                this.stepModel.OnHold.IsBusy = this.IsBusy;
                this.stepModel.PlanToWatch.IsBusy = this.IsBusy;
                this.isBusyChangedBlock = false;
            }
        }
    }
}