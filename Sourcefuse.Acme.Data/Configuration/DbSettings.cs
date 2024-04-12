namespace Sourcefuse.Acme.Data.Configuration
{
    public class DbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public bool AllowDelete { get; set; } = false;
        public bool ErrorOnDelete { get; set; } = false;
    }
}
