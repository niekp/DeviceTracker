using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceTracker.Models;
using DeviceTracker.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeviceTracker.Controllers
{
    public class RuleController : Controller
    {
        private readonly IRuleRepository ruleRepository;

        public RuleController(IRuleRepository ruleRepository)
        {
            this.ruleRepository = ruleRepository;
        }

        public async Task<IActionResult> Index(int DeviceId)
        {
            ViewBag.DeviceId = DeviceId;
            return View(await ruleRepository.GetRules(User, DeviceId));
        }

        public IActionResult Create(int DeviceId)
        {
            return View(new Rule()
            {
                DeviceId = DeviceId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rule Rule)
        {
            if (ModelState.IsValid)
            {
                await ruleRepository.Create(User, Rule);
                return RedirectToAction(nameof(Index), new { Rule.DeviceId });
            }

            return View(Rule);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var rule = await ruleRepository.GetById(User, Id);
            var deviceId = rule.DeviceId;

            await ruleRepository.Delete(User, Id);
            return RedirectToAction(nameof(Index), new { deviceId });

        }
    }
}
