using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents.AnimationTypes
{
    public abstract class Animation
    {
        public int Time { get; set; }
        public Animation(int time)
        {
            this.Time = time;
        }
    }
}
