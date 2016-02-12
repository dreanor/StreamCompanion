using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Octokit;
using StreamCompanion.Contract.ShellBase.Shell;
using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
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
            //CheckForNewVersion();
        }

        private async void CheckForNewVersion()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("StreamCompanion"));
            var user = client.User.Get("dreanor");
            var releases = await client.Release.GetAll("dreanor", "StreamCompanion");
            var latest = releases[0];

            if (!latest.TagName.Equals(Application.ProductVersion.ToString()))
            {
                string releaseUrl = "https://github.com/dreanor/StreamCompanion/releases/tag/" + latest.TagName;

                this.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Theme;
                this.MetroDialogOptions.NegativeButtonText = "Not now";
                this.MetroDialogOptions.AffirmativeButtonText = "Visit GitHub";

                var result = await this.ShowMessageAsync("New Version!", "A new release is available for download on GitHub.", 
                    MessageDialogStyle.AffirmativeAndNegative, 
                    this.MetroDialogOptions);

                if(result == MessageDialogResult.Affirmative)
                {
                    Process.Start(releaseUrl);
                }
            }
        }
    }
}
