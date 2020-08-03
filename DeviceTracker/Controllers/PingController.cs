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

        public PingController(IPingRepository pingRepository, IDeviceRepository deviceRepository)
        {
            this.pingRepository = pingRepository;
            this.deviceRepository = deviceRepository;
        }

        public async Task<IActionResult> Ping(string device, string info = "")
        {
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

            await pingRepository.Ping(device);

            return Ok();
        }
    }
}
