using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceTracker.Models;

namespace DeviceTracker.Repositories
{
    public interface IRuleRepository
    {
        Task<List<Rule>> GetRules(ClaimsPrincipal User, int DeviceId);
    }
}