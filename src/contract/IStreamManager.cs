using StreamCompanion.Contract.StreamTemplate;
using System.Collections.Generic;

namespace StreamCompanion.Contract
{
    public interface IStreamManager
    {
        string GenerateNextEpisodeStream(ISerie serie, List<IStreamItem> cachedStreams);
    }
}
