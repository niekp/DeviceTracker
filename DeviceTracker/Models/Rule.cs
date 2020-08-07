using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceTracker.Models
{
    public class Rule
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int DeviceId { get; set; }
        public ActiveState Active { get; set; }
        public TimeSpan NotifyAfter { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public virtual Device Device { get; set; }

        public DateTime StartCooldown { get; set; }
    }

    public enum ActiveState
    {
        Active,
        Inactive
    }
}
