using StreamCompanion.Contract.StreamTemplate;
using System.ComponentModel;

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
