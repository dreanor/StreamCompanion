using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.json;
using com.gmail.mikeundead.streamcompanion.contract.json.deserialize;
using com.gmail.mikeundead.streamcompanion.contract.json.serialize;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;
using com.gmail.mikeundead.streamcompanion.streamtemplate;
using helper.filehelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace com.gmail.mikeundead.streamcompanion.jsonconverter
{
    public class Converter : IConverter
    {
        public ISerializedRoot Convert(
            Guid id,
            IEnumerable<ISerie> currentlyWatching,
            IEnumerable<ISerie> completed,
            IEnumerable<ISerie> onHold,
            IEnumerable<ISerie> dropped,
            IEnumerable<ISerie> planToWatch)
        {
            return new SerializedRoot(id, currentlyWatching, completed, onHold, dropped, planToWatch);
        }

        public void Export(ISerializedRoot serializedRoot, string inputFileFullname)
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

        public IDeserializedRoot Import(string inputFileFullname)
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
