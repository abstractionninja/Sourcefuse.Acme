namespace Sourcefuse.Acme.Data.Abstracts
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDt { get; set; }

    }
}
