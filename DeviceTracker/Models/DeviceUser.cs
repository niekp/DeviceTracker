using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceTracker.Models
{
    public class DeviceUser
    {
        public int Id { get; set; }

        public string User { get; set; }

        public int DeviceId { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public virtual Device Device { get; set; }

        public DeviceUserStatus Status { get; set; }

        public string Token { get; set; }
    }

    public enum DeviceUserStatus
    {
        Requested,
        Accepted
    }
}
