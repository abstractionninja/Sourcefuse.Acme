namespace Sourcefuse.Acme.WebApi.Configuration
{
    /// <summary>
    /// IOptions class for swagger settings
    /// </summary>
    public class SwaggerSettings
    {
        /// <summary>
        /// Version number
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Path to swagger.json
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Title of Swagger UI page
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Optional path to a stylesheet to inject
        /// </summary>
        public string CssFile {  get; set; } = string.Empty;    

        /// <summary>
        /// Setting for allowing deep linking directly to a specific endpoint definition
        /// </summary>
        public bool AllowDeepLinking { get; set; } = true;
    }
}
