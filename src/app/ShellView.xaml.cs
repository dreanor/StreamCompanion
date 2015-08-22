using StreamCompanion.Contract.ShellBase.Shell;
using System.Deployment.Application;
using System.Reflection;
using System.Windows.Forms;

namespace StreamCompanion.App
{
    public partial class ShellView : IShellView
    {
        public ShellView(IShellViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.Title = string.Format("{0} - {1}", this.Title,  Application.ProductVersion);
        }
    }
}
