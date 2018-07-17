using System.Collections.Generic;
using System.Threading.Tasks;
using GeekHunters.Controllers.Api;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;

namespace GeekHunters.Services
{
    public interface ICandidateService
    {
        Task<QueryResultResource<CandidateResource>> GetAllCandidatesAsync(FilterResource filterResource);
        Task<CandidateResource> GetCandidateAsync(int id);
        Task<CandidateResource> AddCandidateAsync(CreateCandidateResource createCandidateResource);
        Task<CandidateResource> UpdateCandidateAsync(int id, CreateCandidateResource createCandidateResource);
        Task<int?> DeleteCandidateAsync(int id);
    }
}