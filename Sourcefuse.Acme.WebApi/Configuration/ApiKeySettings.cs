namespace Sourcefuse.Acme.WebApi.Configuration
{
    /// <summary>
    /// IOptions class for api key settings
    /// </summary>
    public class ApiKeySettings
    {
        /// <summary>
        /// Name of header key to find api key
        /// </summary>
        public string ApiKeyHeaderKey { get; set; } = string.Empty;

        /// <summary>
        /// Default api key
        /// </summary>
        public string DefaultApiKey { get; set; } = string.Empty;  

        /// <summary>
        /// Optional response message for failed api key authentication
        /// </summary>
        public string ApiKeyFailureMessage { get; set; } = string.Empty;
    }
}
