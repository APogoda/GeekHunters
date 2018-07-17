using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GeekHunters.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        public ICollection<CandidateSkill> CandidateSkills { get; set; }

        public Candidate()
        {
            CandidateSkills = new Collection<CandidateSkill>();
        }
    }
}