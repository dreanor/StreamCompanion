using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace com.gmail.mikeundead.streamcompanion.contract.streamtemplate
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ObservableCollection<IStreamItem> Streams { get; }

        IStreamItem SelectedItem { get; set; }

        ICommand SaveAndExitCmd { get; }

        ICommand AddStreamWebsiteCmd { get; }

        ICommand EditStreamWebsiteCmd { get; }

        ICommand MoveUpCmd { get; }

        ICommand MoveDownCmd { get; }

        ICommand DoneCmd{ get; }

        ICommand CancelCmd { get; }

        bool IsDone { get; }

        bool IsStreamingWebsiteVisible { get; set; }

        string NewStreamingWebsite { get; set; }

        string WhitespaceReplacement { get; set; }

        string GenericUrl { get; set; }

        string UsedOnTypes { get; set; }

        bool IsGeneral{ get; set; }

        bool IsTv{ get; set; }

        bool IsAnime{ get; set; }

        bool IsCartoon{ get; set; }

        bool IsMovie{ get; set; }

        string SelectedStreamLanguage { get; set; }

        ObservableCollection<string> StreamLanguages { get; } 
    }
}
