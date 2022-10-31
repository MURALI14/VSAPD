using APIDev.Models.DTO;
using APIDev.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficlutyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficlutyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            return Ok(await walkDifficultyRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkDifficultyByIdAsync")]
        public async Task<IActionResult> GetWalkDifficultyByIdAsync(Guid id)
        {
            var walkd = await walkDifficultyRepository.GetAsync(id);

            if (walkd == null)
            {
                return NotFound();
            }
            var waldto = mapper.Map<Models.DTO.WalkDifficulty>(walkd);
            return Ok(waldto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficluty(Models.DTO.AddWalkDifficluty addWalkDifficluty)
        {
            var wldd = new Models.Domain.WalkDifficulty
            {
                code = addWalkDifficluty.Code
            };
            wldd = await walkDifficultyRepository.AddAsync(wldd);

            var wlddto = mapper.Map<Models.DTO.WalkDifficulty>(wldd);

            return CreatedAtAction(nameof(GetWalkDifficultyByIdAsync), new { id = wlddto.Id }, wlddto);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateWalkDifficulty(Guid id, AddWalkDifficluty addWalkDifficluty)
        {
            var Wld = new Models.Domain.WalkDifficulty
            {
                code = addWalkDifficluty.Code
            };
            var uwld = await walkDifficultyRepository.UpdateAsync(id, Wld);
            if (uwld == null)
            {
                return NotFound();
            }
            var uwldto = mapper.Map<Models.DTO.WalkDifficulty>(uwld);
            return Ok(uwldto);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            var dwd =await  walkDifficultyRepository.DeleteAsync(id);
            if (dwd == null)
            {
                return NotFound();
            }
            var dwdto = mapper.Map<Models.DTO.WalkDifficulty>(dwd);
            return Ok(dwdto);
        }
    }
}
