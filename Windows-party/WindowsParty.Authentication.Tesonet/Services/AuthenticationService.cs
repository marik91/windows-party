namespace WindowsParty.Authentication.Tesonet.Services
{
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using WindowsParty.Domain.Contracts;
    using WindowsParty.Domain.Models;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly string _endpoint;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonConverterSettings;

        public AuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _endpoint = configuration.GetSection("AuthenticationEndpoint").Value;

            _jsonConverterSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<TokenResult> LogInAsync(Credentials credentials)
        {
            try
            {
                if (UsernameShouldBeSpecified(credentials) || PasswordShouldBeSpecified(credentials))
                {
                    return new TokenResult();
                }

                var jsonCredentials = JsonConvert.SerializeObject(credentials, _jsonConverterSettings);
                var content = new StringContent(jsonCredentials, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await _httpClient.PostAsync(_endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new TokenResult();
                }

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TokenResult>(responseString, _jsonConverterSettings);
            }
            catch
            {
                return new TokenResult();
            }
        }

        private static bool PasswordShouldBeSpecified(Credentials credentials)
        {
            return string.IsNullOrWhiteSpace(credentials.Password);
        }

        private static bool UsernameShouldBeSpecified(Credentials credentials)
        {
            return string.IsNullOrWhiteSpace(credentials.Username);
        }
    }
}