using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Models;

namespace GeekHunters.Repositories
{
    public interface ICandidateRepository
    {
        Task<QueryResult<Candidate>> GetAllCandidatesAsync(Filter filter);
        Task<Candidate> GetCandidateAsync(int id);
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        void DeleteCandidate(Candidate candidate);
    }
}