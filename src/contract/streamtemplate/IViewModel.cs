using helper.mvvm.commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace com.gmail.mikeundead.streamcompanion.contract.streamtemplate
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ObservableCollection<IStreamItem> Streams { get; }

        IStreamItem SelectedItem { get; set; }

        ActionCommand SaveAndExitCmd { get; }

        ActionCommand AddStreamWebsiteCmd { get; }

        ActionCommand EditStreamWebsiteCmd { get; }

        ActionCommand MoveUpCmd { get; }

        ActionCommand MoveDownCmd { get; }

        ActionCommand DoneCmd { get; }

        ActionCommand CancelCmd { get; }

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
