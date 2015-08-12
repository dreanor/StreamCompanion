using System.ComponentModel;

namespace com.gmail.mikeundead.streamcompanion.contract.shellbase.model
{
    public interface IStepModel : INotifyPropertyChanged
    {
        IModel CurrentlyWatching { get; }

        IModel Completed { get; }

        IModel OnHold { get; }

        IModel Dropped { get; }

        IModel PlanToWatch { get; }
    }
}
