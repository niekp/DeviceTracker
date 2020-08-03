using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeviceTracker.Data;
using DeviceTracker.Models;

namespace DeviceTracker.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext db;

        public DeviceRepository(DataContext db)
        {
            this.db = db;
        }

        public async Task<Device> GetOrCreate(string Device)
        {
            var d = await db.Device.Where(d => d.Identifier == Device).FirstOrDefaultAsync();
            if (d is Device)
            {
                return d;
            }

            d = new Device()
            {
                Identifier = Device
            };
            db.Add(d);
            await db.SaveChangesAsync();

            return d;
        }

        public async Task SetInfo(string Device, string Info)
        {
            var device = await GetOrCreate(Device);
            device.Info = Info;
            await db.SaveChangesAsync();
        }
    }
}
