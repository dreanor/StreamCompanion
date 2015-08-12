using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public interface ISerie : INotifyPropertyChanged
    {
        int Number { get; set; }

        string Title { get; set; }

        IProgress Progress { get; set; }

        int Rating { get; set; }

        [JsonIgnore]
        ObservableCollection<string> Types { get; }

        string Type { get; set; }

        string Comment { get; set; }

        string Stream { get; set; }
    }
}
