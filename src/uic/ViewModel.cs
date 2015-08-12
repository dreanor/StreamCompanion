using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.model;

namespace com.gmail.mikeundead.streamcompanion.uic
{
    public class ViewModel : SeriesBase
    {
        public ViewModel(IStepModel stepModel, IModel model, IController controller)
            : base(stepModel, model, controller)
        {
        }
    }
}