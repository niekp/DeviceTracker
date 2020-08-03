using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceTracker.Models;

namespace DeviceTracker.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAll();
        Task<Device> GetOrCreate(string Device);
        Task SetInfo(string Device, string Info);
    }
}