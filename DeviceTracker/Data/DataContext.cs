using System;
using Microsoft.EntityFrameworkCore;
using DeviceTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DeviceTracker.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext()
        {
        }

        public DbSet<Block> Block { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceUser> DeviceUser { get; set; }
        public DbSet<Ping> Ping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=DeviceTracker.db");
    }
}
