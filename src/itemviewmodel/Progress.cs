using helper.mvvm.baseclasses;
using StreamCompanion.Contract;

namespace StreamCompanion.ItemViewModel
{
	public class Progress : ViewModelBase<IProgress>, IProgress
	{
		public Progress(int currentEpisode, int lastEpisode, int? season)
		{
			this.CurrentEpisode = currentEpisode;
			this.LastEpisode = lastEpisode;
			this.UpdateEpisodeDisplay();
			this.Season = season;
		}

		public int CurrentEpisode
		{
			get { return this.Get(x => x.CurrentEpisode); }
			set
			{
				this.Set(x => x.CurrentEpisode, value);
				this.UpdateEpisodeDisplay();
			}
		}

		public int LastEpisode
		{
			get { return this.Get(x => x.LastEpisode); }
			set
			{
				this.Set(x => x.LastEpisode, value);
				this.UpdateEpisodeDisplay();
			}
		}

		public int? Season
		{
			get { return this.Get(x => x.Season); }
			set { this.Set(x => x.Season, value); }
		}

		public string EpisodeDisplay
		{
			get { return this.Get(x => x.EpisodeDisplay); }
			set { this.Set(x => x.EpisodeDisplay, value); }
		}

		private void UpdateEpisodeDisplay()
		{
			this.EpisodeDisplay = string.Format("{0}/{1}", this.CurrentEpisode, this.LastEpisode);
		}
	}
}
