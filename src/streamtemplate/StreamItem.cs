using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;
using helper.mvvm.baseclasses;
using com.gmail.mikeundead.streamcompanion.contract;
using Newtonsoft.Json;

namespace com.gmail.mikeundead.streamcompanion.streamtemplate
{
    public class StreamItem : ViewModelBase<IStreamItem>, IStreamItem
    {
        public StreamItem(string website, string whitespaceReplacement)
        {
            this.Website = website;
            this.WhitespaceReplacement = whitespaceReplacement;
            this.UsedOnTypes = SerieType.Mixed.ToString();
            this.StreamLanguage = Language.English.ToString();
        }

        [JsonConstructor]
        public StreamItem(string website, string whitespaceReplacement, string usedOnTypes, string streamLanguage)
        {
            this.Website = website;
            this.WhitespaceReplacement = whitespaceReplacement;
            this.UsedOnTypes = usedOnTypes;
            this.StreamLanguage = streamLanguage;
        }

        public string Website
        {
            get { return this.Get(x => x.Website); }
            set { this.Set(x => x.Website, value); }
        }

        public string WhitespaceReplacement
        {
            get { return this.Get(x => x.WhitespaceReplacement); }
            set { this.Set(x => x.WhitespaceReplacement, value); }
        }

        public string UsedOnTypes
        {
            get { return this.Get(x => x.UsedOnTypes); }
            set { this.Set(x => x.UsedOnTypes, value); }
        }

        public string StreamLanguage
        {
            get { return this.Get(x => x.StreamLanguage); }
            set { this.Set(x => x.StreamLanguage, value); }
        }
    }
}
