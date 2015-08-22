using System.Collections.Generic;

namespace StreamCompanion.Contract.StreamTemplate
{
    public interface IModel
    {
        List<IStreamItem> Streams { get; set; }
    }
}
