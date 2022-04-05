using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.MapComponents
{
    public class Map
    {
        public int MapId { get; set; }
        public string Name { get; set; }
        public string Description { get; set;}
        public int sizeX { get; set; }
        public int sizeY { get; set; }
        public string RectangleFile { get; set; }
        public string BarrierFile { get; set; }
        public string RewardFile { get; set; }
        public string FinishFile { get; set; }
        public ICollection<Block> Blocks { get; set; }
        public Map()
        {
            this.MapId = 0;
            this.Name = "Nová mapa";
            this.Description = "Získaj všetky odmeny a dostaň sa do ciela!";
            this.sizeX = 8;
            this.sizeY = 8;
            this.RectangleFile = "";
            this.RewardFile = "";
            this.BarrierFile = "";
            this.FinishFile = "";
            this.Blocks = new List<Block>();
        }
    }
}
