using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceTracker.Models;

namespace DeviceTracker.Repositories
{
    public interface IPingRepository
    {
        Task<Ping> GetMostRecentPing(int DeviceId);
        Task<List<Ping>> GetUnproccessed();
        Task<Ping> Ping(string Device);
        Task<Ping> Ping(int DeviceId);
    }
}