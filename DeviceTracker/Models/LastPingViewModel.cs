using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceTracker.Models
{
    [NotMapped]
    public class LastPingViewModel
    {
        public List<DevicePing> Devices;
    }

    [NotMapped]
    public class DevicePing
    {
        public Device Device { get; set; }
        public Block Block { get; set; }
        public Ping LastPing { get; set; }
    }
}
