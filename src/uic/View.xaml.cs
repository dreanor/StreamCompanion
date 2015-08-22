using StreamCompanion.Contract;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace StreamCompanion.Uic
{
    public partial class View : IView
    {
        public View(IViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OpenClickedLink(object sender, RoutedEventArgs e)
        {
            var link = (Hyperlink)e.OriginalSource;
            Process.Start(link.NavigateUri.AbsoluteUri);
        }
    }
}
