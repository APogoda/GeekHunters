using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GeekHunters.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<CandidateSkill> CandidateSkills { get; set; }

        public Skill()
        {
            CandidateSkills = new Collection<CandidateSkill>();
        }
    }
}