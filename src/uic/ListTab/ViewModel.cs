using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Model;

namespace StreamCompanion.Uic.TabControlUic
{
    public class ViewModel : SeriesBase
    {
        public ViewModel(IStepModel stepModel, IModel model, IController controller)
            : base(stepModel, model, controller)
        {
        }
    }
}