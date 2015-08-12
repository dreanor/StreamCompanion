using System.Windows;
using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.model;
using com.gmail.mikeundead.streamcompanion.contract.shellbase.shell;
using com.gmail.mikeundead.streamcompanion.controller;
using com.gmail.mikeundead.streamcompanion.model;
using com.gmail.mikeundead.streamcompanion.shellviewmodel;
using com.gmail.mikeundead.streamcompanion.uic;
using DropNet;

namespace com.gmail.mikeundead.streamcompanion.app
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var dropNet = new DropNetClient("hidden kappa", "hidden kappa", "hidden kappa", "hidden kappa");

            IStepModel stepModel = new StepModel();
            IController controller = new Controller(stepModel, dropNet);

            ISerieStepUIC currentlyWatching = new Step("Currently Watching", new View(new ViewModel(stepModel, stepModel.CurrentlyWatching, controller)));
            ISerieStepUIC completed = new Step("Completed", new View(new ViewModel(stepModel, stepModel.Completed, controller)));
            ISerieStepUIC onHold = new Step("On Hold", new View(new ViewModel(stepModel, stepModel.OnHold, controller)));
            ISerieStepUIC dropped = new Step("Dropped", new View(new ViewModel(stepModel, stepModel.Dropped, controller)));
            ISerieStepUIC planToWatch = new Step("Plan To Watch", new View(new ViewModel(stepModel, stepModel.PlanToWatch, controller)));

            IShellViewModel shellViewmodel = new ShellViewModel(controller, currentlyWatching, completed, onHold, dropped, planToWatch);
            var shellView = new ShellView(shellViewmodel);
            shellView.Show();
        }
    }
}
