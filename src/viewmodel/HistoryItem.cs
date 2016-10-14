using StreamCompanion.Contract;
using System;

namespace StreamCompanion.ShellViewModel
{
    public class HistoryItem : IHistoryItem
    {
        public HistoryItem(string details, DateTime lastModified, string title)
        {
            Details = details;
            LastModified = lastModified;
            Title = title;
        }

        public string Details { get; private set; }

        public DateTime LastModified { get; private set; }

        public string Title { get; private set; }
    }
}
