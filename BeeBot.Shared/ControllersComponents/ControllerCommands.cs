using BeeBot.Shared.BeeComponents;
using BeeBot.Shared.BeeComponents.AnimationTypes;
using BeeBot.Shared.MapComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.ControllersComponents
{
    public class ControllerCommands
    {
        public List<Command> Commands { get; }        
        public CancellationTokenSource? CancellationToken { get; set; }
        private readonly Bee Bee;
        public ControllerCommands(Bee bee)
        {
            this.Bee = bee;
            this.Commands = new List<Command>();
            this.CancellationToken = null;
        }
        public void AddCommand(Command command)
        {
            this.CancellationToken?.Cancel();
            this.Commands.Add(command);
            this.OnChanged?.Invoke();            
        }
        public void StopAnimation()
        {
            this.CancellationToken?.Cancel();
            this.Bee.Reset();
            this.OnChanged?.Invoke();
        }
        public void DeleteCommands()
        {
            this.Commands.Clear();
            this.CancellationToken?.Cancel();
            this.Bee.Reset();
            this.OnChanged?.Invoke();
        }
        public async Task<bool?> StartAnimationAsync()
        {
            this.CancellationToken = new CancellationTokenSource();
            this.Bee.Reset();

            try
            {             
                foreach (Command command in this.Commands)
                {                    
                    //move animation
                    int startX = this.Bee.PositionX * 100 + 50;
                    int startY = this.Bee.PositionY * 100 + 50;
                    string path = "M " + startX + " " + startY;
                    //rotate animation
                    int oldAngle = (int)this.Bee.Rotation;
                    //time of animation
                    int time = 1;
                    //active command
                    command.Active = true;
                    this.OnChanged?.Invoke();
                    switch (command.Type)
                    {
                        case CommandType.moveForward:
                            if (this.Bee.MoveForward())
                            {
                                int newX = this.Bee.PositionX * 100 + 50;
                                int newY = this.Bee.PositionY * 100 + 50;
                                path += " L " + newX + " " + newY + " ";
                            }
                            else
                            {
                                int newX = this.Bee.PositionX * 100 + 50;
                                int newY = this.Bee.PositionY * 100 + 50;
                                switch (this.Bee.Rotation)
                                {
                                    case Rotation.UP:
                                        newY -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newY += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                    case Rotation.DOWN:
                                        newY += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newY -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                    case Rotation.RIGHT:
                                        newX += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newX -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                    case Rotation.LEFT:
                                        newX -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newX += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                }                                    
                            }
                            this.Bee.Animation = new Move(path,time);
                            break;
                        case CommandType.moveBack:
                            if (this.Bee.MoveBack())
                            {
                                int newX = this.Bee.PositionX * 100 + 50;
                                int newY = this.Bee.PositionY * 100 + 50;
                                path += "L " + newX + " " + newY + " ";
                            }
                            else
                            {
                                int newX = this.Bee.PositionX * 100 + 50;
                                int newY = this.Bee.PositionY * 100 + 50;
                                switch (this.Bee.Rotation)
                                {
                                    case Rotation.UP:
                                        newY += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newY -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                    case Rotation.DOWN:
                                        newY -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newY += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                    case Rotation.RIGHT:
                                        newX -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newX += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                    case Rotation.LEFT:
                                        newX += 10;
                                        path += "L " + newX + " " + newY + " ";
                                        newX -= 10;
                                        path += "L " + newX + " " + newY + " ";
                                        break;
                                }
                            }
                            this.Bee.Animation = new Move(path, time);
                            break;
                        case CommandType.rotateLeft:
                            this.Bee.RotateLeft();
                            this.Bee.Animation = new Rotate(oldAngle, (int)this.Bee.Rotation, time);
                            break;
                        case CommandType.rotateRight:
                            this.Bee.RotateRight();
                            this.Bee.Animation = new Rotate(oldAngle, (int)this.Bee.Rotation, time);
                            break;
                    }
                    this.OnChanged?.Invoke();

                    await Task.Delay((time * 900), this.CancellationToken.Token);

                    command.Active = false;
                    this.Bee.Animation = null;
                    this.Bee.CollectReward();
                    this.OnChanged?.Invoke();

                    await Task.Delay(500, this.CancellationToken.Token);
                }
            }
            catch (TaskCanceledException)
            {
                this.ResetCommands();
                this.Bee.Reset();
                this.Bee.Animation = null;
                this.CancellationToken = null;
                this.OnChanged?.Invoke();
                return null;
            }
            this.CancellationToken = null;
            this.OnChanged?.Invoke();
            return this.Bee.StateOfGame();
        }

        private void ResetCommands()
        {
            foreach(var command in this.Commands)
            {
                command.Active = false;
            }
        }

        public event Action? OnChanged;
    }
}
