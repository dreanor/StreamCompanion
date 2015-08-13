using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ActionCommand AddSerieCmd { get; }

        ActionCommand EditSerieCmd { get; }

        ActionCommand ForceReloadStreamCmd { get; }

        ActionCommand SetAsCurrentlyWatchingCmd { get; }

        ActionCommand SetAsCompletedCmd { get; }

        ActionCommand SetAsOnHoldCmd { get; }

        ActionCommand SetAsDroppedCmd { get; }

        ActionCommand SetAsPlanToWatchCmd { get; }

        ActionCommand DoneCmd { get; }

        ActionCommand CancelCmd { get; }

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
