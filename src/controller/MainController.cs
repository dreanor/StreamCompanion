using StreamCompanion.Contract;
using StreamCompanion.Contract.Json;
using StreamCompanion.Contract.Json.Deserialize;
using StreamCompanion.Contract.ShellBase.Model;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.Controller.Properties;
using StreamCompanion.JsonConverter;
using StreamCompanion.StreamTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IModel = StreamCompanion.Contract.StreamTemplate.IModel;

namespace StreamCompanion.Controller
{
    public class MainController : IController
    {
        private readonly IConverter converter;
        private readonly IStepModel stepModel;
        private readonly IStreamManager streamManager;
        private readonly SemaphoreSlim semaphoreSlim;
        private List<IStreamItem> cachedStreams;
        private string streamTemplatePath;
        private string folderPath;
        private string rootPath;
        private string dataPath;
        private Guid userId;

        public MainController(IStepModel stepModel)
        {
            this.converter = new Converter();
            this.streamManager = new StreamManager();
            this.stepModel = stepModel;
            this.rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion"); 
            this.folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings");
            this.dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\data.json");
            this.streamTemplatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\streams.json");;
            this.semaphoreSlim = new SemaphoreSlim(1);

            this.TryLoadSettings();
        }

        public string ChangelogPath { get; private set; }

        public string GenerateNextEpisodeStream(ISerie serie)
        {
            return this.streamManager.GenerateNextEpisodeStream(serie, this.cachedStreams);
        }

        public void SaveData()
        {
            this.semaphoreSlim.Wait();
            var root = this.converter.Convert(this.userId, this.stepModel.CurrentlyWatching.Series, this.stepModel.Completed.Series, this.stepModel.OnHold.Series, this.stepModel.Dropped.Series, this.stepModel.PlanToWatch.Series);
            this.converter.Export(root, this.dataPath);
            this.semaphoreSlim.Release();
        }
        
        public void SaveStreamTemplates(IModel model)
        {
            this.converter.ExportStreams(model, this.streamTemplatePath);
            this.cachedStreams = model.Streams;
        }

        public IEnumerable<IStreamItem> LoadStreamTemplates()
        {
            var streams = this.converter.ImportStreams(this.streamTemplatePath).Streams;
            this.cachedStreams = streams;
            return streams;
        }

        private void TryLoadSettings()
        {
            if (Directory.Exists(this.folderPath))
            {
                this.LoadData();
                this.LoadStreamTemplates();
            }
            else
            {
                this.userId = Guid.NewGuid();
                Directory.CreateDirectory(this.folderPath);
                this.SaveData();
                this.SaveStreamTemplates(this.GetDefaultStreams());
            }

            this.CopyChangelog();
        }

        private void CopyChangelog()
        {
            const string changelog = "changelog.txt";
            string[] words = Resources.Changelog.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            File.WriteAllLines(Path.Combine(rootPath, changelog), words);

            this.ChangelogPath = Path.Combine(this.rootPath, changelog);
        }

        private IModel GetDefaultStreams()
        {
            var streams = new List<StreamItem>();
            streams.Add(new StreamItem("http://www.watchcartoononline.com/{0}-season-{1}-episode-{2}", "-"));
            streams.Add(new StreamItem("http://kinox.to/Stream/{0}-1.html,s{1}e{2}", "_"));
            streams.Add(new StreamItem("http://watchseries.lt/episode/{0}_s{1}_e{2}.html", "_"));
            return new Model(streams);
        }

        private void LoadData()
        {
            var root = this.converter.Import(this.dataPath);

            new Task(() => this.LoadData(root)).Start();
        }

        private void LoadData(IDeserializedRoot root)
        {
            foreach (var serie in root.CurrentlyWatching)
            {
                this.stepModel.CurrentlyWatching.Series.Add(serie);
            }

            foreach (var serie in root.Completed)
            {
                this.stepModel.Completed.Series.Add(serie);
            }

            foreach (var serie in root.OnHold)
            {
                this.stepModel.OnHold.Series.Add(serie);
            }

            foreach (var serie in root.Dropped)
            {
                this.stepModel.Dropped.Series.Add(serie);
            }

            foreach (var serie in root.PlanToWatch)
            {
                this.stepModel.PlanToWatch.Series.Add(serie);
            }

            this.userId = root.Id;
        }
    }
}
