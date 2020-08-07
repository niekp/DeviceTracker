using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeviceTracker.Models;
using DeviceTracker.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeviceTracker.Controllers
{
    public class CheckController : Controller
    {
        private readonly IRuleRepository ruleRepository;
        private readonly IDeviceRepository deviceRepository;
        private readonly IBlockRepository blockRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IPingRepository pingRepository;
        private readonly IEmailSender emailSender;

        public CheckController(
            IRuleRepository ruleRepository,
            IDeviceRepository deviceRepository,
            IBlockRepository blockRepository,
            UserManager<IdentityUser> userManager,
            IPingRepository pingRepository,
            IEmailSender emailSender
        )
        {
            this.ruleRepository = ruleRepository;
            this.deviceRepository = deviceRepository;
            this.blockRepository = blockRepository;
            this.userManager = userManager;
            this.pingRepository = pingRepository;
            this.emailSender = emailSender;
        }

        private bool RuleValid(Rule rule, Block block)
        {
            return block.GetTimespan() >= rule.NotifyAfter
                && (
                    rule.Active == ActiveState.Active && block.IsActive()
                    || rule.Active == ActiveState.Inactive && !block.IsActive()
                );
        }

        public async Task<IActionResult> Index()
        {
            var devices = await deviceRepository.GetAll();
            foreach (var device in devices)
            {
                var rules = await ruleRepository.GetRules(device.Id);
                var block = await blockRepository.GetCurrentBlock(device.Id);

                var notifyUsers = rules.Where(r => RuleValid(r, block)).Select(r => r.User).Distinct();

                if (notifyUsers.Any())
                {
                    var ping = await pingRepository.GetMostRecentPing(device.Id);
                    var lastPing = ping.Time.ToString("dd-MM HH:mm:ss");
                    var currentBlock = string.Format("{0} - {1}", block.From.ToString("dd-MM HH:mm"), block.To.ToString("dd-MM HH:mm"));
                    var timeSpan = ((int)block.GetTimespan().TotalHours + block.GetTimespan().ToString(@"\:mm\:ss"));
                    var active = block.IsActive() ? "Ja" : "Nee";

                    foreach (var id in notifyUsers)
                    {
                        var user = userManager.Users.Where(u => u.Id == id).FirstOrDefault();
                        if (user is IdentityUser)
                        {
                            // Check cooldown
                            var authenticatedDevice = await deviceRepository.GetDeviceUser(device.Id, id);

                            if (!(authenticatedDevice is DeviceUser)
                                || authenticatedDevice.Status != DeviceUserStatus.Accepted
                                || authenticatedDevice.StartCooldown.AddHours(5) > DateTime.Now
                                )
                            {
                                continue;
                            }

                            await emailSender.SendEmailAsync(
                                user.Email,
                                string.Format("Waarschuwing: {0}", device.Identifier),
                                $"<strong>{device.Identifier}</strong><br />" +
                                $"<table style='width:400px;'>" +
                                $"<tr><td>Actief</td><td>{active}</td></tr>" +
                                $"<tr><td>Laatste bericht</td><td>{lastPing}</td></tr>" +
                                $"<tr><td>Huidig blok</td><td>{currentBlock}</td></tr>" +
                                $"<tr><td>Duur</td><td>{timeSpan}</td></tr>" +
                                $"</table>");

                            await deviceRepository.StartCooldown(user, device.Id);
                        }
                    }
                }
            }

            return Ok();
        }
    }
}
