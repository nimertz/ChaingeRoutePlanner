using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using ChaingeRoutePlanner.VroomClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChaingeRoutePlanner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly ILogger<RouteController> _logger;
        private IVroomApiClient _vroomApiClient;


        public RouteController(ILogger<RouteController> logger, EnvironmentConfig config)
        {
            _logger = logger;
            _vroomApiClient = new VroomApiClient(config);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<Route>> Post(VroomInput vi)
        {
            var response = await _vroomApiClient.PerformRequest(vi);

            return response.Routes;
        }
    }
}
