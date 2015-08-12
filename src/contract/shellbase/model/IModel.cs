using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.gmail.mikeundead.streamcompanion.contract.shellbase.model
{
    public interface IModel : INotifyPropertyChanged
    {
        ObservableCollection<ISerie> Series { get; set; }

        bool IsBusy { get; set; }
    }
}
