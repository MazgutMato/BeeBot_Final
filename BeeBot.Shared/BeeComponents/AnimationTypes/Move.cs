using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents.AnimationTypes
{
    public class Move : Animation
    {
        public string path { get; set; }
        public Move(string path, int time) : base(time)
        {
            this.path = path;
        }
    }
}
