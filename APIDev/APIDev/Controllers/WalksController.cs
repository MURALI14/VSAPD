using APIDev.Models.Domain;
using APIDev.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walk = await walkRepository.GetAllAsync();

            var walkdto = mapper.Map<List<Models.DTO.Walk>>(walk);

            return Ok(walkdto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByAsync")]
        public async Task<IActionResult> GetWalkByAsync(Guid id)
        {
            var walk = await walkRepository.GetWalkAsync(id);

            var walkdto = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkdto);
        }
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            walk = await walkRepository.AddAsync(walk);

            var walkdto = mapper.Map<Models.DTO.Walk>(walk);

            return CreatedAtAction(nameof(GetWalkByAsync), new { id = walkdto.Id }, walkdto);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            walk = await walkRepository.UpdateAsync(id, walk);
            if (walk == null)
            {
                return NotFound();
            }
            var walkdto = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkdto);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkd = await walkRepository.DeleteAsyn(id);

            if(walkd == null)
            {
                return NotFound();
            }

            var walkdto = mapper.Map< Models.DTO.Walk >(walkd);

            return Ok(walkdto);
         }
    }
}
