using StreamCompanion.Contract;
using StreamCompanion.Contract.ShellBase.Model;
using StreamCompanion.Contract.ShellBase.Shell;
using StreamCompanion.Controller;
using StreamCompanion.Model;
using StreamCompanion.ShellViewModel;
using StreamCompanion.Uic;
using System.Windows;

namespace StreamCompanion.App
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            IStepModel stepModel = new StepModel();
            IController controller = new MainController(stepModel);

            ISerieStepUIC currentlyWatching = new Step("Currently Watching", new View(new ViewModel(stepModel, stepModel.CurrentlyWatching, controller)));
            ISerieStepUIC completed = new Step("Completed", new View(new ViewModel(stepModel, stepModel.Completed, controller)));
            ISerieStepUIC onHold = new Step("On Hold", new View(new ViewModel(stepModel, stepModel.OnHold, controller)));
            ISerieStepUIC dropped = new Step("Dropped", new View(new ViewModel(stepModel, stepModel.Dropped, controller)));
            ISerieStepUIC planToWatch = new Step("Plan To Watch", new View(new ViewModel(stepModel, stepModel.PlanToWatch, controller)));

            IShellViewModel shellViewmodel = new MainShellViewModel(controller, currentlyWatching, completed, onHold, dropped, planToWatch);
            var shellView = new ShellView(shellViewmodel);
            shellView.Show();
        }
    }
}
