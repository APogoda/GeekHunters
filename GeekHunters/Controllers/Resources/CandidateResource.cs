using System.Collections.Generic;
using System.Collections.ObjectModel;
using GeekHunters.Models;

namespace GeekHunters.Controllers.Resources
{
    public class CandidateResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<SkillResource> Skills { get; set; }

        public CandidateResource()
        {
            Skills = new Collection<SkillResource>();
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is CandidateResource item))
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