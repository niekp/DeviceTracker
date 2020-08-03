using System.Threading.Tasks;

namespace DeviceTracker.Repositories
{
    public interface IPingRepository
    {
        Task Ping(string Device);
        Task Ping(int DeviceId);
    }
}