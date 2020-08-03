using System;
using System.Threading.Tasks;
using DeviceTracker.Data;
using DeviceTracker.Models;

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

        public async Task Ping(string Device)
        {
            var device = await deviceRepository.GetOrCreate(Device);
            await Ping(device.Id);
        }

        public async Task Ping(int DeviceId)
        {
            db.Add(new Ping()
            {
                DeviceId = DeviceId,
                Time = DateTime.Now,
            });
            await db.SaveChangesAsync();
        }
    }
}
