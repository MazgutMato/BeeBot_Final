using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.MapComponents
{
    public class Block
    {
        public BlockType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Block()
        {
            this.X = 0;
            this.Y = 0;
            this.Type = BlockType.RECTANGLE;
        }
        public Block(int x, int y, BlockType type) {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }
    }
}
