namespace GeekHunters.Controllers.Resources
{
    public class FilterResource
    {
        public int? SkillId { get; set; }
        
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}