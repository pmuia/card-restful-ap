
namespace Core.Domain.Common
{
    public class AccessToken
    {
        public string access_token { get; set; } = string.Empty;
        public string expire_in { get; set; } = string.Empty;
    }
}
