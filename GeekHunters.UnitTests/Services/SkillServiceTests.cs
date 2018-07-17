using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekHunters.Controllers.Resources;
using GeekHunters.Mapping;
using GeekHunters.Models;
using GeekHunters.Repositories;
using GeekHunters.Services.Impl;
using GeekHunters.Services.Implementation;
using Moq;
using NUnit.Framework;

namespace GeekHunters.UnitTests.Services
{
    [TestFixture]
    public class SkillServiceTests
    {
        private List<Skill> _skills;
        private List<SkillResource> _skillResources;
        private Mock<ISkillRepository> _repository;
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;
        private ISkillService _service;
        
        [SetUp]
        public void SetUp()
        {
            SetupSkills();
            SetupSkillResources();
            MockRepository();
            MockUnitOfWork();
            SetupMapper();
            SetupService();
        }
       
        [Test]
        public async Task GetListOfSkillsAsync_ListOfSkillResourcesReturned()
        {
            var returnedSkills = await _service.GetAllSkillsAsync();
            Assert.IsTrue(returnedSkills.SequenceEqual(_skillResources));
        }
        
        [Test]
        public async Task GetSkillAsync_ValidIdAsParam_SkillResourceReturned()
        {
            const int id = 1;
            var extectedSkill = _skillResources.Single(s => s.Id == id);
            var returnedSkill = await _service.GetSkillsAsync(id);
            
            Assert.AreEqual(extectedSkill.Id,returnedSkill.Id);
            Assert.AreEqual(extectedSkill.Name,returnedSkill.Name);
        }
        
        [Test]
        public async Task GetSkillAsync_InvalidIdAsParam_NullReturned()
        {
            const int id = 10;
            var returnedSkill = await _service.GetSkillsAsync(id);
            Assert.IsNull(returnedSkill);
        }
        
        [Test]
        public async Task AddNewSkillAsync_SkillResourceAsParam_CreatedSkillResourceReturned()
        {
            var newSkill = new SkillResource() {Name = "New Skill"};
            

            var addedSkill = await _service.AddSkillAsync(newSkill);
            
            Assert.AreEqual(newSkill.Name,addedSkill.Name);
            Assert.IsNotNull(addedSkill.Id);
        }
        
        [Test]
        public async Task UpdateSkill_SkillResourceAsParam_UpdatedSkillResourceReturned()
        {
            const int id = 1;
            var skill = _skillResources.Single(s=>s.Id==id);
            skill.Name = "Updated name";

            var updatedSkill = await _service.UpdateSkillAsync(id, skill);
            
            Assert.AreEqual(skill,updatedSkill);
        }
        
        [Test]
        public async Task UpdateSkill_InvalidIdAsParam_NullReturned()
        {
            const int invalidId = 10;
            const int index = 0;
            var skill = _skillResources[index];
            skill.Name = "Updated name";

            var updatedSkill = await _service.UpdateSkillAsync(invalidId, skill);
            
            Assert.IsNull(updatedSkill);
        }
        
        [Test]
        public async Task UpdateSkill_DifferentIdInBody_SkillResourceWithOldIdReturned()
        {
            const int id = 1;
            var skill = _skillResources.Single(c=>c.Id==id);
            skill.Id++;
            skill.Name = "Updated name";

            var updatedSkill = await _service.UpdateSkillAsync(id, skill);
            
            Assert.AreNotEqual(skill.Id,updatedSkill.Id);
        }

        [Test]
        public async Task DeleteSkill_ValidIdAsPAram_DeletedIdReturned()
        {
            const int id = 1;

            var deletedId = await _service.DeleteSkillAsync(id);
            
            Assert.AreEqual(id,deletedId);
        }
        
        [Test]
        public async Task DeleteSkill_InvalidIdAsParam_NullReturned()
        {
            const int invalidId = 10;

            var deletedId = await _service.DeleteSkillAsync(invalidId);
            
            Assert.IsNull(deletedId);
        }
        
         private void SetupService()
        {
            _service = new SkillService(_repository.Object, _mapper, _unitOfWork.Object);
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

        private void SetupSkills()
        {
            _skills = new List<Skill>()
            {
                new Skill() {Id = 1, Name = "Skill_1"},
                new Skill() {Id = 2, Name = "Skill_2"},
                new Skill() {Id = 3, Name = "Skill_3"}
            };
        }
        
        private void SetupSkillResources()
        {
            _skillResources = new List<SkillResource>()
            {
                new SkillResource() {Id = 1, Name = "Skill_1"},
                new SkillResource() {Id = 2, Name = "Skill_2"},
                new SkillResource() {Id = 3, Name = "Skill_3"}
            };
        }
        
        private void MockRepository()
        {
            _repository = new Mock<ISkillRepository>();
            MockGetAllSkillsAsync();
            MockGetSkillAsync();
            MockAddSkillAsync();
            MockDeleteSkillAsync();
            
        }

        private void MockAddSkillAsync()
        {
            _repository.Setup(r => r.AddSkillAsync(It.IsAny<Skill>())).Returns<Skill>(async (s) =>
            {
                await Task.Yield();
                s.Id = new Random().Next();
                return new Skill() {Id = s.Id, Name = s.Name};
            });
        }

        private void MockGetSkillAsync()
        {
            _repository.Setup(r => r.GetSkillAsync(It.IsAny<int>())).Returns<int>(async (id) =>
            {
                
                await Task.Yield();
                return _skills.SingleOrDefault(s=>s.Id==id);
            });
        }
        
        private void MockDeleteSkillAsync()
        {
            _repository.Setup(r => r.DeleteSkill(It.IsAny<Skill>())).Verifiable();
        }

        private void MockGetAllSkillsAsync()
        {
            _repository.Setup(r => r.GetAllSkillsAsync()).Returns(async () =>
            {
                await Task.Yield();
                return _skills;
            });
        }


    }
}