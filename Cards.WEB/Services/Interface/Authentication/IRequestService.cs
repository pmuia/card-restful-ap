
namespace Cards.WEB.Services.Interface.Authentication
{
    public interface IRequestService
    {
        Task<TResult> PostAsync<TRequest, TResult>(string uri, string payload, string token);
        Task<TResult> GetAsync<TRequest, TResult>(string uri, string token);
        Task<TResult> PutAsync<TRequest, TResult>(string uri, string payload, string token);
    }
}
