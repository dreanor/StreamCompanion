using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.uic.schritt;

namespace com.gmail.mikeundead.streamcompanion.shellviewmodel
{
    public class ShellViewModel : ShellViewModelBase
    {
        public ShellViewModel(IController controller, params IStepUIC[] steps)
            : base(controller, steps)
        {
        }
    }
}