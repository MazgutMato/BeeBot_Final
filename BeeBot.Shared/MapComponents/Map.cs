using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.MapComponents
{
    public class Map
    {
        public string Name { get; set; }
        public string Description { get; set;}
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string RectangleFile { get; set; }
        public string BarrierFile { get; set; }
        public string RewardFile { get; set; }
        public string FinishFile { get; set; }
        public ICollection<Block> Blocks { get; set; }
        public Map()
        {
            this.Name = "Nová mapa";
            this.Description = "Pozbieraj všetky kvetiny, vyhni sa kríkom a dostaň sa do úľu!";
            this.SizeX = 3;
            this.SizeY = 2;
            this.RectangleFile = "";
            this.RewardFile = "";
            this.BarrierFile = "";
            this.FinishFile = "";
            this.Blocks = new List<Block>{
                new Block(1,0,BlockType.Barrier),
                new Block(1,1,BlockType.Reward),
                new Block(2,0,BlockType.Finish),
            };
        }
    }
}
