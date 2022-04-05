using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.MapComponents
{
    public class Block
    {
        public int BlockId { get; set; }
        public BlockType type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
