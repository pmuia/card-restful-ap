namespace Cards.WEB.Models.Authentication
{
    public class TokenResponse
    {
        public ResponseStatus Status { get; set; }

        public IEnumerable<Token> Data { get; set; } //dynamic[]

        //public Paging Paging { get; set; }
    }

    public class ResponseStatus
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }

    public class Token
    {
        /// <summary>
        /// JWT token to be used in the subsequent calls for authorization
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// Timestamp at which the token expires
        /// </summary>
        public long expires { get; set; }

        /// <summary>
        /// Type of token
        /// </summary>
        public string tokenType { get; set; } = "Bearer";
    }
}
