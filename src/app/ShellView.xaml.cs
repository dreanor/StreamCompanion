using MahApps.Metro.Controls;
using StreamCompanion.Contract.ShellBase.Shell;
using Application = System.Windows.Forms.Application;

namespace StreamCompanion.App
{
    public partial class ShellView : MetroWindow, IShellView
    {
        public ShellView(IShellViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.Title = string.Format("{0} - {1}", this.Title, Application.ProductVersion);
        }
    }
}
