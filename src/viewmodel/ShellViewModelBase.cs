using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.shell;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.uic.schritt;
using com.gmail.mikeundead.streamcompanion.streamtemplate;
using helper.mvvm.baseclasses;
using helper.mvvm.commands;

namespace com.gmail.mikeundead.streamcompanion.shellviewmodel
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
            this.SelectedItem = this.Steps[0];

            this.OpenInBrowserCmd = new ActionCommand(() => Process.Start("https://dl.dropboxusercontent.com/u/24979157/development/streamcompanion/index.html"));
            this.EditStreamTemplatesCmd = new ActionCommand(this.EditStreamTemplates);
            this.ContactCmd = new ActionCommand(() => Process.Start("mailto:mikeundead@gmail.com"));
            this.OpenOptionsCmd = new ActionCommand(() => { this.AreOptionsVisible = true; this.IsHelpVisible = false; });
            this.OpenHelpCmd = new ActionCommand(() => { this.AreOptionsVisible = false; this.IsHelpVisible = true; });
            this.HelpCmd = new ActionCommand(() => Process.Start("https://bitbucket.org/Dreanor/stream-companion/wiki/How%20to%20use"));
            this.ShowChangelogCmd = new ActionCommand(() => Process.Start(controller.ChangelogPath));
        }

        public ObservableCollection<IStepUIC> Steps
        {
            get { return this.Get(x => x.Steps); }
            internal set { this.Set(x => x.Steps, value); }
        }

        public IStepUIC SelectedItem
        {
            get { return this.Get(x => x.SelectedItem); }
            set { this.Set(x => x.SelectedItem, value); }
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
