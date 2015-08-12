using System.Collections.ObjectModel;
using System.Collections.Specialized;
using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.model;
using helper.utils.baseclasses;

namespace com.gmail.mikeundead.streamcompanion.model
{
    public class Model : NotifyPropertyChangedBase<IModel>, IModel
    {
        public Model()
        {
            this.Series = new ObservableCollection<ISerie>();
            this.Series.CollectionChanged += this.OnCollectionChanged;
        }

        public ObservableCollection<ISerie> Series
        {
            get { return this.Get(x => x.Series); }
            set { this.Set(x => x.Series, value); }
        }

        public bool IsBusy
        {
            get { return this.Get(x => x.IsBusy); }
            set { this.Set(x => x.IsBusy, value); }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            for (int i = 0; i < this.Series.Count; i++)
            {
                this.Series[i].Number = i + 1;
            }
        }
    }
}
