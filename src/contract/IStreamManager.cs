using System.Collections.Generic;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public interface IStreamManager
    {
        string GenerateNextEpisodeStream(ISerie serie, List<IStreamItem> cachedStreams);
    }
}
