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
        public Dictionary<BlockType, string> Files { get; set; }
        public ICollection<Block> Blocks { get; set; }
        public Map()
        {
            this.Name = "Nová mapa";
            this.Description = "Pozbieraj všetky kvetiny, vyhni sa kríkom a dostaň sa do úľu!";
            this.SizeX = 3;
            this.SizeY = 2;
            this.Files = new Dictionary<BlockType, string>();
            this.Files.Add(BlockType.REWARD, "images/defaultMap/reward.svg");
            this.Files.Add(BlockType.RECTANGLE, "images/defaultMap/rectangle.svg");
            this.Files.Add(BlockType.BARRIER, "images/defaultMap/barrier.svg");
            this.Files.Add(BlockType.FINISH, "images/defaultMap/finish.svg");
            this.Blocks = new List<Block>{
                new Block(1,0,BlockType.BARRIER),
                new Block(1,1,BlockType.REWARD),
                new Block(2,0,BlockType.FINISH),
            };
        }
    }
}
