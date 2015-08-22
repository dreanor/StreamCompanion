using StreamCompanion.Contract.Json.Deserialize;
using StreamCompanion.Contract.Json.Serialize;
using StreamCompanion.Contract.StreamTemplate;
using System;
using System.Collections.Generic;

namespace StreamCompanion.Contract.Json
{
    public interface IConverter
    {
        ISerializedRoot Convert(
            Guid id,
            IEnumerable<ISerie> currentlyWatching,
            IEnumerable<ISerie> completed,
            IEnumerable<ISerie> onHold,
            IEnumerable<ISerie> dropped,
            IEnumerable<ISerie> planToWatch);

        void Export(ISerializedRoot serializedRoot, string inputFileFullname);

        IDeserializedRoot Import(string inputFileFullname);

        void ExportStreams(IModel model, string inputFileFullname);
        IModel ImportStreams(string inputFileFullname);
    }
}
