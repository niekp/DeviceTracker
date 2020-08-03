using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceTracker.Models
{
    public class Ping
    {
        [Key]
        public int Id { get; set; }

        public int DeviceId { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public Device Device { get; set; }

        public DateTime Time { get; set; }

        public int? BlockId { get; set; }

        [ForeignKey(nameof(BlockId))]
        public Block Block { get; set; }
    }
}
