using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using Microsoft.Extensions.Options;

namespace ChaingeRoutePlanner.VroomClient
{
    public class VroomApiClient : IVroomApiClient
    {
        private readonly string? _host;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };
        
        public VroomApiClient(EnvironmentConfig config)
        {
            string? host = config.ApiEndpoint;
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (!host.EndsWith("/"))
            {
                host += "/";
            }

            _host = host;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("accept",
                "application/json, application/geo+json, application/gpx+xml, img/png; charset=utf-8");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type",
                "application/json; charset=utf-8");
            _client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", config.ApiKey);
        }

        public async Task<VroomOutput> PerformRequest(VroomInput vroomInput)
        {
            string input = JsonSerializer.Serialize(vroomInput, _serializerOptions);
            var response = await _client.PostAsync(_host,
                new StringContent(input, Encoding.UTF8, "application/json"));

            string content = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<VroomOutput>(content, _serializerOptions);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Server responded with status code {response.StatusCode}. Content: " +
                                    JsonSerializer.Serialize(output, _serializerOptions));
            }

            return output;
        }

        public async Task<bool> IsHealthy()
        {
            var result = await _client.GetAsync(_host + "health");
            return result.StatusCode == HttpStatusCode.OK;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}