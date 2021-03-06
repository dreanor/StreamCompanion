﻿using LiveCharts;
using LiveCharts.Wpf;
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
            converter = new Converter();
            streamManager = new StreamManager();
            this.stepModel = stepModel;
            rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion"); 
            folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings");
            dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\data.json");
            streamTemplatePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\streams.json");;
            historyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Stream Companion\settings\history.json"); ;
            semaphoreSlim = new SemaphoreSlim(1);
            TryLoadSettings();
        }
        #endregion

        #region Public Methods
        public IStatistic LoadStatistic()
        {
            var ratings = GetAllRatings();
            ratings.Sort();
            return new Statistic(GetAllEpisodeCount(), GetEpisodeMeanScore(), GetScoreDistribution(ratings), ratings.Select(item => Convert.ToString(item)).ToArray(), GetSerieCount());
        }

        public string GenerateNextEpisodeStream(ISerie serie)
        {
            return streamManager.GenerateNextEpisodeStream(serie, cachedStreams);
        }

        public void SaveData()
        {
            semaphoreSlim.Wait();
            var root = converter.Convert(userId, stepModel.CurrentlyWatching.Series, stepModel.Completed.Series, stepModel.OnHold.Series, stepModel.Dropped.Series, stepModel.PlanToWatch.Series);
            converter.Export(root, dataPath);
            semaphoreSlim.Release();
        }
        
        public void SaveStreamTemplates(IModel model)
        {
            converter.ExportStreams(model, streamTemplatePath);
            cachedStreams = model.Streams;
        }

        public IEnumerable<IStreamItem> LoadStreamTemplates()
        {
            if (File.Exists(streamTemplatePath))
            {
                var streams = converter.ImportStreams(streamTemplatePath).Streams;
                cachedStreams = streams;
                return streams;
            }
            else
            {
                IModel tmpModel = GetDefaultStreams();
                SaveStreamTemplates(tmpModel);
                return tmpModel.Streams;
            }
        }

        public void SaveHistory(IHistoryItem history)
        {
            converter.SaveHistory(history, historyPath);
        }

        public List<IHistoryItem> LoadHistory()
        {
            return converter.LoadHistory(historyPath);
        }
        #endregion

        #region Data Loading
        private void TryLoadSettings()
        {
            if (Directory.Exists(folderPath))
            {
                LoadData();
                LoadStreamTemplates();
            }
            else
            {
                userId = Guid.NewGuid();
                Directory.CreateDirectory(folderPath);
                SaveData();
                SaveStreamTemplates(GetDefaultStreams());
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
            var root = converter.Import(dataPath);

            new Task(() => LoadData(root)).Start();
        }

        private void LoadData(IConverterRoot root)
        {
            foreach (var serie in root.CurrentlyWatching)
            {
                stepModel.CurrentlyWatching.Series.Add(serie);
            }

            foreach (var serie in root.Completed)
            {
                stepModel.Completed.Series.Add(serie);
            }

            foreach (var serie in root.OnHold)
            {
                stepModel.OnHold.Series.Add(serie);
            }

            foreach (var serie in root.Dropped)
            {
                stepModel.Dropped.Series.Add(serie);
            }

            foreach (var serie in root.PlanToWatch)
            {
                stepModel.PlanToWatch.Series.Add(serie);
            }

            userId = root.Id;
        }
        #endregion

        #region Statistic
        private SeriesCollection GetScoreDistribution(List<double> ratings)
        {
            ChartValues<int> chartValues = new ChartValues<int>();
            foreach (var item in ratings)
            {
                chartValues.Add(GetRatingCount(item));
            }

            var seriesCollection = new SeriesCollection();
            seriesCollection.Add(new RowSeries
            {
                Title = "Ratings",
                Values = chartValues,
                DataLabels = true,
                LabelPoint = x => x.X.ToString()
            });

            return seriesCollection;
        }

        private List<double> GetAllRatings()
        {
            List<double> scores = new List<double>();
            scores.AddRange(stepModel.Completed.Series.Select(x => x.Rating).ToList());
            scores.AddRange(stepModel.CurrentlyWatching.Series.Select(x => x.Rating).ToList());
            scores.AddRange(stepModel.Dropped.Series.Select(x => x.Rating).ToList());
            scores.AddRange(stepModel.OnHold.Series.Select(x => x.Rating).ToList());
            scores.AddRange(stepModel.PlanToWatch.Series.Select(x => x.Rating).ToList());
            return scores.Distinct().ToList();
        }

        private int GetRatingCount(double rating)
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
