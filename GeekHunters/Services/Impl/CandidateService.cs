using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeekHunters.Controllers.Api;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;
using GeekHunters.Repositories;

namespace GeekHunters.Services.Impl
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateService(ICandidateRepository repository,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<QueryResultResource<CandidateResource>> GetAllCandidatesAsync(FilterResource filterResource)
        {
            var candidates = await _repository.GetAllCandidatesAsync(_mapper.Map<FilterResource,Filter>(filterResource));
            return (_mapper.Map<QueryResult<Candidate>, QueryResultResource<CandidateResource>>(candidates));
        }

        public async Task<CandidateResource> GetCandidateAsync(int id)
        {
            var candidate = await _repository.GetCandidateAsync(id);
            return (_mapper.Map<Candidate, CandidateResource>(candidate));
        }

        public async Task<CandidateResource> AddCandidateAsync(CreateCandidateResource createCandidateResource)
        {
           var candidate = _mapper.Map<CreateCandidateResource, Candidate>(createCandidateResource);
           var createdCandidate = await _repository.AddCandidateAsync(candidate);
           await _unitOfWork.CommitAsync();
           await _repository.GetCandidateAsync(candidate.Id);
           return _mapper.Map<Candidate,CandidateResource>(createdCandidate);
        }

        public async Task<CandidateResource> UpdateCandidateAsync(int id, CreateCandidateResource createCandidateResource )
        {
            var candidate = await _repository.GetCandidateAsync(id);
            
            if (candidate == null)
                return null;
            
            _mapper.Map<CreateCandidateResource, Candidate>(createCandidateResource,candidate);
            await _unitOfWork.CommitAsync();
            await _repository.GetCandidateAsync(id);
            return _mapper.Map<Candidate,CandidateResource>(candidate);
            
        }

        public async Task<int?> DeleteCandidateAsync(int id)
        {
            var candidate = await _repository.GetCandidateAsync(id);
            
            if (candidate == null)
                return null;
            
            _repository.DeleteCandidate(candidate);
            await _unitOfWork.CommitAsync();
            return id;
        }
    }
}