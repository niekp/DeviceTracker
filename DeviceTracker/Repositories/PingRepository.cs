using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceTracker.Data;
using DeviceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceTracker.Repositories
{
    public class PingRepository : IPingRepository
    {
        private readonly DataContext db;
        private readonly IDeviceRepository deviceRepository;

        public PingRepository(
            DataContext db,
            IDeviceRepository deviceRepository
        )
        {
            this.db = db;
            this.deviceRepository = deviceRepository;
        }

        public Task<List<Ping>> GetUnproccessed()
        {
            return db.Ping
                .Where(p => p.BlockId == null)
                .OrderBy(p => p.Time)
                .ToListAsync();
        }

        public Task<Ping> GetMostRecentPing(int DeviceId)
        {
            return db.Ping
                .Where(b => b.DeviceId == DeviceId)
                .OrderByDescending(b => b.Time)
                .FirstOrDefaultAsync();
        }

        public async Task<Ping> Ping(string Device)
        {
            var device = await deviceRepository.GetOrCreate(Device);
            return await Ping(device.Id);
        }

        public async Task<Ping> Ping(int DeviceId)
        {
            var ping = new Ping()
            {
                DeviceId = DeviceId,
                Time = DateTime.Now,
            };

            db.Add(ping);
            await db.SaveChangesAsync();
            return ping;
        }
    }
}
