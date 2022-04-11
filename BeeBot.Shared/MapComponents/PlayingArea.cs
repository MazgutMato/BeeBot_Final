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
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public ICollection<Block> UncollectedReward { get; set; }
        public Block[][] PlayArea { get; set; }
        public Mode? Mode { get; set; }
        private readonly Map map;
        public PlayingArea(Map map)
        {
            this.map = map;
            this.ChangeMap(this.map);
        }
        public void ChangeSize(int x, int y)
        {
            Block[][] oldArea = this.PlayArea;
            int oldX = this.SizeX;
            int oldY = this.SizeY;
            this.SizeX = x;
            this.SizeY = y;
            this.UncollectedReward.Clear();
            this.PlayArea = new Block[this.SizeX][];
            for (int i = 0; i < this.SizeX; i++)
            {
                this.PlayArea[i] = new Block[this.SizeY];
                for (int j = 0; j < this.SizeY; j++)
                {
                    PlayArea[i][j] = new Block
                    {
                        X = i,
                        Y = j
                    };
                    if (i < oldX && j < oldY)
                    {
                        this.PlayArea[i][j].Type = oldArea[i][j].Type;
                        if(oldArea[i][j].Type == BlockType.REWARD)
                        {
                            this.UncollectedReward.Add(this.PlayArea[i][j]);
                        }
                    }                    
                }
            }
        }
        private void NewPlayArea(Map map)
        {
            this.SizeX = map.SizeX;
            this.SizeY = map.SizeY;
            this.UncollectedReward = new List<Block>();
            this.PlayArea = new Block[this.SizeX][];
            for (int i = 0; i < this.SizeX; i++)
            {
                this.PlayArea[i] = new Block[this.SizeY];
                for (int j = 0; j < this.SizeY; j++)
                {
                    PlayArea[i][j] = new Block
                    {
                        X = i,
                        Y = j,
                        Type = BlockType.RECTANGLE
                    };
                }
            }
        }
        public void ChangeMap(Map map)
        {
            this.NewPlayArea(map);
            foreach (Block block in map.Blocks)
            {
                this.PlayArea[block.X][block.Y] = block;
                if(block.Type == BlockType.REWARD)
				{
                    this.UncollectedReward.Add(block);
				}
            }
        }
    }
}
