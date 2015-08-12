using System.Collections.Generic;
using com.gmail.mikeundead.streamcompanion.contract.streamtemplate;

namespace com.gmail.mikeundead.streamcompanion.streamtemplate
{
    public class Model : IModel
    {
        public Model(IEnumerable<StreamItem> streams)
        {
            this.Streams = new List<IStreamItem>(streams);
        }

        public List<IStreamItem> Streams { get; set; }
    }
}
