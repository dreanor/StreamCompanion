using System.Deployment.Application;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.shell;

namespace com.gmail.mikeundead.streamcompanion.app
{
    public partial class ShellView : IShellView
    {
        public ShellView(IShellViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                this.Title = string.Format("{0} - {1}.{2}.{3}.{4} (dev)", this.Title, version.Major, version.Minor, version.Build, version.Revision);
            }
        }
    }
}
