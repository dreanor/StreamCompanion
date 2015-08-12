using System.Collections.Generic;
using System.Net;
using com.gmail.mikeundead.streamcompanion.contract;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;
using System;

namespace com.gmail.mikeundead.streamcompanion.controller
{
    public class StreamManager : IStreamManager
    {
        public string GenerateNextEpisodeStream(ISerie serie, List<IStreamItem> cachedStreams)
        {
            foreach (var cachedStream in cachedStreams)
            {
                var url = this.DynamicStreamCheck(cachedStream, serie);

                if (!string.IsNullOrEmpty(url))
                {
                    return url;
                }
            }

            if (serie.Type == "Movie")
            {
               return string.Format("https://www.google.com/#q={0}+stream&safe=off", serie.Title);
            }

            if (serie.Progress.Season == null)
            {
                return string.Format("https://www.google.com/#q={0}+episode+{1}+stream&safe=off", serie.Title, this.GetNextEpisode(serie.Progress));
            }
            else
            {
                return string.Format("https://www.google.com/#q={0}+season+{1}+episode+{2}+stream&safe=off", serie.Title, serie.Progress.Season, this.GetNextEpisode(serie.Progress));
            }
        }

        private string DynamicStreamCheck(IStreamItem cachedStream, ISerie serie)
        {
            var title = serie.Title;
            var progress = serie.Progress;
            var correctedTitle = title.Replace(" ", cachedStream.WhitespaceReplacement);

            var hasTitle = cachedStream.Website.Contains("{0}");
            var hasSeason = cachedStream.Website.Contains("{1}");
            var hasEpisode = cachedStream.Website.Contains("{2}");

            if (!hasTitle && !hasSeason && !hasEpisode)
            {
                return string.Empty;
            }
            if (hasTitle && !hasSeason && !hasEpisode)
            {
                return string.Format(cachedStream.Website, correctedTitle);
            }
            if (hasTitle && hasSeason && !hasEpisode)
            {
                return string.Format(cachedStream.Website, correctedTitle, progress.Season);
            }
            if (hasTitle && hasEpisode && !hasSeason)
            {
                var stream = cachedStream.Website.Replace("{2}", "{1}");
                if (this.IsValidUrl(string.Format(stream, correctedTitle, progress.CurrentEpisode)))
                {
                    return string.Format(stream, correctedTitle, this.GetNextEpisode(progress));
                }
            }

            if (this.IsValidUrl(string.Format(cachedStream.Website, correctedTitle, progress.Season, progress.CurrentEpisode)))
            {
                return string.Format(cachedStream.Website, correctedTitle, progress.Season, this.GetNextEpisode(progress));
            }

            return string.Empty;
        }

        private int GetNextEpisode(IProgress progress)
        {
            int nextEpisode;
            if (progress.CurrentEpisode == progress.LastEpisode)
            {
                nextEpisode = progress.CurrentEpisode;
            }
            else
            {
                nextEpisode = progress.CurrentEpisode + 1;
            }

            return nextEpisode;
        }

        private bool IsValidUrl(string url)
        {
            try
            {
                using (var webClient = new WebClientExtension())
                {
                    webClient.HeadOnly = true;
                    webClient.DownloadString(url);
                }
            }
            catch (WebException)
            {
                return false;
            }

            return true;
        }
    }
}
