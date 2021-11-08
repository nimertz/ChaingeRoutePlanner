using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChaingeRoutePlanner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };
        private readonly ILogger<RouteController> _logger;
        Uri baseAddress = new Uri("https://api.openrouteservice.org");
        private readonly String _apiKey = "5b3ce3597851110001cf624890b9bd86eea74d508e1e2d272ce4c7bd";

        public RouteController(ILogger<RouteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<List<Route>> Post(VroomInput vi)
        {
            using (var httpClient = new HttpClient {BaseAddress = baseAddress})
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept",
                    "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type",
                    "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);
                
                var json = JsonSerializer.Serialize(vi,_serializerOptions);
                _logger.LogInformation("JSON:\n" + json);
                using (var content = new StringContent(json,Encoding.UTF8, "application/json"))
                {
                    using (var response = await httpClient.PostAsync("/optimization", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation("REPONSE:\n" + responseData);
                        var result = JsonSerializer.Deserialize<VroomOutput>(responseData,_serializerOptions);
                        return result.Routes;
                    }
                }
            }
        }
    }
}
