using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequestDTO regionRequestDTO)
        {
            // Request to Domain model
            var region = new Region()
            {
                Code = regionRequestDTO.Code,
                Area = regionRequestDTO.Area,
                Lat = regionRequestDTO.Lat,
                Longt = regionRequestDTO.Longt,
                Name = regionRequestDTO.Name,
                Population = regionRequestDTO.Population
            };
            // Pass details to repository and take it again by return
            region = await regionRepository.AddAsync(region);

            // Convert back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Longt = region.Longt,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from database
            var region = await regionRepository.DeleteAsync(id);

            // If null Not Found
            if(region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Longt = region.Longt,
                Name = region.Name,
                Population = region.Population
            };

            // Return response ok
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Convert DTO to Domain Model
            var region = new Region()
            {
                Code = updateRegionRequestDTO.Code,
                Area = updateRegionRequestDTO.Area,
                Lat = updateRegionRequestDTO.Lat,
                Longt = updateRegionRequestDTO.Longt,
                Name = updateRegionRequestDTO.Name,
                Population = updateRegionRequestDTO.Population
            };
            // Update Region using Repository
            region = await regionRepository.UpdateAsync(id, region);
            // If null then Not Found
            if(region == null)
            {
                return NotFound();
            }
            // Convert Domain to DTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Longt = region.Longt,
                Name = region.Name,
                Population = region.Population
            };
            // Return Ok response 
            return Ok(regionDTO);
        }

        // Antes do AutoMapper...
        //public IActionResult GetAllRegions()
        //{
        //    var regions = regionRepository.GetAll();

        //    var regionsDTO = new List<RegionDTO>();

        //    regions.ToList().ForEach(region =>
        //    {
        //        var regionDTO = new RegionDTO()
        //        {
        //            Id = region.Id,
        //            Name = region.Name,
        //            Code = region.Code,
        //            Area = region.Area,
        //            Lat = region.Lat,
        //            Longt = region.Longt,
        //            Population = region.Population,
        //        };
        //        regionsDTO.Add(regionDTO);
        //    });

        //    return Ok(regionsDTO);
        //}
    }
}
