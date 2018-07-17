

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekHunters.Controllers.Api;
using GeekHunters.Controllers.Resources;
using GeekHunters.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GeekHunters.UnitTests.Repositories
{
    [TestFixture]
    public class CandidatesControllerTests
    {
        private List<CandidateResource> _candidateResources;
        private Mock<ICandidateService> _service;
        private CandidatesController _controller;
        private int _okCodeExpected = 200;
        private int _notFoundCodeExpected = 404;
        private int _badRequestCodeExpected = 400;
        private FilterResource _filterResource;
        
        [SetUp]
        public void SetUp()
        {

            SetupCandidateResorces();
            _service = new Mock<ICandidateService>();
            MockGetAllCadnidatesAsync();
            MockGetCandidateAsync();
            MockAddCandidateAsync();
            MockUpdateCandidateAsync();
            MockDeleteSkillAsync();
            _controller = new CandidatesController(_service.Object);
            _filterResource = new FilterResource(){SkillId = 1};
        }
        
        [Test]
        public async Task GetListOfCandidatesAsync_OkReturned()
        {
            var result = await _controller.GetCandidatesAsync(_filterResource);
            AssertForOk(result);
        }
        
        [Test]
        public async Task GetCandidateAsync_ValidIdAsParam_OkReturned()
        {
            var id = 1;
            var result = await _controller.GetCandidateAsync(id);
            AssertForOk(result);
        } 
        
        [Test]
        public async Task GetCandidateAsync_InvalidIdAsParam_NotFoundReturned()
        {
            var id = 10;
            var result = await _controller.GetCandidateAsync(id);
            AssertForNotFound(result);
        } 
        
        [Test]
        public async Task AddCandidateAsync_CandidateCreateResourceAsParam_OkReturned()
        {
            var createCandidateResource = new CreateCandidateResource(){FirstName = "NewFirstName",LastName = "NewLastname"};
            
            var result = await _controller.AddCandidateAsync(createCandidateResource);
            AssertForOk(result);
        } 
        
        [Test]
        public async Task AddCandidateAsync_NullFirstNameAsParam_BadRequestReturned()
        {
            var createCandidateResource = new CreateCandidateResource();
            
            var result = await _controller.AddCandidateAsync(createCandidateResource);
            AssertForBadRequest(result);
        } 
        
        [Test]
        public async Task UpdateCandidate_CandidateCreateResourceAsParam_OkReturned()
        {
            const int id = 1;
            var createCandidateResource = new CreateCandidateResource(){FirstName = "FirstName_3"};

            var result = await _controller.UpdateCandidateAsync(id, createCandidateResource);
            
            AssertForOk(result);
        }
        
        [Test]
        public async Task UpdateCandidate_InvalidIdAsParam_NotFoundReturned()
        {
            const int id = 10;
            var createCandidateResource = new CreateCandidateResource(){FirstName = "FirstName_3"};
            var result = await _controller.UpdateCandidateAsync(id, createCandidateResource);
            AssertForNotFound(result);
        }
        
        [Test]
        public async Task DeleteCandidate_ValidIdAsPAram_OkReturned()
        {
            const int id = 1;

            var result = await _controller.DeleteCandidateAsync(id);
            AssertForOk(result);
        }
        
        [Test]
        public async Task DeleteCandidate_InvalidIdAsParam_NotFoundReturned()
        {
            const int invalidId = 10;

            var result = await _controller.DeleteCandidateAsync(invalidId);
            AssertForNotFound(result);
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
        
        private void MockGetAllCadnidatesAsync()
        {
            _service.Setup(c => c.GetAllCandidatesAsync(It.IsAny<FilterResource>())).Returns(async () =>
            {
                await Task.Yield();
                var result = new QueryResultResource<CandidateResource>() {Items = _candidateResources};
                return result;
            });
        }
        
        private void MockGetCandidateAsync()
        {
            _service.Setup(c => c.GetCandidateAsync(It.IsAny<int>())).Returns<int>(async (id) =>
            {
                
                await Task.Yield();
                return _candidateResources.SingleOrDefault(s=>s.Id==id);
            });
        }
        
        private void MockAddCandidateAsync()
        {
            _service.Setup(c => c.AddCandidateAsync(It.IsAny<CreateCandidateResource>())).Returns (async (CandidateResource c) =>
            {
                await Task.Yield();
                var candidateResource = new CandidateResource();
                return candidateResource;
            });
        }
        
        private void MockUpdateCandidateAsync()
        {
            _service.Setup(c => c.UpdateCandidateAsync(It.IsAny<int>(),It.IsAny<CreateCandidateResource>())).Returns<int,CandidateResource>(async (id,sr) =>
            {
                await Task.Yield();
                var candidate = await _service.Object.GetCandidateAsync(id);
                return candidate;
            });
        }
        
        private void MockDeleteSkillAsync()
        {
            _service.Setup(c => c.DeleteCandidateAsync(It.IsAny<int>())).Returns<int> (async (id) =>
            {
                await Task.Yield();
                var candidate = await _service.Object.GetCandidateAsync(id);
                return candidate?.Id;
            });
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
        
        private void AssertForBadRequest(IActionResult result)
        {
            var badRequestResult = result as BadRequestResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(_badRequestCodeExpected, badRequestResult.StatusCode);
        }
    }
}