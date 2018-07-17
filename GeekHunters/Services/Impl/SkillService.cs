using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;
using GeekHunters.Repositories;
using GeekHunters.Services.Implementation;

namespace GeekHunters.Services.Impl
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SkillService(ISkillRepository repository,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SkillResource>> GetAllSkillsAsync()
        {
            var skills = await _repository.GetAllSkillsAsync();
            return (_mapper.Map<IEnumerable<Skill>, IEnumerable<SkillResource>>(skills));
            
        }

        public async Task<SkillResource> GetSkillsAsync(int id)
        {
            var skill = await _repository.GetSkillAsync(id);
            return (_mapper.Map<Skill, SkillResource>(skill));
        }

        public async Task<SkillResource> AddSkillAsync(SkillResource skillResource)
        {
            var skill = _mapper.Map<SkillResource, Skill>(skillResource);
            var createdSkill = await _repository.AddSkillAsync(skill);
            await _unitOfWork.CommitAsync();
            return (_mapper.Map<Skill, SkillResource>(createdSkill));
        }

        public async Task<SkillResource> UpdateSkillAsync(int id, SkillResource skillResource)
        {
            var skill = await _repository.GetSkillAsync(id);
            if (skill == null)
                return null;
            _mapper.Map<SkillResource, Skill>(skillResource, skill);
            await _unitOfWork.CommitAsync();
            return (_mapper.Map<Skill, SkillResource>(skill));
        }        

        public async Task<int?> DeleteSkillAsync(int id)
        {
            var skill = await _repository.GetSkillAsync(id);
            if (skill == null)
                return null;
            _repository.DeleteSkill(skill);
            await _unitOfWork.CommitAsync();
            return id;
        }
    }
}