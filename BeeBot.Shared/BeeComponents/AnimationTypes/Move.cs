using BeeBot.Shared.ControllersComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents.AnimationTypes
{
    public class Move : Animation
    {
        public string Path { get; set; }
        public Move(string path,CommandType type, int time) : base(type, time)
        {
            this.Path = path;
        }
    }
}
