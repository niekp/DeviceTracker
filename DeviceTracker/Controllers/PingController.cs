using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeviceTracker.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeviceTracker.Controllers
{
    public class PingController : Controller
    {
        private readonly IPingRepository pingRepository;
        private readonly IDeviceRepository deviceRepository;
        private readonly IBlockRepository blockRepository;

        public PingController(IPingRepository pingRepository,
            IDeviceRepository deviceRepository,
            IBlockRepository blockRepository
        )
        {
            this.pingRepository = pingRepository;
            this.deviceRepository = deviceRepository;
            this.blockRepository = blockRepository;
        }

        public async Task Proccess()
        {
            foreach (var ping in await pingRepository.GetUnproccessed())
            {
                await blockRepository.ProccessPing(ping);
            }
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Ping(string device, string info = "")
        {
            if (string.IsNullOrEmpty(device))
            {
                ModelState.AddModelError("device", "Device not set");
                return BadRequest();
            }

            try
            {
                if (!string.IsNullOrEmpty(info))
                {
                    await deviceRepository.SetInfo(device, info);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var ping = await pingRepository.Ping(device);
            await blockRepository.ProccessPing(ping);

            return Ok();
        }
    }
}
