﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeviceTracker.Models;
using DeviceTracker.Data;
using DeviceTracker.Repositories;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DeviceTracker.Controllers
{
    [Authorize]
    public class DeviceController : Controller
    {
        private readonly IDeviceRepository deviceRepository;

        public DeviceController(
            IDeviceRepository deviceRepository
        )
        {
            this.deviceRepository = deviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DeviceAuthenticationViewModel()
            {
                All = await deviceRepository.GetAll(),
                Authenticated = await deviceRepository.GetAuthenticated(User)
            };

            return View(vm);
        }

        public async Task<IActionResult> RequestAccess(int Id)
        {
            await deviceRepository.RequestAccess(User, Id);
            return View("AccesRequested");
        }

        public async Task<IActionResult> GrantAccess(int Id, string Token)
        {
            try
            {
                await deviceRepository.GrantAccess(Id, Token);
            } catch (ArgumentException)
            {
                ViewBag.Error = "Ongeldige token";
            }
            return View("AccesGranted");
        }


    }
}
