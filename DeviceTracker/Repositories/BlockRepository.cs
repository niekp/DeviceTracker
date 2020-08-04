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
        private const int BLOCK_MINUTE_TRESHOLD = 30;
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

            if (!(block is Block))
            {
                return null;
            }

            if (block.To.AddMinutes(BLOCK_MINUTE_TRESHOLD) < DateTime.Now)
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
                b.DeviceId == ping.DeviceId
                && b.To.AddMinutes(BLOCK_MINUTE_TRESHOLD) > ping.Time
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
