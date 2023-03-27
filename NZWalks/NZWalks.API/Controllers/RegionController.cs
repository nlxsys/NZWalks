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
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTO);
        }

        // Antes do AutoMapper...
        //public IActionResult GetAll()
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
