namespace WindowsParty.Repository.Tesonet.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Domain.Entities;
    using WindowsParty.Domain.Models;

    public class ServersQueryService : IServerQueryService
    {
        private readonly string _endpoint;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonConverterSettings;

        public ServersQueryService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _endpoint = configuration.GetSection("ServersEndpoint").Value;

            _jsonConverterSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<IList<Server>> GetAsync(TokenResult token)
        {
            try
            {
                if (TokenShouldBeProvided(token))
                {
                    return new List<Server>();
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var response = await _httpClient.GetAsync(_endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<Server>();
                }

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IList<Server>>(responseString, _jsonConverterSettings);
            }
            catch
            {
                return new List<Server>();
            }
        }

        private static bool TokenShouldBeProvided(TokenResult token)
        {
            return string.IsNullOrWhiteSpace(token.Token);
        }
    }
}