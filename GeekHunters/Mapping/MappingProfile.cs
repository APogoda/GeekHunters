using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using GeekHunters.Controllers.Api;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;

namespace GeekHunters.Mapping
{
    public class MappingProfile : MapperConfigurationExpression 
    {
        public MappingProfile()
        {
            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));
            CreateMap<Candidate, CreateCandidateResource>()
                .ForMember(cr => cr.Skills, opt => opt.MapFrom(c=> c.CandidateSkills.Select(cs=> cs.SkillId)));
            CreateMap<Candidate, CandidateResource>()
                .ForMember(cr => cr.Skills, opt => opt.MapFrom(c => c.CandidateSkills.Select(cs=> new SkillResource{Id=cs.Skill.Id,Name = cs.Skill.Name})));
            CreateMap<Skill, SkillResource>();
            

            CreateMap<CreateCandidateResource, Candidate>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CandidateSkills,opt => opt.Ignore())
                .AfterMap((cr, c) =>
                    {
                        var removedSkills = c.CandidateSkills.Where(s => !cr.Skills.Contains(s.SkillId));
                        foreach (var s in removedSkills.ToList())
                            c.CandidateSkills.Remove(s);

                        var addedSkills = cr.Skills.Where(id => c.CandidateSkills.All(s => s.SkillId != id)).Select(id=> new CandidateSkill{SkillId = id});
                        foreach (var s in addedSkills)
                            c.CandidateSkills.Add(s);
                    }
                    
                    );
            CreateMap<SkillResource, Skill>().ForMember(s => s.Id, opt => opt.Ignore());
            CreateMap<FilterResource, Filter>();
        }
    }
}