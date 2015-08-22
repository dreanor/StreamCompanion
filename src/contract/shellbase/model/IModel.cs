using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StreamCompanion.Contract.ShellBase.Model
{
    public interface IModel : INotifyPropertyChanged
    {
        ObservableCollection<ISerie> Series { get; set; }

        bool IsBusy { get; set; }
    }
}
