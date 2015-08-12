using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.uic.schritt;

namespace com.gmail.mikeundead.streamcompanion.contract.shellbase.shell
{
    public interface IShellViewModel : INotifyPropertyChanged
    {
        ObservableCollection<IStepUIC> Steps { get; }

        IStepUIC SelectedItem { get; set; }

        ICommand OpenInBrowserCmd { get; }

        ICommand EditStreamTemplatesCmd { get; }

        ICommand ShowChangelogCmd { get; }

        ICommand ContactCmd { get; }

        ICommand HelpCmd { get; }

        ICommand OpenOptionsCmd { get; }

        ICommand OpenHelpCmd { get; }

        bool AreOptionsVisible { get; }

        bool IsHelpVisible { get; }

        bool IsEnabled { get; }
    }
}