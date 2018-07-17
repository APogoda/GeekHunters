namespace GeekHunters.Controllers.Resources
{
    public class SkillResource
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is SkillResource item))
            {
                return false;
            }

            return this.Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}