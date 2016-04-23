using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StreamCompanion.Contract
{
    public interface ISerie : INotifyPropertyChanged
    {
        int Number { get; set; }

        string Title { get; set; }

        IProgress Progress { get; set; }

        double Rating { get; set; }

        [JsonIgnore]
        ObservableCollection<string> Types { get; }

        string Type { get; set; }

        string Comment { get; set; }

        string Stream { get; set; }
    }
}
