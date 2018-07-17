using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekHunters.Controllers.Resources;
using GeekHunters.Models;
using GeekHunters.Persistence;
using GeekHunters.Services;
using GeekHunters.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekHunters.Controllers.Api
{
    [Route("api/[controller]")]
    public class CandidatesController : Controller
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCandidatesAsync(FilterResource filterResource)
        {
           var candidates = await _candidateService.GetAllCandidatesAsync(filterResource);
           return Ok(candidates);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateAsync(int id)
        {
            var candidate = await _candidateService.GetCandidateAsync(id);
            if (candidate == null)
                return NotFound();
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidateAsync([FromBody] CreateCandidateResource createCandidateResource)
        {
            if (!ModelState.IsValid)
               return BadRequest(ModelState);
            var newCandidate = await _candidateService.AddCandidateAsync(createCandidateResource);
            if (newCandidate == null)
                return BadRequest();
            return Ok(newCandidate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidateAsync(int id, [FromBody] CreateCandidateResource createCandidateResource)
        {
            if (!ModelState.IsValid)          
                return BadRequest(ModelState); 
           
            var updatedCandidate = await _candidateService.UpdateCandidateAsync(id, createCandidateResource);
            if (updatedCandidate == null)
                return NotFound();
            return Ok(updatedCandidate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidateAsync(int id)
        {
            var candidate = await _candidateService.DeleteCandidateAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

    } 
} 