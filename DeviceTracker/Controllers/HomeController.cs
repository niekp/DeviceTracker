using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeviceTracker.Models;
using DeviceTracker.Data;
using DeviceTracker.Repositories;

namespace DeviceTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDeviceRepository deviceRepository;
        private readonly IBlockRepository blockRepository;
        private readonly IPingRepository pingRepository;

        public HomeController(
            IDeviceRepository deviceRepository,
            IBlockRepository blockRepository,
            IPingRepository pingRepository
        )
        {
            this.deviceRepository = deviceRepository;
            this.blockRepository = blockRepository;
            this.pingRepository = pingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new LastPingViewModel()
            {
                Devices = new List<DevicePing>()
            };

            foreach (var device in await deviceRepository.GetAll())
            {
                var block = await blockRepository.GetCurrentBlock(device.Id);
                var ping = await pingRepository.GetMostRecentPing(device.Id);

                vm.Devices.Add(new DevicePing()
                {
                    Device = device,
                    Block = block,
                    LastPing = ping
                });
            }

            return View(vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
