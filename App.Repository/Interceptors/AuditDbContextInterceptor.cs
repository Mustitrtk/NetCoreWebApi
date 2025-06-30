using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Interceptors
{
    public class AuditDbContextInterceptor:SaveChangesInterceptor
    {
        private static Dictionary<EntityState, Action<DbContext, IAuditEntity>> _behavior = new()
        {
            {EntityState.Added,AddBehavior },
            {EntityState.Modified,ModifiedBehavior }
        };

        private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
        {
            auditEntity.Created = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
        {
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
            auditEntity.Updated = DateTime.Now;
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

                _behavior[entityEntry.State](eventData.Context,auditEntity);

                //switch (entityEntry.State)
                //{
                //    case EntityState.Added:


                //        AddBehavior(eventData.Context, auditEntity);

                //    break;

                //case EntityState.Modified:

                //        ModifiedBehavior(eventData.Context, auditEntity);

                //    break;
                //}
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
