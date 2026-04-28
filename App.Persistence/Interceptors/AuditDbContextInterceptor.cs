using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            { EntityState.Added, AddBehavior},
            { EntityState.Modified,ModifiedBehavior }
        };

        private static void AddBehavior(DbContext context, IAuditEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            context.Entry(entity).Property(e => e.Updated).IsModified = false;
        }

        private static void ModifiedBehavior(DbContext context, IAuditEntity entity)
        {
            entity.Updated = DateTime.UtcNow;
            context.Entry(entity).Property(e => e.Created).IsModified = false;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);
            foreach (var entry in context.ChangeTracker.Entries<IAuditEntity>())
            {
                if (Behaviors.TryGetValue(entry.State, out var behavior))
                {
                    behavior(context, entry.Entity);
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
