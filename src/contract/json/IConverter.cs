using System;
using System.Collections.Generic;
using com.gmail.mikeundead.streamcompanion.contract.json.deserialize;
using com.gmail.mikeundead.streamcompanion.contract.json.serialize;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;

namespace com.gmail.mikeundead.streamcompanion.contract.json
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
