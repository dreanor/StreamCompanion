using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ICommand AddSerieCmd { get; }

        ICommand EditSerieCmd { get; }

        ICommand ForceReloadStreamCmd { get; }

        ICommand SetAsCurrentlyWatchingCmd { get; }

        ICommand SetAsCompletedCmd { get; }

        ICommand SetAsOnHoldCmd { get; }

        ICommand SetAsDroppedCmd { get; }

        ICommand SetAsPlanToWatchCmd { get; }

        ICommand DoneCmd { get; }

        ICommand CancelCmd { get; }

        ISerie SelectedItem { get; set; }

        ObservableCollection<ISerie> Series { get; }

        ObservableCollection<string> Types { get; }

        bool IsBusy { get; }

        bool IsAddNewSerieVisible { get; set; }

        string NewSerieName{ get; set; }

        int? NewSerieSeason { get; set; }

        int? NewSerieCurrentEpisode { get; set; }

        int? NewSerieMaxEpisodes{ get; set; }

        string NewSerieComment{ get; set; }

        string NewSerieType { get; set; }
    }

    public enum SerieType
    {
        TV,
        Anime,
        Cartoon,
        Movie,
        OVA,
        Special,
        Mixed
    }

    public enum Language
    {
        Deutsch,
        English,
        Français,
        Español
    }
}
