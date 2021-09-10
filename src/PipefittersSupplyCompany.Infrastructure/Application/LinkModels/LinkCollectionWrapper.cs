using System.Collections.Generic;

namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels
{
    public class LinkCollectionWrapper<T> : LinkResourceBase
    {
        public LinkCollectionWrapper() { }

        public LinkCollectionWrapper(List<T> value) => Value = value;

        public List<T> Value { get; set; } = new List<T>();
    }
}