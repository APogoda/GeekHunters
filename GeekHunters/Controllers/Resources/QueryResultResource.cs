using System.Collections.Generic;

namespace GeekHunters.Controllers.Api
{
    public class QueryResultResource<T>
    {
        public int totalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}