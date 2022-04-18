using BeeBot.Shared.ControllersComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents.AnimationTypes
{
    public class Rotate : Animation
    {
        public int OldAngle { get; set; }
        public int NewAngle { get; set; }
        public Rotate(int oldAngle, int newAngle,CommandType type, int time) : base(type, time)
        {
            this.OldAngle = oldAngle;
            this.NewAngle = newAngle;
        }
    }
}
