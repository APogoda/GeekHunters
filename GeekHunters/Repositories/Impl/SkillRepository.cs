using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;
using GeekHunters.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GeekHunters.Repositories.Impl
{
    public class SkillRepository : ISkillRepository
    {
        private readonly GeekHuntersDbContext _dbContext;

        public SkillRepository(GeekHuntersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await _dbContext.Skills.ToListAsync();
        }

        public async Task<Skill> GetSkillAsync(int id)
        {
            return await _dbContext.Skills.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var newSkill = await _dbContext.Skills.AddAsync(skill);
            return newSkill.Entity;
        }

        public void DeleteSkill(Skill skill)
        {
            _dbContext.Skills.Remove(skill);
            
        }
    }
}