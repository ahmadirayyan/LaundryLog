using Microsoft.EntityFrameworkCore;
using LaundryLog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace LaundryLog.Web.Data
{
    public class LauContext : DbContext
    {
        public LauContext(DbContextOptions<LauContext> options) : base(options)
        {

        }

        public DbSet<LauItem> LauItems { get; set; }
        public DbSet<LauUnit> LauUnits { get; set; }
        public DbSet<LauLog> LauLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LauItem>().ToTable("LauItem");
            modelBuilder.Entity<LauUnit>().ToTable("LauUnit");
            modelBuilder.Entity<LauLog>().ToTable("LauLog");
        }

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimeStamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));
            
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).DateCreated = DateTime.Now;
                }

                ((BaseModel)entity.Entity).DateModified = DateTime.Now;
            }
        }
    }
}
