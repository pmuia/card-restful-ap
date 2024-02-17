
using Newtonsoft.Json;
using Cards.WEB.Models.Authentication;
using Cards.WEB.Services.Interface.Authentication;

namespace Cards.WEB.Services.Implementation.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestService _requestService;
        public IConfiguration Configuration { get; }

        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(IRequestService requestService, IConfiguration configuration, ILogger<AuthenticationService> logger)
        {
            _requestService = requestService;

            Configuration = configuration;

            _logger = logger;
        }

        public async Task<TokenResponse> GetAccessToken()
        {
            var response = new TokenResponse();
            try
            {
                var token = new TokenRequest
                {
                    ApiKey = Configuration.GetSection("Keys")["Key"],
                    AppSecret = Configuration.GetSection("Keys")["secret"]
                };

                var payload = JsonConvert.SerializeObject(token);

                response = await _requestService.PostAsync<TokenRequest, TokenResponse>(GlobalSettings.AuthEndpoint, payload, string.Empty);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
