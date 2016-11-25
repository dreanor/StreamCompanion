using helper.utils.baseclasses;
using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Uic;

namespace StreamCompanion.Uic.TabControlUic
{
    public class Step : NotifyPropertyChangedBase<ISerieStepUIC>, ISerieStepUIC
    {
        public Step(string header, IUICView view)
        {
            this.Header = header;
            this.View = view;
        }

        public virtual IUICView View
        {
            get { return this.Get(x => x.View); }
            private set { this.Set(x => x.View, value); }
        }

        public string Header
        {
            get { return this.Get(x => x.Header); }
            private set { this.Set(x => x.Header, value); }
        }
    }
}
