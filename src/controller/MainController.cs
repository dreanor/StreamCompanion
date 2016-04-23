using StreamCompanion.Contract;
using StreamCompanion.Contract.Json;
using StreamCompanion.Contract.ShellBase.Model;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.ItemViewModel;
using StreamCompanion.JsonConverter;
using StreamCompanion.StreamTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IModel = StreamCompanion.Contract.StreamTemplate.IModel;

namespace StreamCompanion.Controller
{
    public class MainController : IController
    {
        #region fields
        private readonly IConverter converter;
        private readonly IStepModel stepModel;
        private readonly IStreamManager streamManager;
        private readonly SemaphoreSlim semaphoreSlim;
        private List<IStreamItem> cachedStreams;
        private string streamTemplatePath;
        private string folderPath;
        private string rootPath;
        private string dataPath;
        private string historyPath;
        private Guid userId;
        #endregion

        #region ctor
        public MainController(IStepModel stepModel)
        {
            this.converter = new Converter();
            this.streamManager = new StreamManager();
            this.stepModel = stepModel;
            this.rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion"); 
            this.folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings");
            this.dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\data.json");
            this.streamTemplatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\streams.json");;
            this.historyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\history.json"); ;
            this.semaphoreSlim = new SemaphoreSlim(1);
            this.TryLoadSettings();
        }
        #endregion

        #region Public Methods
        public IStatistic LoadStatistic()
        {
            return new Statistic(GetAllEpisodeCount(), GetEpisodeMeanScore(), GetScoreDistribution(), GetSerieCount());
        }
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
            if (File.Exists(this.streamTemplatePath))
            {
                var streams = this.converter.ImportStreams(this.streamTemplatePath).Streams;
                this.cachedStreams = streams;
                return streams;
            }
            else
            {
                IModel tmpModel = this.GetDefaultStreams();
                SaveStreamTemplates(tmpModel);
                return tmpModel.Streams;
            }
        }

        public void SaveHistory(IHistoryItem history)
        {
            this.converter.SaveHistory(history, this.historyPath);
        }

        public List<IHistoryItem> LoadHistory()
        {
            return this.converter.LoadHistory(this.historyPath);
        }
        #endregion

        #region Data Loading
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
        }

        private IModel GetDefaultStreams()
        {
            var streams = new List<StreamItem>();
            streams.Add(new StreamItem("http://www.watchcartoononline.com/{0}-season-{1}-episode-{2}", "-"));
            streams.Add(new StreamItem("http://kinox.to/Stream/{0}-1.html,s{1}e{2}", "_"));
            streams.Add(new StreamItem("http://watchseries.lt/episode/{0}_s{1}_e{2}.html", "_"));
            streams.Add(new StreamItem("http://gogoanime.io/{0}-episode-{2}", "-", "Anime", "English"));
            streams.Add(new StreamItem("http://www.animestv.us/watch/{0}-episode-{2}", "-", "Anime", "English"));
            return new Model(streams);
        }

        private void LoadData()
        {
            var root = this.converter.Import(this.dataPath);

            new Task(() => this.LoadData(root)).Start();
        }

        private void LoadData(IConverterRoot root)
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
        #endregion

        #region Statistic
        private Dictionary<int, int> GetScoreDistribution()
        {
            Dictionary<int, int> distribution = new Dictionary<int, int>();

            for (int i = 1; i < 11; i++)
            {
                distribution.Add(i, GetRatingCount(i));
            }

            return distribution;
        }

        private int GetRatingCount(int rating)
        {
            return stepModel.Completed.Series.Where(x => x.Rating == rating).Count()
                 + stepModel.CurrentlyWatching.Series.Where(x => x.Rating == rating).Count()
                 + stepModel.Dropped.Series.Where(x => x.Rating == rating).Count()
                 + stepModel.OnHold.Series.Where(x => x.Rating == rating).Count()
                 + stepModel.PlanToWatch.Series.Where(x => x.Rating == rating).Count();
        }

        private int GetSerieCount()
        {
            return stepModel.Completed.Series.Count()
                + stepModel.CurrentlyWatching.Series.Count()
                + stepModel.Dropped.Series.Count()
                + stepModel.OnHold.Series.Count()
                + stepModel.PlanToWatch.Series.Count();
        }

        private double GetEpisodeMeanScore()
        {
            int count = GetSerieCount();
            double ratings = stepModel.Completed.Series.Sum(x => x.Rating)
                + stepModel.CurrentlyWatching.Series.Sum(x => x.Rating)
                + stepModel.Dropped.Series.Sum(x => x.Rating)
                + stepModel.OnHold.Series.Sum(x => x.Rating)
                + stepModel.PlanToWatch.Series.Sum(x => x.Rating);

            return Math.Round(ratings / count, 2);
        }

        private int GetAllEpisodeCount()
        {
            return stepModel.Completed.Series.Sum(x => x.Number)
                + stepModel.CurrentlyWatching.Series.Sum(x => x.Number)
                + stepModel.Dropped.Series.Sum(x => x.Number)
                + stepModel.OnHold.Series.Sum(x => x.Number)
                + stepModel.PlanToWatch.Series.Sum(x => x.Number);
        }
        #endregion
    }
}
