using Microsoft.EntityFrameworkCore;
using LaundryLog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
