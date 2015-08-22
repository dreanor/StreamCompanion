using System.ComponentModel;

namespace StreamCompanion.Contract.StreamTemplate
{
    public interface IStreamItem : INotifyPropertyChanged
    {
        string Website { get; set; }

        string WhitespaceReplacement { get; set; }

        string UsedOnTypes { get; set; }

        string StreamLanguage { get; set; }
    }
}