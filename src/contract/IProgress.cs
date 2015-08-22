using System.ComponentModel;

namespace StreamCompanion.Contract
{
    public interface IProgress : INotifyPropertyChanged
    {
        int CurrentEpisode { get; set; }

        int LastEpisode { get; set; }

        int? Season { get; set; }

        string EpisodeDisplay { get; set; }
    }
}
