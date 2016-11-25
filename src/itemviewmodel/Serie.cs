using helper.mvvm.baseclasses;
using StreamCompanion.Contract;
using System;
using System.Collections.ObjectModel;

namespace StreamCompanion.ItemViewModel
{
    public class Serie : ViewModelBase<ISerie>, ISerie
    {
        public Serie(int number, string title, Progress progress, double rating, string type, string comment, string stream)
        {
            this.Number = number;
            this.Title = title;
            this.Progress = progress;
            this.Rating = rating;
            this.Type = type;
            this.Comment = comment;
            this.Stream = stream;

            this.Types = new ObservableCollection<string>(Enum.GetNames(typeof(SerieType)));
        }

        public int Number
        {
            get { return this.Get(x => x.Number); }
            set { this.Set(x => x.Number, value); }
        }

        public string Title
        {
            get { return this.Get(x => x.Title); }
            set { this.Set(x => x.Title, value); }
        }

        public IProgress Progress
        {
            get { return this.Get(x => x.Progress); }
            set { this.Set(x => x.Progress, value); }
        }

        public double Rating
        {
            get { return this.Get(x => x.Rating); }
            set { this.Set(x => x.Rating, value); }
        }

        public ObservableCollection<string> Types
        {
            get { return this.Get(x => x.Types); }
            private set { this.Set(x => x.Types, value); }
        }

        public string Type
        {
            get { return this.Get(x => x.Type); }
            set { this.Set(x => x.Type, value); }
        }

        public string Comment
        {
            get { return this.Get(x => x.Comment); }
            set { this.Set(x => x.Comment, value); }
        }

        public string Stream
        {
            get { return this.Get(x => x.Stream); }
            set { this.Set(x => x.Stream, value); }
        }
    }
}
