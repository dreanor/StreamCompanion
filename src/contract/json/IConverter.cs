using StreamCompanion.Contract.StreamTemplate;
using System;
using System.Collections.Generic;

namespace StreamCompanion.Contract.Json
{
    public interface IConverter
    {
        IConverterRoot Convert(
            Guid id,
            IEnumerable<ISerie> currentlyWatching,
            IEnumerable<ISerie> completed,
            IEnumerable<ISerie> onHold,
            IEnumerable<ISerie> dropped,
            IEnumerable<ISerie> planToWatch);

        void Export(IConverterRoot serializedRoot, string inputFileFullname);

        IConverterRoot Import(string inputFileFullname);

        void ExportStreams(IModel model, string inputFileFullname);
        IModel ImportStreams(string inputFileFullname);

        void SaveHistory(IHistoryItem history, string inputFileFullname);
        List<IHistoryItem> LoadHistory(string inputFileFullname);
    }
}
