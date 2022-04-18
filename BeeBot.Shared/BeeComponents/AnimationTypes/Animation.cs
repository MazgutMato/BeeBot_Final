using BeeBot.Shared.ControllersComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents.AnimationTypes
{
    public abstract class Animation
    {
        public CommandType Type { get; set; }
        public int Time { get; set; }
        public Animation(CommandType type, int time)
        {
            this.Type = type;
            this.Time = time;
        }
    }
}
