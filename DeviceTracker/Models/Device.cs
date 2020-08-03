using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeviceTracker.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        public string Identifier { get; set; }

        public virtual ICollection<Block> Blocks { get; set; }

        public string Info { get; set; }
    }
}
