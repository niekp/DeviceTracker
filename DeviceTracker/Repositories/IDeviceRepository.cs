using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace DeviceTracker.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAll();
        Task<List<Device>> GetAuthenticated(ClaimsPrincipal User);
        Task<List<Device>> GetAuthenticated(string User);
        Task<Device> GetOrCreate(string Device);
        Task SetInfo(string Device, string Info);
        Task RequestAccess(ClaimsPrincipal User, int DeviceId);
        Task GrantAccess(int id, string Token);
        Task<DeviceUser> GetDeviceUser(int deviceId, string User);
    }
}