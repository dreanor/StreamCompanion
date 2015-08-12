using System.ComponentModel;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public interface IProgress : INotifyPropertyChanged
    {
        int CurrentEpisode { get; set; }

        int LastEpisode { get; set; }

        int? Season { get; set; }

        string EpisodeDisplay { get; set; }
    }
}
