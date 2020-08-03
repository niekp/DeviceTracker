using System;
using Microsoft.EntityFrameworkCore;
using DeviceTracker.Models;

namespace DeviceTracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<Block> Block { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Ping> Ping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=device.db");
    }
}
