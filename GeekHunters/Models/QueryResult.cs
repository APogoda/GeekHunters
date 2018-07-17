using System.Collections.Generic;

namespace GeekHunters.Models
{
    public class QueryResult<T>
    {
        public int totalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
