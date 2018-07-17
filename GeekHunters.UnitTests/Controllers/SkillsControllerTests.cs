using System;
using System.Collections.Generic;
using System.Linq;
using GeekHunters.Controllers.Resources;
using GeekHunters.Services.Implementation;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using GeekHunters.Controllers.Api;
using Microsoft.AspNetCore.Mvc;


namespace GeekHunters.UnitTests.Repositories
{
    [TestFixture]
    public class SkillsControllerTests
    {
        private List<SkillResource> _skillResources;
        private Mock<ISkillService> _service;
        private SkillsController _controller;
        private int _okCodeExpected = 200;
        private int _notFoundCodeExpected = 404;

        [SetUp]
        public void SetUp()
        {
            
            SetupSkillResources();
            _service = new Mock<ISkillService>();
            MockGetAllSkillsAsync();
            MockGetSkillAsync();
            MockAddSkillAsync();
            MockUpdateSkillAsync();
            MockDeleteSkillAsync();
            _controller = new SkillsController(_service.Object);
        }
        
        [Test]
        public async Task GetListOfSkillsAsync_OkReturned()
        {
            var result = await _controller.GetSkillsAsync();
            AssertForOk(result);
        }

        

        [Test]
        public async Task GetSkillAsync_ValidIdAsParam_OkReturned()
        {
            var id = 1;
            var result = await _controller.GetSkillAsync(id);
            AssertForOk(result);
        } 
        
        [Test]
        public async Task GetSkillAsync_InvalidIdAsParam_NotFoundReturned()
        {
            var id = 10;
            var result = await _controller.GetSkillAsync(id);
            AssertForNotFound(result);
        }

        

        [Test]
        public async Task AddSkillAsync_SkillResourceAsParam_OkReturned()
        {
            var skillResource = new SkillResource() {Name = "Skill_3"};
            
            var result = await _controller.AddSkillAsync(skillResource);
            AssertForOk(result);
        } 
        
        [Test]
        public async Task UpdateSkill_SkillResourceAsParam_OkReturned()
        {
            const int id = 1;
            var skill = _skillResources.Single(s=>s.Id==id);
            skill.Name = "Updated name";

            var result = await _controller.UpdateSkillAsync(id, skill);
            
            AssertForOk(result);
        }
        
        [Test]
        public async Task UpdateSkill_SkillResourceAsParam_NotFoundReturned()
        {
            const int id = 10;
            const int index = 0;
            var skill = _skillResources[index];
            skill.Name = "Updated name";

            var result = await _controller.UpdateSkillAsync(id, skill);
            AssertForNotFound(result);
        }
        
        [Test]
        public async Task DeleteSkill_ValidIdAsPAram_OkReturned()
        {
            const int id = 1;

            var result = await _controller.DeleteSkillAsync(id);
            AssertForOk(result);
        }
        
        [Test]
        public async Task DeleteSkill_InvalidIdAsParam_NotFoundReturned()
        {
            const int invalidId = 10;

            var result = await _controller.DeleteSkillAsync(invalidId);
            AssertForNotFound(result);
        }

        private void MockGetAllSkillsAsync()
        {
            _service.Setup(s => s.GetAllSkillsAsync()).Returns(async () =>
            {
                await Task.Yield();
                return _skillResources;
            });
        }
        
        private void MockGetSkillAsync()
        {
            _service.Setup(s => s.GetSkillsAsync(It.IsAny<int>())).Returns<int>(async (id) =>
            {
                
                await Task.Yield();
                return _skillResources.SingleOrDefault(s=>s.Id==id);
            });
        }
        
        private void MockAddSkillAsync()
        {
            _service.Setup(s => s.AddSkillAsync(It.IsAny<SkillResource>())).Returns<SkillResource>(async (s) =>
            {
                await Task.Yield();
                s.Id = new Random().Next();
                return new SkillResource() {Id = s.Id, Name = s.Name};
            });
        }
        
        private void MockUpdateSkillAsync()
        {
            _service.Setup(s => s.UpdateSkillAsync(It.IsAny<int>(),It.IsAny<SkillResource>())).Returns<int,SkillResource>(async (id,sr) =>
            {
                await Task.Yield();
                var skill = await _service.Object.GetSkillsAsync(id);
                return skill;
            });
        }
        
        private void MockDeleteSkillAsync()
        {
            _service.Setup(s => s.DeleteSkillAsync(It.IsAny<int>())).Returns<int> (async (id) =>
            {
                await Task.Yield();
                var skill = await _service.Object.GetSkillsAsync(id);
                return skill?.Id;
            });;
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
        
        private void AssertForOk(IActionResult result)
        {
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(_okCodeExpected, okResult.StatusCode);
        }
        
        private void AssertForNotFound(IActionResult result)
        {
            var notFoundResult = result as NotFoundResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(_notFoundCodeExpected, notFoundResult.StatusCode);
        }
    }
}