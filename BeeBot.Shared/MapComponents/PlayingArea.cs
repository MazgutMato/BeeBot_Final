using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.MapComponents
{
    public enum Mode
    {
        Editing,
        Playing
    }
    public class PlayingArea
    {
        public int sizeX { get; set; }
        public int sizeY { get; set; }
        public ICollection<Block> uncollectedReward { get; set; }
        public Block[][] playArea { get; set; }
        public Mode? mode { get; set; }
        private readonly Map map;
        public PlayingArea(Map map)
        {
            this.map = map;
            this.changeMap(this.map);
        }
        public void changeSize(int x, int y)
        {
            Block[][] oldArea = this.playArea;
            int oldX = this.sizeX;
            int oldY = this.sizeY;
            this.sizeX = x;
            this.sizeY = y;
            this.uncollectedReward.Clear();
            this.playArea = new Block[this.sizeX][];
            for (int i = 0; i < this.sizeX; i++)
            {
                this.playArea[i] = new Block[this.sizeY];
                for (int j = 0; j < this.sizeY; j++)
                {
                    playArea[i][j] = new Block();
                    playArea[i][j].X = i;
                    playArea[i][j].Y = j;
                    if (i < oldX && j < oldY)
                    {
                        this.playArea[i][j].type = oldArea[i][j].type;
                        if(oldArea[i][j].type == BlockType.Reward)
                        {
                            this.uncollectedReward.Add(this.playArea[i][j]);
                        }
                    }                    
                }
            }
        }
        private void newPlayArea(Map map)
        {
            this.sizeX = map.sizeX;
            this.sizeY = map.sizeY;
            this.uncollectedReward = new List<Block>();
            this.playArea = new Block[this.sizeX][];
            for (int i = 0; i < this.sizeX; i++)
            {
                this.playArea[i] = new Block[this.sizeY];
                for (int j = 0; j < this.sizeY; j++)
                {
                    playArea[i][j] = new Block();
                    playArea[i][j].X = i;
                    playArea[i][j].Y = j;
                    playArea[i][j].type = BlockType.None;
                }
            }
        }
        public void changeMap(Map map)
        {
            this.newPlayArea(map);
            foreach (Block block in map.Blocks)
            {
                this.playArea[block.X][block.Y] = block;
                if(block.type == BlockType.Reward)
				{
                    this.uncollectedReward.Add(block);
				}
            }
        }
    }
}
