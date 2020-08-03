using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceTracker.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }

        public int DeviceId { get; set; }

        [ForeignKey(nameof(DeviceId))]
        public Device Device { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public virtual ICollection<Ping> Pings { get; set; }

    }
}
