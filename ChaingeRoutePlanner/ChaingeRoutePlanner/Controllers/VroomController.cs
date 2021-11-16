using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using ChaingeRoutePlanner.VroomClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChaingeRoutePlanner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VroomController : ControllerBase
    {
        private readonly ILogger<VroomController> _logger;
        private IVroomApiClient _vroomApiClient;


        public VroomController(ILogger<VroomController> logger, EnvironmentConfig config)
        {
            _logger = logger;
            _vroomApiClient = new VroomApiClient(config);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<VroomOutput> Post(VroomInput vi)
        {
            var response = await _vroomApiClient.PerformRequest(vi);

            return response;
        }
    }
}
