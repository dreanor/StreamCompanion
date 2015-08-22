using System.ComponentModel;

namespace StreamCompanion.Contract.ShellBase.Uic.Step
{
    public interface IStepUIC : INotifyPropertyChanged
    {
        string Header { get; }

        IUICView View { get; }
    }
}