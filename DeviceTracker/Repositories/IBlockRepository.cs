using System.Threading.Tasks;
using DeviceTracker.Models;

namespace DeviceTracker.Repositories
{
    public interface IBlockRepository
    {
        Task ProccessPing(Ping ping);
        Task<Block> GetCurrentBlock(int DeviceId);
    }
}