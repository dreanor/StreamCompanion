﻿using System;
using System.Collections.Generic;

namespace StreamCompanion.Contract.Json.Deserialize
{
    public interface IDeserializedRoot
    {
        Guid Id { get; }

        List<ISerie> CurrentlyWatching { get; }

        List<ISerie> Completed { get; }

        List<ISerie> OnHold { get; }

        List<ISerie> Dropped { get; }

        List<ISerie> PlanToWatch { get; }
    }
}