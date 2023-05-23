using Demo.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalityController : ControllerBase
    {
        private readonly IMunicipalityService _municipalityService;

        public MunicipalityController(IMunicipalityService municipalityService)
        {
            _municipalityService = municipalityService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName(string name , CancellationToken cancellationToken)
        {
            var result = await _municipalityService.SearchByNameAsync(name, cancellationToken);

            return Ok(result);

        }
    }
}
