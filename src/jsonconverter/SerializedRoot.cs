using StreamCompanion.Contract;
using StreamCompanion.Contract.Json.Serialize;
using System;
using System.Collections.Generic;

namespace StreamCompanion.JsonConverter
{
    public class SerializedRoot : ISerializedRoot
    {
        public SerializedRoot(Guid id, IEnumerable<ISerie> currentlyWatching, IEnumerable<ISerie> completed, IEnumerable<ISerie> onHold, IEnumerable<ISerie> dropped, IEnumerable<ISerie> planToWatch)
        {
            this.CurrentlyWatching = new List<ISerie>(currentlyWatching);
            this.Completed = new List<ISerie>(completed);
            this.OnHold = new List<ISerie>(onHold);
            this.Dropped = new List<ISerie>(dropped);
            this.PlanToWatch = new List<ISerie>(planToWatch);
            this.Id = id;
        }

        public Guid Id { get; private set; }

        public List<ISerie> CurrentlyWatching { get; private set; }

        public List<ISerie> Completed { get; private set; }

        public List<ISerie> OnHold { get; private set; }

        public List<ISerie> Dropped { get; private set; }

        public List<ISerie> PlanToWatch { get; private set; }
    }
}
