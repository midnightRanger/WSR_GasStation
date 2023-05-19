using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_GasStation.Domain.Models
{
    public class StationDbContext : DbContext
    {
        public DbSet<Station> Station { get; set; }
        public DbSet<StationInfo> StationInfo { get; set; }

        public StationDbContext(DbContextOptions options) : base(options)
        {
        }

        public StationDbContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<StationInfo>().HasKey(s => s.ID_StationInfo);
            modelBuilder.Entity<Station>().HasKey(s => s.ID_Station);
            modelBuilder.Entity<StationInfo>().HasOne(s => s.Station).WithMany(e => e.StationInfos).HasForeignKey(s => s.StationId); 
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=GasStation; Trusted_Connection=True");
        }
    }
}
