using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;

namespace com.gmail.mikeundead.streamcompanion.streamtemplate
{
    public partial class StreamTemplateView
    {
        public StreamTemplateView(IViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.Show();
        }
    }
}
