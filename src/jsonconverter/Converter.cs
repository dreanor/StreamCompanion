﻿using StreamCompanion.Contract;
using StreamCompanion.Contract.Json;
using StreamCompanion.Contract.StreamTemplate;
using StreamCompanion.StreamTemplate;
using helper.filehelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using StreamCompanion.ShellViewModel;

namespace StreamCompanion.JsonConverter
{
    public class Converter : IConverter
    {
        public IConverterRoot Convert(
            Guid id,
            IEnumerable<ISerie> currentlyWatching,
            IEnumerable<ISerie> completed,
            IEnumerable<ISerie> onHold,
            IEnumerable<ISerie> dropped,
            IEnumerable<ISerie> planToWatch)
        {
            return new SerializedRoot(id, currentlyWatching, completed, onHold, dropped, planToWatch);
        }

        public List<IHistoryItem> LoadHistory(string inputFileFullname)
        {
            if (!File.Exists(inputFileFullname))
            {
                return new List<IHistoryItem>();
            }

            var readLines = new FileHelper().ReadAllLines(inputFileFullname);
            string value = readLines.Aggregate(string.Empty, (current, line) => current + line);

            return new List<IHistoryItem>(JsonConvert.DeserializeObject<List<HistoryItem>>(value));
        }

        public void SaveHistory(IHistoryItem history, string inputFileFullname)
        {
            var previousHistory = LoadHistory(inputFileFullname);
            previousHistory.Add(history);

            using (var fileStream = File.Open(inputFileFullname, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("Windows-1252")))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;

                var jsonSerializer = new JsonSerializer();
                jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
                jsonSerializer.Serialize(jsonTextWriter, previousHistory);
            }
        }

        public void Export(IConverterRoot serializedRoot, string inputFileFullname)
        {
            using (var fileStream = File.Open(inputFileFullname, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("Windows-1252")))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;

                var jsonSerializer = new JsonSerializer();
                jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
                jsonSerializer.Serialize(jsonTextWriter, serializedRoot);
            }
        }

        public IConverterRoot Import(string inputFileFullname)
        {
            var readLines = new FileHelper().ReadAllLines(inputFileFullname);
            string value = readLines.Aggregate(string.Empty, (current, line) => current + line);
            
            return JsonConvert.DeserializeObject<DeserializedRoot>(value);
        }

        public void ExportStreams(IModel model, string inputFileFullname)
        {
            using (var fileStream = File.Open(inputFileFullname, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream, Encoding.GetEncoding("Windows-1252")))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
            {
                jsonTextWriter.Formatting = Formatting.Indented;

                var jsonSerializer = new JsonSerializer();
                jsonSerializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
                jsonSerializer.Serialize(jsonTextWriter, model);
            }
        }

        public IModel ImportStreams(string inputFileFullname)
        {
            var readLines = new FileHelper().ReadAllLines(inputFileFullname);
            string value = readLines.Aggregate(string.Empty, (current, line) => current + line);

            return JsonConvert.DeserializeObject<Model>(value);
        }
    }
}
