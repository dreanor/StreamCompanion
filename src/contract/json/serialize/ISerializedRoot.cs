using System;
using System.Collections.Generic;

namespace com.gmail.mikeundead.streamcompanion.contract.json.serialize
{
    public interface ISerializedRoot
    {
        Guid Id { get; }

        List<ISerie> CurrentlyWatching { get; }

        List<ISerie> Completed { get; }

        List<ISerie> OnHold { get; }

        List<ISerie> Dropped { get; }

        List<ISerie> PlanToWatch { get; }
    }
}
