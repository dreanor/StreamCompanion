using System.ComponentModel;

namespace StreamCompanion.Contract.ShellBase.Model
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
