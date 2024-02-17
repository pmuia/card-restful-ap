using Cards.WEB.Models.Authentication;

namespace Cards.WEB.Services.Interface.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> GetAccessToken();
    }
}
