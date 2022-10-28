using APIDev.Models.Domain;
using APIDev.Models.DTO;
using APIDev.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //var regionsDTO = new List<Models.DTO.Region>();

            //return DTO regions
            //regions.ToList().ForEach(region =>
            //{
            //  var regionDTO = new Models.DTO.Region()
            //{
            //  Id = region.Id,
            //Code = region.Code,
            //Name = region.Name,
            //Area = region.Area,
            //Lat = region.Lat,
            //Long = region.Long,
            //Population = region.Population,
            //};
            //regionsDTO.Add(regionDTO);
            //});
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,

            };
            //pass details to repository
            region = await regionRepository.AddAsync(region);
            //convert back to DTO.
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionByIdAsync(Guid id)
        {
            //get reguion from database
            var delRegion = await regionRepository.DeleteAsync(id);

            //if null not found
            if (delRegion == null)
            {
                return NotFound();
            }
            //convert to DTO
            var deldto = mapper.Map<Models.DTO.Region>(delRegion);
            //return ok
            return Ok(deldto);

        }
        [HttpPut]
        [Route("{id:guid}") ]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody]Models.DTO.AddRegionRequest upRegionRequest)
        {
            //dto to domain
            var region = new Models.Domain.Region()
            {
                Code = upRegionRequest.Code,
                Area = upRegionRequest.Area,
                Lat = upRegionRequest.Lat,
                Long = upRegionRequest.Long,
                Name = upRegionRequest.Name,
                Population = upRegionRequest.Population,

            };

            //update
            region = await regionRepository.UpdateAsync(id, region);

            //not null
            if(region == null)
            {
                return NotFound();
            }
            //back to dto
            var dtreg = mapper.Map<Models.DTO.Region>(region);
            //return
            return Ok(dtreg);
        }
    }
}
