using BeeBot.Shared.BeeComponents.AnimationTypes;
using BeeBot.Shared.MapComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.BeeComponents
{
    public class Bee
    {
		public Animation? Animation { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Rotation Rotation { get; set; }
        public List<Block> CollectedBlocks { get; set; }
        private readonly PlayingArea playingArea;
        public Bee(PlayingArea playingArea)
        {
            this.playingArea = playingArea;
            this.CollectedBlocks = new List<Block>();
            this.Reset();
        }
        public void Reset()
        {
            this.PositionX = 0;
            this.PositionY = 0;
            this.Rotation = Rotation.RIGHT;
            this.Animation = null;
            foreach (Block block in this.CollectedBlocks)
            {
                this.playingArea.PlayArea[block.X][block.Y].Type = BlockType.REWARD;
                this.playingArea.UncollectedReward.Add(block);
            }
            this.CollectedBlocks.Clear();
        }

        public bool MoveForward()
        {
            switch (this.Rotation)
            {
                case Rotation.UP:
                    if (this.PositionY > 0)
                    {
                        if (this.playingArea.PlayArea[this.PositionX][this.PositionY - 1].Type == BlockType.BARRIER)
                            return false;
                        this.PositionY--;
                        return true;
                    }
                    return false;
                case Rotation.DOWN:
                    if (this.PositionY < this.playingArea.SizeY - 1)
                    {
                        if (this.playingArea.PlayArea[this.PositionX][this.PositionY + 1].Type == BlockType.BARRIER)
                            return false;
                        this.PositionY++;
                        return true;
                    }
                    return false;
                case Rotation.RIGHT:
                    if (this.playingArea.SizeX - 1 > this.PositionX)
                    {
                        if (this.playingArea.PlayArea[this.PositionX + 1][this.PositionY].Type == BlockType.BARRIER)
                            return false;
                        this.PositionX++;
                        return true;
                    }
                    return false;
                case Rotation.LEFT:
                    if (this.PositionX > 0)
                    {
                        if (this.playingArea.PlayArea[this.PositionX - 1][this.PositionY].Type == BlockType.BARRIER)
                            return false;
                        this.PositionX--;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        public bool MoveBack()
        {
            switch (this.Rotation)
            {
                case Rotation.UP:
                    if (this.PositionY < this.playingArea.SizeY - 1)
                    {
                        if (this.playingArea.PlayArea[this.PositionX][this.PositionY + 1].Type == BlockType.BARRIER)
                            return false;
                        this.PositionY++;                        
                        return true;
                    }
                    return false;
                case Rotation.DOWN:
                    if (this.PositionY > 0)
                    {
                        if (this.playingArea.PlayArea[this.PositionX][this.PositionY - 1].Type == BlockType.BARRIER)
                            return false;
                        this.PositionY--;
                        return true;
                    }
                    return false;
                case Rotation.RIGHT:
                    if (this.PositionX > 0)
                    {
                        if (this.playingArea.PlayArea[this.PositionX - 1][this.PositionY ].Type == BlockType.BARRIER)
                            return false;
                        this.PositionX--;
                        return true;
                    }
                    return false;
                case Rotation.LEFT:
                    if (this.PositionX < this.playingArea.SizeX - 1)
                    {
                        if (this.playingArea.PlayArea[this.PositionX + 1][this.PositionY].Type == BlockType.BARRIER)
                            return false;
                        this.PositionX++;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        public void RotateLeft()
        {
            if (this.Rotation == 0)
            {
                this.Rotation = (Rotation)270;
            }
            else
            {
                this.Rotation = (Rotation)((int)this.Rotation - 90);
            }
        }
        public void RotateRight()
        {
            if (this.Rotation == (Rotation)270)
            {
                this.Rotation = (Rotation)0;
            }
            else
            {
                this.Rotation = (Rotation)((int)this.Rotation + 90);
            }
        }
        public void CollectReward()
        {
            if(this.playingArea.PlayArea[this.PositionX][this.PositionY].Type == BlockType.REWARD)
            {
                this.playingArea.UncollectedReward.Remove(this.playingArea.PlayArea[this.PositionX][this.PositionY]);
                this.CollectedBlocks.Add(this.playingArea.PlayArea[this.PositionX][this.PositionY]);
                this.playingArea.PlayArea[this.PositionX][this.PositionY].Type = BlockType.RECTANGLE;
            }
        }
        private bool IsHome()
        {
            return this.playingArea.PlayArea[this.PositionX][this.PositionY].Type == BlockType.FINISH;
        }
        public bool StateOfGame()
        {
            if(this.IsHome() && this.playingArea.UncollectedReward.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
