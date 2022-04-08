using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.MapComponents
{
    public class Block
    {
        public BlockType type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Block()
        {
            this.X = 0;
            this.Y = 0;
            this.type = BlockType.None;
        }
        public Block(int x, int y, BlockType type) {
            this.X = x;
            this.Y = y;
            this.type = type;
        }
    }
}
