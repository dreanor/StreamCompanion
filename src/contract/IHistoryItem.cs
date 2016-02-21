﻿using System;

namespace StreamCompanion.Contract
{
    public interface IHistoryItem
    {
        string Title { get; }

        string Details { get; }

        string LastModified { get; }
    }
}
