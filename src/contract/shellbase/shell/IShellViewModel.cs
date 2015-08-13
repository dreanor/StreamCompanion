using System.Collections.ObjectModel;
using System.ComponentModel;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.uic.schritt;
using helper.mvvm.commands;

namespace com.gmail.mikeundead.streamcompanion.contract.shellbase.shell
{
    public interface IShellViewModel : INotifyPropertyChanged
    {
        ObservableCollection<IStepUIC> Steps { get; }

        IStepUIC SelectedItem { get; set; }

        ActionCommand OpenInBrowserCmd { get; }

        ActionCommand EditStreamTemplatesCmd { get; }

        ActionCommand ShowChangelogCmd { get; }

        ActionCommand ContactCmd { get; }

        ActionCommand HelpCmd { get; }

        ActionCommand OpenOptionsCmd { get; }

        ActionCommand OpenHelpCmd { get; }

        bool AreOptionsVisible { get; }

        bool IsHelpVisible { get; }

        bool IsEnabled { get; }
    }
}