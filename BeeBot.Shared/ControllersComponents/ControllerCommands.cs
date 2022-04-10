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
        public Bee Bee { get; set; }
        public CancellationTokenSource? CancellationToken { get; set; }
        public ControllerCommands(Bee bee)
        {
            this.Bee = bee;
            this.Commands = new List<Command>();
            this.CancellationToken = null;
        }
        public void addCommand(Command command)
        {
            this.CancellationToken?.Cancel();
            this.Commands.Add(command);
            this.OnChanged?.Invoke();            
        }
        public void stopAnimation()
        {
            this.CancellationToken?.Cancel();
            this.Bee.Reset();
            this.OnChanged?.Invoke();
        }
        public void deleteCommands()
        {
            this.Commands.Clear();
            this.CancellationToken?.Cancel();
            this.Bee.Reset();
            this.OnChanged?.Invoke();
        }
        public async Task<bool?> startAnimationAsync()
        {
            this.CancellationToken = new CancellationTokenSource();
            this.Bee.Reset();

            try
            {             
                foreach (Command command in this.Commands)
                {                    
                    //move animation
                    int startX = this.Bee.positionX * 100 + 50;
                    int startY = this.Bee.positionY * 100 + 50;
                    string path = "M " + startX + " " + startY;
                    //rotate animation
                    int oldAngle = (int)this.Bee.rotation;
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
                                int newX = this.Bee.positionX * 100 + 50;
                                int newY = this.Bee.positionY * 100 + 50;
                                path += " L " + newX + " " + newY + " ";
                            }
                            else
                            {
                                int newX = this.Bee.positionX * 100 + 50;
                                int newY = this.Bee.positionY * 100 + 50;
                                switch (this.Bee.rotation)
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
                            this.Bee.animation = new Move(path,time);
                            break;
                        case CommandType.moveBack:
                            if (this.Bee.MoveBack())
                            {
                                int newX = this.Bee.positionX * 100 + 50;
                                int newY = this.Bee.positionY * 100 + 50;
                                path += "L " + newX + " " + newY + " ";
                            }
                            else
                            {
                                int newX = this.Bee.positionX * 100 + 50;
                                int newY = this.Bee.positionY * 100 + 50;
                                switch (this.Bee.rotation)
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
                            this.Bee.animation = new Move(path, time);
                            break;
                        case CommandType.rotateLeft:
                            this.Bee.RotateLeft();
                            this.Bee.animation = new Rotate(oldAngle, (int)this.Bee.rotation, time);
                            break;
                        case CommandType.rotateRight:
                            this.Bee.RotateRight();
                            this.Bee.animation = new Rotate(oldAngle, (int)this.Bee.rotation, time);
                            break;
                    }
                    this.OnChanged?.Invoke();

                    await Task.Delay((time * 900), this.CancellationToken.Token);

                    command.Active = false;
                    this.Bee.animation = null;
                    this.Bee.collectReward();
                    this.OnChanged?.Invoke();

                    await Task.Delay(500, this.CancellationToken.Token);
                }
            }
            catch (TaskCanceledException)
            {
                this.resetCommands();
                this.Bee.Reset();
                this.Bee.animation = null;
                this.CancellationToken = null;
                this.OnChanged?.Invoke();
                return null;
            }
            this.CancellationToken = null;
            this.OnChanged?.Invoke();
            return this.Bee.stateOfGame();
        }

        private void resetCommands()
        {
            foreach(var command in this.Commands)
            {
                command.Active = false;
            }
        }

        public event Action? OnChanged;
    }
}
