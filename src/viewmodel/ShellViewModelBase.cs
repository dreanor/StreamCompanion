using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Shell;
using StreamCompanion.Contract.ShellBase.Uic.Step;
using StreamCompanion.StreamTemplate;
using helper.mvvm.baseclasses;
using helper.mvvm.commands;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StreamCompanion.ShellViewModel
{ 
    public abstract class ShellViewModelBase : ViewModelBase<IShellViewModel>, IShellViewModel
    {
        private readonly IController controller;
        private StreamTemplateView tempStreamTemplateView;

        protected ShellViewModelBase(IController controller, params IStepUIC[] stepUics)
        {
            this.IsEnabled = true;
            this.controller = controller;
            this.Steps = new ObservableCollection<IStepUIC>(stepUics);
            this.History = new ObservableCollection<IHistoryItem>(controller.LoadHistory());
            this.Statistic = controller.LoadStatistic();
            this.SelectedItem = this.Steps[0];

            this.OpenInBrowserCmd = new ActionCommand(() => Process.Start("https://dreanor.github.io/StreamCompanion/"));
            this.EditStreamTemplatesCmd = new ActionCommand(this.EditStreamTemplates);
            this.ContactCmd = new ActionCommand(() => Process.Start("mailto:mikeundead@gmail.com"));
            this.OpenOptionsCmd = new ActionCommand(() => { this.AreOptionsVisible = true; this.IsHelpVisible = false; });
            this.OpenHelpCmd = new ActionCommand(() => { this.AreOptionsVisible = false; this.IsHelpVisible = true; });
            this.HelpCmd = new ActionCommand(() => Process.Start("https://github.com/dreanor/StreamCompanion/wiki"));
            this.ShowChangelogCmd = new ActionCommand(() => Process.Start("https://github.com/dreanor/StreamCompanion/wiki/Changelog"));
            this.PropertyChanged += OnPropertyChanged;
        }

        public IStatistic Statistic
        {
            get { return this.Get(x => x.Statistic); }
            internal set { this.Set(x => x.Statistic, value); }
        }

        public ObservableCollection<IStepUIC> Steps
        {
            get { return this.Get(x => x.Steps); }
            internal set { this.Set(x => x.Steps, value); }
        }

        public ObservableCollection<IHistoryItem> History
        {
            get { return this.Get(x => x.History); }
            internal set { this.Set(x => x.History, value); }
        }

        public IStepUIC SelectedItem
        {
            get { return this.Get(x => x.SelectedItem); }
            set { this.Set(x => x.SelectedItem, value); }
        }

        public int SelectedTabIndex
        {
            get { return this.Get(x => x.SelectedTabIndex); }
            set { this.Set(x => x.SelectedTabIndex, value); }
        }

        public ActionCommand OpenInBrowserCmd { get; private set; }

        public ActionCommand EditStreamTemplatesCmd { get; private set; }

        public ActionCommand HelpCmd { get; private set; }

        public ActionCommand ShowChangelogCmd { get; private set; }

        public ActionCommand ContactCmd { get; private set; }

        public ActionCommand OpenOptionsCmd { get; private set; }

        public ActionCommand OpenHelpCmd { get; private set; }

        public bool AreOptionsVisible
        {
            get { return this.Get(x => x.AreOptionsVisible); }
            set { this.Set(x => x.AreOptionsVisible, value); }
        }

        public bool IsHelpVisible
        {
            get { return this.Get(x => x.IsHelpVisible); }
            set { this.Set(x => x.IsHelpVisible, value); }
        }

        public bool IsEnabled
        {
            get { return this.Get(x => x.IsEnabled); }
            private set { this.Set(x => x.IsEnabled, value); }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(x => x.SelectedTabIndex) && SelectedTabIndex == 1)
            {
                this.History = new ObservableCollection<IHistoryItem>(controller.LoadHistory().OrderByDescending(x => x.LastModified));
            }
        }

        private void EditStreamTemplates()
        {
            this.IsEnabled = false;
            var viewModel = new ViewModel(this.controller);
            viewModel.PropertyChanged += this.HandlePropertyChanged;
            this.tempStreamTemplateView = new StreamTemplateView(viewModel);
            this.tempStreamTemplateView.Closing += this.HandleUnexpectedClosing;
        }

        private void HandleUnexpectedClosing(object sender, CancelEventArgs e)
        {
            this.IsEnabled = true;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDone")
            {
                this.tempStreamTemplateView.Close();
                this.IsEnabled = true;
            }
        }
    }
}
