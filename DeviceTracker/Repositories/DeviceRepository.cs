using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeviceTracker.Data;
using DeviceTracker.Models;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;

namespace DeviceTracker.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext db;
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DeviceRepository(DataContext db, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.emailSender = emailSender;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<List<Device>> GetAll()
        {
            return db.Device.ToListAsync();
        }

        public Task<List<Device>> GetAuthenticated(ClaimsPrincipal User)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (
                from d in db.Device
                join du in db.DeviceUser on d.Id equals du.DeviceId
                where du.User == id && du.Status == DeviceUserStatus.Accepted
                select d
            ).ToListAsync();
        }

        public async Task<Device> GetOrCreate(string Device)
        {
            var d = await db.Device.Where(d => d.Identifier == Device).FirstOrDefaultAsync();
            if (d is Device)
            {
                return d;
            }

            d = new Device()
            {
                Identifier = Device
            };
            db.Add(d);
            await db.SaveChangesAsync();

            return d;
        }

        public async Task GrantAccess(int id, string Token)
        {
            var request = await db.DeviceUser.Where(d => d.Id == id && d.Token == Token).FirstOrDefaultAsync();
            if (!(request is DeviceUser))
            {
                throw new ArgumentException("Invalid request");
            }

            request.Status = DeviceUserStatus.Accepted;
            await db.SaveChangesAsync();
        }

        public async Task RequestAccess(ClaimsPrincipal User, int DeviceId)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.FindFirstValue(ClaimTypes.Name);
            var device = await db.Device.Where(d => d.Id == DeviceId).FirstOrDefaultAsync();
            if (!(device is Device) || string.IsNullOrEmpty(id))
            {
                return;
            }

            var request = await db.DeviceUser.Where(d => d.DeviceId == DeviceId && d.User == id).FirstOrDefaultAsync();
            if (!(request is DeviceUser))
            {
                request = new DeviceUser()
                {
                    DeviceId = DeviceId,
                    User = id,
                    Status = DeviceUserStatus.Requested,
                    Token = Guid.NewGuid().ToString()
                };
                db.Add(request);
                await db.SaveChangesAsync();
            }

            var url = string.Format("{0}://{1}/Device/GrantAccess/?Id={2}&Token={3}",
                httpContextAccessor.HttpContext.Request.Scheme,
                httpContextAccessor.HttpContext.Request.Host,
                request.Id,
                request.Token);

            await emailSender.SendEmailAsync("device-admin@niekvantoepassing.nl",
                "Toegang tot apparaat",
                $"De gebruiker {name} wil toegang tot {device.Identifier}. <a href='{url}'>Geef toegang</a>");
        }

        public async Task SetInfo(string Device, string Info)
        {
            var device = await GetOrCreate(Device);
            device.Info = Info;
            await db.SaveChangesAsync();
        }
    }
}
