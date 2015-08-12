using System.Collections.Generic;

namespace com.gmail.mikeundead.streamcompanion.contract.streamtemplate
{
    public interface IModel
    {
        List<IStreamItem> Streams { get; set; }
    }
}
