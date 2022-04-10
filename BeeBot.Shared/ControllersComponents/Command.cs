using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.ControllersComponents
{
    public class Command
    {
        public CommandType Type { get; set; }
        public bool Active { get; set; }
        public Command(CommandType type)
        {
            this.Type = type;
            this.Active = false;
        }
    }
    public enum CommandType
    {
        moveForward,
        rotateLeft,
        rotateRight,
        moveBack,
        none
    }
}
