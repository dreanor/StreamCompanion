﻿using StreamCompanion.Contract;
using StreamCompanion.Contract.Json;
using StreamCompanion.ItemViewModel;
using System;
using System.Collections.Generic;

namespace StreamCompanion.JsonConverter
{
    public class DeserializedRoot : IConverterRoot
    {
        public DeserializedRoot(
            Guid id,
            string streamLanguage,
            IEnumerable<Serie> currentlyWatching,
            IEnumerable<Serie> completed,
            IEnumerable<Serie> onHold,
            IEnumerable<Serie> dropped,
            IEnumerable<Serie> planToWatch)
        {
            this.CurrentlyWatching = new List<ISerie>(currentlyWatching);
            this.Completed = new List<ISerie>(completed);
            this.OnHold = new List<ISerie>(onHold);
            this.Dropped = new List<ISerie>(dropped);
            this.PlanToWatch = new List<ISerie>(planToWatch);
            this.Id = id;
            this.StreamLanguage = streamLanguage;
        }

        public string StreamLanguage { get; private set; }

        public Guid Id { get; private set; }

        public List<ISerie> CurrentlyWatching { get; private set; }

        public List<ISerie> Completed { get; private set; }

        public List<ISerie> OnHold { get; private set; }

        public List<ISerie> Dropped { get; private set; }

        public List<ISerie> PlanToWatch { get; private set; }
    }
}
