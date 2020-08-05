using System;
using System.Collections.Generic;

namespace DeviceTracker.Models
{
    public class DeviceAuthenticationViewModel
    {
        public List<Device> All { get; set; }
        public List<Device> Authenticated { get; set; }
    }
}
