using System.Threading.Tasks;
using DeviceTracker.Models;

namespace DeviceTracker.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device> GetOrCreate(string Device);
        Task SetInfo(string Device, string Info);
    }
}