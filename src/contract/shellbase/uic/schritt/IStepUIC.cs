using System.ComponentModel;

namespace com.gmail.mikeundead.streamcompanion.contract.shellbase.uic.schritt
{
    public interface IStepUIC : INotifyPropertyChanged
    {
        string Header { get; }

        IUICView View { get; }
    }
}