using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return View(await ruleRepository.GetRules(User, DeviceId));
        }
    }
}
