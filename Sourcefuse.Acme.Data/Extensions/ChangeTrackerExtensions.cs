using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sourcefuse.Acme.Data.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static ChangeTracker ImplementNoDelete(this ChangeTracker changeTracker, bool errorOnDelete = false)
        {
            var deletedEntities = changeTracker.Entries()
                                                .Where(e => e.State == EntityState.Deleted);

            if (errorOnDelete && deletedEntities.Any())
                throw new Exception("Deleting of database records is not allowed");


            foreach (EntityEntry entity in deletedEntities)
            {
                entity.State = EntityState.Unchanged;
            }

            return changeTracker;
        }

        
    }
}
