using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;

namespace GeekHunters.Repositories
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<Skill> GetSkillAsync(int id);
        Task<Skill> AddSkillAsync(Skill skill);
        void DeleteSkill(Skill skill);
    }
}