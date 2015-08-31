using StreamCompanion.Contract.StreamTemplate;
using System.Collections.Generic;

namespace StreamCompanion.StreamTemplate
{
    public class Model : IModel
    {
        public Model(List<StreamItem> streams)
        {
            this.Streams = new List<IStreamItem>(streams);
        }

        public List<IStreamItem> Streams { get; set; }
    }
}
