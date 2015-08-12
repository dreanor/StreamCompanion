using System.ComponentModel;

namespace com.gmail.mikeundead.streamcompanion.contract.streamtemplate
{
    public interface IStreamItem : INotifyPropertyChanged
    {
        string Website { get; set; }

        string WhitespaceReplacement { get; set; }

        string UsedOnTypes { get; set; }

        string StreamLanguage { get; set; }
    }
}