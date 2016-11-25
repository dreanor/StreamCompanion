using helper.utils.baseclasses;
using StreamCompanion.Contract.ShellBase.Model;

namespace StreamCompanion.Model
{
    public class StepModel : NotifyPropertyChangedBase<IStepModel>, IStepModel
    {
        public StepModel()
        {
            this.CurrentlyWatching = new Model();
            this.Completed = new Model();
            this.OnHold = new Model();
            this.Dropped = new Model();
            this.PlanToWatch = new Model();
        }

        public IModel CurrentlyWatching
        {
            get { return this.Get(x => x.CurrentlyWatching); }
            internal set { this.Set(x => x.CurrentlyWatching, value); }
        }

        public IModel Completed
        {
            get { return this.Get(x => x.Completed); }
            internal set { this.Set(x => x.Completed, value); }
        }

        public IModel OnHold
        {
            get { return this.Get(x => x.OnHold); }
            internal set { this.Set(x => x.OnHold, value); }
        }

        public IModel Dropped
        {
            get { return this.Get(x => x.Dropped); }
            internal set { this.Set(x => x.Dropped, value); }
        }

        public IModel PlanToWatch
        {
            get { return this.Get(x => x.PlanToWatch); }
            internal set { this.Set(x => x.PlanToWatch, value); }
        }
    }
}
