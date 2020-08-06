using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceTracker.Data;
using DeviceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceTracker.Repositories
{
    public class RuleRepository : IRuleRepository
    {
        private readonly DataContext db;

        public RuleRepository(DataContext db)
        {
            this.db = db;
        }

        public Task<List<Rule>> GetRules(ClaimsPrincipal User, int DeviceId)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return db.Rule.Where(
                r => r.User == id
                && r.DeviceId == DeviceId
            ).ToListAsync();
        }
    }
}