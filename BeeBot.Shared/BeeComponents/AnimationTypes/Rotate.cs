using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents.AnimationTypes
{
    public class Rotate : Animation
    {
        public int oldAngle { get; set; }
        public int newAngle { get; set; }
        public Rotate(int oldAngle, int newAngle, int time) : base(time)
        {
            this.oldAngle = oldAngle;
            this.newAngle = newAngle;
        }
    }
}
