using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.data;
using NZWalksAPI.Models.Domain;
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

            var regions = dbContext.Regions.ToList();

            return Ok(regions);

        }


        // GET SINGLE REGION (Get Region By ID)
        // GET:htpps://localhost:1234/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]

        public IActionResult GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);

            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }

}
