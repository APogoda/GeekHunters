using GeekHunters.Extensions;

namespace GeekHunters.Models
{
    public class Filter : IQueryObject
    {
        public int? SkillId { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}