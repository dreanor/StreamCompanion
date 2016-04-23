using helper.mvvm.commands;
using StreamCompanion.Contract.ShellBase.Uic.Step;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StreamCompanion.Contract.ShellBase.Shell
{
    public interface IShellViewModel : INotifyPropertyChanged
    {
        ObservableCollection<IStepUIC> Steps { get; }

        ObservableCollection<IHistoryItem> History { get; }

        IStatistic Statistic { get; }

        int SelectedTabIndex { get; set; }

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