using StreamCompanion.Contract.StreamTemplate;

namespace StreamCompanion.StreamTemplate
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
