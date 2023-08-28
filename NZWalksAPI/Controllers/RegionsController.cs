using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using System.Reflection.Metadata.Ecma335;

namespace NZWalksAPI.Controllers
{
    // htpps://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        // get all regions
        // htpps://localhost:1234/api/regions

        [HttpGet]
        public IActionResult GetAll()
        {

            // Get Data From Database - Domain models
            var regionsDomain = dbContext.Regions.ToList();

            //Map domain models to DTOS
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    ImageUrl = regionDomain.ImageUrl
                });
            }

            //retun DTOS to client
            return Ok(regionsDto);

        }



        // GET SINGLE REGION (Get Region By ID)
        // GET:htpps://localhost:1234/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);

            // Get Region Domain Model from DataBase
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);


            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region Dto

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                ImageUrl = regionDomain.ImageUrl

            };
            return Ok(regionDto);
        }
    

    // POST  To Create New Region
    // POST: htpps://localhost:1234/api/regions

    [HttpPost]
    public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        //Map or Convert DTO to Domain Model

        var regionDomainModel = new Region
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            ImageUrl = addRegionRequestDto.ImageUrl,
        };

        dbContext.Regions.Add(regionDomainModel);
        dbContext.SaveChanges();
   
        return CreatedAtAction(nameof())
    }   


    }

}
