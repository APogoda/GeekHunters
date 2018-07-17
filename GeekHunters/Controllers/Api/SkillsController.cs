using System.Threading.Tasks;
using GeekHunters.Controllers.Resources;
using GeekHunters.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GeekHunters.Controllers.Api
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        private readonly ISkillService _skillService;

        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkillsAsync()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillAsync(int id)
        {
            var skill = await _skillService.GetSkillsAsync(id);
            if (skill == null)
                return NotFound();
            return Ok(skill);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddSkillAsync([FromBody] SkillResource skillResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdSkill = await _skillService.AddSkillAsync(skillResource);
            if (createdSkill == null)
                return BadRequest();
            return Ok(createdSkill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillAsync(int id, [FromBody] SkillResource skillResource)
        {
            if (!ModelState.IsValid)          
                return BadRequest(ModelState); 
           
            var updatedSkill = await _skillService.UpdateSkillAsync(id, skillResource);
            if (updatedSkill == null)
                return NotFound();
            return Ok(updatedSkill);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkillAsync(int id)
        {
            var candidate = await _skillService.DeleteSkillAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            return Ok(candidate);
        }

        
    }
}