﻿using StreamCompanion.Contract.StreamTemplate;
using System.Collections.Generic;

namespace StreamCompanion.Contract
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