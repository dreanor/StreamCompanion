using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Uic.Step;

namespace StreamCompanion.ShellViewModel
{
    public class MainShellViewModel : ShellViewModelBase
    {
        public MainShellViewModel(IController controller, params IStepUIC[] steps)
            : base(controller, steps)
        {
        }
    }
}