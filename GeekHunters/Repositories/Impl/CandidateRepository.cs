using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekHunters.Extensions;
using GeekHunters.Models;
using GeekHunters.Persistence;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GeekHunters.Repositories.Impl
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly GeekHuntersDbContext _dbContext;

        public CandidateRepository(GeekHuntersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<QueryResult<Candidate>> GetAllCandidatesAsync(Filter filter)
        {
            var result = new QueryResult<Candidate>();
            var query = _dbContext.Candidates
                .Include(c=>c.CandidateSkills)
                .ThenInclude(cs => cs.Skill).AsQueryable();
            
           if (filter.SkillId.HasValue)
               query = query.Where(c => c.CandidateSkills.Any(cs=>cs.SkillId.Equals(filter.SkillId)));

            result.totalItems = await query.CountAsync();
            query = query.ApplyPaging(filter);
            result.Items = await query.ToListAsync();
            return result;
        }

        public async Task<Candidate> GetCandidateAsync(int id)
        {
            return await _dbContext.Candidates
                .Include(c=>c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            var newCandidate = await _dbContext.Candidates.AddAsync(candidate);
            return newCandidate.Entity;
        }


        public void DeleteCandidate(Candidate candidate)
        {
            _dbContext.Candidates.Remove(candidate);
        }
    }
}