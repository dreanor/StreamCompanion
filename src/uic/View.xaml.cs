using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using com.gmail.mikeundead.streamcompanion.contract;

namespace com.gmail.mikeundead.streamcompanion.uic
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
