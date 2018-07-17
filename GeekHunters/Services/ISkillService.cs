using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Controllers.Resources;

namespace GeekHunters.Services.Implementation
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillResource>> GetAllSkillsAsync();
        Task<SkillResource> GetSkillsAsync(int id);
        Task<SkillResource> AddSkillAsync(SkillResource skillResource);
        Task<SkillResource> UpdateSkillAsync(int id, SkillResource skillResource);
        Task<int?> DeleteSkillAsync(int id);
    }
}