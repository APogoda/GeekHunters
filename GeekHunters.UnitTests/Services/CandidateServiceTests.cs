using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekHunters.Controllers.Resources;
using GeekHunters.Mapping;
using GeekHunters.Models;
using GeekHunters.Repositories;
using GeekHunters.Services;
using GeekHunters.Services.Impl;
using Moq;
using NUnit.Framework;

namespace GeekHunters.UnitTests.Services
{
    [TestFixture]
    public class CandidateServiceTests
    {
        private List<CandidateResource> _candidateResources;
        private List<Candidate> _candidates;
        private Mock<ICandidateRepository> _repository;
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;
        private ICandidateService _service;
        private FilterResource _filterResource;
        
        [SetUp]
        public void SetUp()
        {
            SetupCandidateResorces();
            SetupCandidates();
            MockRepository();
            MockUnitOfWork();
            SetupMapper();
            SetupService();
            _filterResource = new FilterResource(){SkillId = 1};
        }

        [Test]
        public async Task GetAllCandidatesAsync_ListOfCandidateResourcesReturned()
        {
            var returnedCandidates = await _service.GetAllCandidatesAsync(_filterResource);
            Assert.IsTrue(returnedCandidates.Items.SequenceEqual(_candidateResources));

        }

        [Test]
        public async Task GetCandidateAsync_ValidIdAsParam_CandidateResourceRedurned()
        {
            const int id = 1;
            var expectedCandidate = _candidateResources.SingleOrDefault(c=>c.Id==id);
            
            var returnedCandidate = await _service.GetCandidateAsync(id);
            
            AssertCandidates(expectedCandidate,returnedCandidate);
            
        }
        
        [Test]
        public async Task GetCandidateAsync_InvalidIdAsParam_NullReturned()
        {
            const int invalidId = 10;
            
            var returnedCandidate = await _service.GetCandidateAsync(invalidId);
            
            Assert.IsNull(returnedCandidate);
            
        }
        
        [Test]
        public async Task AddNewCandidateAsync_CreateCandidateResourceAsParam_CreatedCandidateResourceReturned()
        {
            var newCandidate = GetNewCandidate();
            var expectedCandidate = GetExpectedCandidate();

            var addedCandidate = await _service.AddCandidateAsync(newCandidate);
            
            AssertCandidates(expectedCandidate, addedCandidate);
        }
        
        [Test]
        public async Task UpdateCandidate_CreateCandidateResourceAsParam_UpdatedCandidateResourceReturned()
        {
            var n = 0;
            var candidate = GetCreateCandidateFromList(n);
            candidate.FirstName += "1";
            var expectedCandidate = _candidateResources[n];
            expectedCandidate.FirstName += "1";
            var updateCandidate =await _service.UpdateCandidateAsync(candidate.Id??default(int),candidate);
            
            AssertCandidates(expectedCandidate,updateCandidate);
        }
        
        [Test]
        public async Task UpdateCandidate_CreateCandidateResourceAsParamWithLessSkills_UpdatedCandidateResourceWithLessSkillsReturned()
        {
            var index = 0;
            var candidate = GetCreateCandidateFromList(index);
            candidate.Skills.Remove(candidate.Skills.ToList()[0]);
            var expectedCandidate = _candidateResources[index];
            expectedCandidate.Skills.Remove(expectedCandidate.Skills.ToList()[0]);

            var updateCandidate =await _service.UpdateCandidateAsync(candidate.Id??default(int),candidate);
            
            Assert.AreEqual(expectedCandidate.Skills.Count,updateCandidate.Skills.Count);
        }
        
        [Test]
        public async Task UpdateCandidate_CreateCandidateResourceAsParamWithMoreSkills_UpdatedCandidateResourceWithMoreSkillsReturned()
        {
            var index = 0;
            var candidate = GetCreateCandidateFromList(index);
            var newSkillId = 3;
            candidate.Skills.Add(newSkillId);
            var expectedCandidate = _candidateResources[index];
            expectedCandidate.Skills.Add(new SkillResource(){Id=newSkillId});

            var updateCandidate =await _service.UpdateCandidateAsync(candidate.Id??default(int),candidate);
            
            Assert.AreEqual(expectedCandidate.Skills.Count,updateCandidate.Skills.Count);
        }
        
        [Test]
        public async Task UpdateCandidate_InvalidIdAsParam_NullReturned()
        {
            var n = 0;
            var candidate = GetCreateCandidateFromList(n);
            var invalidId = 10;
            
            var updateCandidate =await _service.UpdateCandidateAsync(invalidId,candidate);
            
            Assert.IsNull(updateCandidate);
        }
        
        [Test]
        public async Task DeleteCandidate_ValidIdAsParam_DeletedIdReturned()
        {
            var id = 1;

            var deletedId = await _service.DeleteCandidateAsync(id);
            
            Assert.AreEqual(id,deletedId);
        }
        
        [Test]
        public async Task DeleteCandidate_InvalidIdAsParam_NullReturned()
        {
            var id = 10;

            var deletedId = await _service.DeleteCandidateAsync(id);
            
            Assert.IsNull(deletedId);
        }

        private CreateCandidateResource GetCreateCandidateFromList(int index)
        {
            return new CreateCandidateResource()
            {
                Id = _candidateResources[index].Id,
                FirstName = _candidateResources[index].FirstName,
                LastName = _candidateResources[index].LastName,
                Skills = _candidateResources[index].Skills.Select(s=>s.Id??default(int)).ToList()
            };
        }

        private static void AssertCandidates(CandidateResource expectedCandidate, CandidateResource actualCandidate)
        {
            Assert.AreEqual(expectedCandidate.FirstName, actualCandidate.FirstName);
            Assert.AreEqual(expectedCandidate.LastName, actualCandidate.LastName);
            Assert.IsTrue(expectedCandidate.Skills.SequenceEqual(actualCandidate.Skills));
        }

        private static CandidateResource GetExpectedCandidate()
        {
            return new CandidateResource()
            {
                FirstName = "FirstName_5",
                LastName = "LastName_5",
                Skills = new List<SkillResource>()
                {
                    new SkillResource()
                    {
                        Id = 1, Name = "Skill_1"
                    },
                    new SkillResource()
                    {
                        Id = 2, Name = "Skill_2"
                    }
                }
            };
        }

        private static CreateCandidateResource GetNewCandidate()
        {
            return new CreateCandidateResource()
            {
                FirstName = "FirstName_5", LastName = "LastName_5",
                Skills = new List<int>(){1,2}
            };
        }
        
        private void SetupService()
        {
            _service = new CandidateService(_repository.Object, _mapper, _unitOfWork.Object);
        }

        private void SetupMapper()
        {
            var mappingProfile = new MappingProfile();
            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);
        }

        private void MockUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.CommitAsync()).Returns(async () => { await Task.Yield(); });
        }

        private void MockRepository()
        {
            _repository = new Mock<ICandidateRepository>();
            MockGetAllCandidatesAsync();
            MockGetCandidateAsync();
            MockAddCandidateAsync();
            MockDeleteCandidateAsync();

        }

        private void MockGetAllCandidatesAsync()
        {
            _repository.Setup(r => r.GetAllCandidatesAsync(It.IsAny<Filter>())).Returns(async () =>
            {
                await Task.Yield();
                var candidates = _candidates;
                if (_filterResource.SkillId.HasValue)
                    candidates=candidates.Where(c => c.CandidateSkills.Any(cs=>cs.SkillId.Equals(_filterResource.SkillId))).ToList();
                var result = new QueryResult<Candidate>(){Items=candidates};
                return result;
            });
        }

        private void MockGetCandidateAsync()
        {
            _repository.Setup(r => r.GetCandidateAsync(It.IsAny<int>())).Returns<int>(async (n) =>
            {
                await Task.Yield();
                var candidate = _candidates.SingleOrDefault(c=>c.Id == n);
                if (candidate == null || candidate.CandidateSkills.Count==0) return candidate;
                foreach (var cs in candidate.CandidateSkills)
                {
                    if (cs.Skill==null)
                        cs.Skill = new Skill(){Id=cs.SkillId};
                }
                return candidate;
            });
        }
        
        private void MockAddCandidateAsync()
        {
            _repository.Setup(r => r.AddCandidateAsync(It.IsAny<Candidate>())).Returns<Candidate>(async (c) =>
            {
                await Task.Yield();
                c.Id = new Random().Next();
                return new Candidate()
                {
                    Id = c.Id, 
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CandidateSkills = FillCandidateSkills()
                    
                };
            });
        }
        
        private void MockDeleteCandidateAsync()
        {
            _repository.Setup(r => r.DeleteCandidate(It.IsAny<Candidate>())).Verifiable();
        }

        private static List<CandidateSkill> FillCandidateSkills()
        {
            return new List<CandidateSkill>()
            {
                new CandidateSkill()
                {
                    Skill = new Skill()
                    {
                        Id=1, Name = "Skill_1"
                    },
                            
                },
                new CandidateSkill()
                {
                    Skill = new Skill()
                    {
                        Id=2, Name = "Skill_2"
                    },
                            
                }
                        
            };
        }

        private void SetupCandidates()
        {
            _candidates = new List<Candidate>()
            {
                new Candidate()
                {
                    Id = 1,
                    FirstName = "FirstName_1",
                    LastName = "LastName_1",
                    CandidateSkills =
                        new List<CandidateSkill>()
                        {
                            new CandidateSkill()
                            {
                                Id = 1, 
                                Skill = new Skill()
                                {
                                    Id = 1, 
                                    Name = "Skill_1"
                                },
                                SkillId = 1
                            },
                            new CandidateSkill()
                            {
                                Id = 2, 
                                Skill = new Skill()
                                {
                                    Id = 2, 
                                    Name = "Skill_2"
                                },
                                SkillId = 2
                            }
                        }
                },
            };
        }

        private void SetupCandidateResorces()
        {
            _candidateResources = new List<CandidateResource>()
            {
                new CandidateResource()
                {
                    Id = 1,
                    FirstName = "FirstName_1",
                    LastName = "LastName_1",
                    Skills = new List<SkillResource>()
                    {
                        new SkillResource() {Id = 1, Name = "Skill_1"},
                        new SkillResource() {Id = 2, Name = "Skill_2"}
                    }
                }
            };
        }

    }
}