using System.Collections.Generic;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public interface IController
    {
        void SaveData();

        string GenerateNextEpisodeStream(ISerie serie);

        void SaveStreamTemplates(IModel model);

        IEnumerable<IStreamItem> LoadStreamTemplates();

        string ChangelogPath { get; }
    }
}
