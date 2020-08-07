using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceTracker.Data;
using DeviceTracker.Models;
using Microsoft.AspNetCore.Identity;
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

        public async Task Create(ClaimsPrincipal User, Rule rule)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            rule.User = id;
            db.Add(rule);
            await db.SaveChangesAsync();
        }

        public async Task Delete(ClaimsPrincipal User, int id)
        {
            var rule = await GetById(User, id);
            if (rule is Rule)
            {
                db.Remove(rule);
                await db.SaveChangesAsync();
            }
        }

        public Task<Rule> GetById(ClaimsPrincipal User, int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return db.Rule.Where(r => r.User == userId && r.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Rule>> GetRules(ClaimsPrincipal User, int DeviceId)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return db.Rule.Where(
                r => r.User == id
                && r.DeviceId == DeviceId
            ).ToListAsync();
        }

        public Task<List<Rule>> GetRules(int DeviceId)
        {
            return db.Rule.Where(r =>
                r.DeviceId == DeviceId
            ).ToListAsync();
        }

    }
}