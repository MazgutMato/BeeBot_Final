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
        public Block[][] playingArea { get; set; }
        public Mode? mode { get; set; }
        private readonly Map map;
        public PlayingArea(Map map)
        {
            this.map = map;
            this.sizeX = map.sizeX;
            this.sizeY = map.sizeY;
            this.uncollectedReward = new List<Block>();
            this.playingArea = new Block[this.sizeX][];
            for (int i = 0; i < this.sizeX; i++)
            {
                this.playingArea[i] = new Block[this.sizeY];
                for (int j = 0; j < this.sizeY; j++)
                {
                    playingArea[i][j] = new Block();
                    playingArea[i][j].X = i;
                    playingArea[i][j].Y = j;
                    playingArea[i][j].type = BlockType.None;
                }
            }
        }
        public void changeSize(int x, int y)
        {
            Block[][] oldArea = this.playingArea;
            int oldX = this.sizeX;
            int oldY = this.sizeY;
            this.sizeX = x;
            this.sizeY = y;
            this.playingArea = new Block[this.sizeX][];
            for (int i = 0; i < this.sizeX; i++)
            {
                this.playingArea[i] = new Block[this.sizeY];
                for (int j = 0; j < this.sizeY; j++)
                {
                    playingArea[i][j] = new Block();
                    playingArea[i][j].X = i;
                    playingArea[i][j].Y = j;
                    if (i < oldX && j < oldY)
                    {
                        this.playingArea[i][j].type = oldArea[i][j].type;
                        if(oldArea[i][j].type == BlockType.Reward)
                        {
                            this.uncollectedReward.Remove(oldArea[i][j]);
                        }
                    }                    
                }
            }
        }
    }
}
