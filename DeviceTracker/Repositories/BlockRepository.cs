using System;
using System.Linq;
using System.Threading.Tasks;
using DeviceTracker.Data;
using DeviceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceTracker.Repositories
{
    public class BlockRepository : IBlockRepository
    {
        private readonly DataContext db;

        public BlockRepository(DataContext db)
        {
            this.db = db;
        }

        public async Task<Block> GetCurrentBlock(int DeviceId)
        {
            var block = await db.Block
                    .Where(b => b.DeviceId == DeviceId)
                    .OrderByDescending(b => b.To)
                    .FirstOrDefaultAsync();

            if (block.To.AddMinutes(15) < DateTime.Now)
            {
                block = new Block()
                {
                    DeviceId = DeviceId,
                    From = block.To,
                    To = DateTime.Now
                };
            }
            return block;
        }

        public async Task ProccessPing(Ping ping)
        {
            var block = db.Block.Where(b =>
                b.To.AddMinutes(15) > ping.Time
            ).FirstOrDefault();

            if (!(block is Block))
            {
                block = new Block()
                {
                    DeviceId = ping.DeviceId,
                    From = ping.Time,
                    To = ping.Time
                };
                db.Add(block);
            }

            ping.Block = block;
            if (ping.Time > block.To)
            {
                block.To = ping.Time;
            }

            await db.SaveChangesAsync();
        }
    }
}
