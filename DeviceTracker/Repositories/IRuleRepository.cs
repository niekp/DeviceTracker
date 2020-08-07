using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace DeviceTracker.Repositories
{
    public interface IRuleRepository
    {
        Task<List<Rule>> GetRules(int DeviceId);
        Task<List<Rule>> GetRules(ClaimsPrincipal User, int DeviceId);
        Task Create(ClaimsPrincipal User, Rule rule);
        Task Delete(ClaimsPrincipal User, int id);
        Task<Rule> GetById(ClaimsPrincipal User, int id);
    }
}