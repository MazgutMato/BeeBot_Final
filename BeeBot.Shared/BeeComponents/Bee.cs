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
		public Animation? animation { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public Rotation rotation { get; set; }
        public List<Block> collectedBlocks { get; set; }
        private readonly PlayingArea playingArea;
        public Bee(PlayingArea playingArea)
        {
            this.playingArea = playingArea;
            this.collectedBlocks = new List<Block>();
            this.Reset();
        }
        public void Reset()
        {
            this.positionX = 0;
            this.positionY = 0;
            this.rotation = Rotation.RIGHT;
            this.animation = null;
            foreach (Block block in this.collectedBlocks)
            {
                this.playingArea.playArea[block.X][block.Y].type = BlockType.Reward;
                this.playingArea.uncollectedReward.Add(block);
            }
            this.collectedBlocks.Clear();
        }

        public bool MoveForward()
        {
            switch (this.rotation)
            {
                case Rotation.UP:
                    if (this.positionY > 0)
                    {
                        if (this.playingArea.playArea[this.positionX][this.positionY - 1].type == BlockType.Barrier)
                            return false;
                        this.positionY--;
                        return true;
                    }
                    return false;
                case Rotation.DOWN:
                    if (this.positionY < this.playingArea.sizeY - 1)
                    {
                        if (this.playingArea.playArea[this.positionX][this.positionY + 1].type == BlockType.Barrier)
                            return false;
                        this.positionY++;
                        return true;
                    }
                    return false;
                case Rotation.RIGHT:
                    if (this.playingArea.sizeX - 1 > this.positionX)
                    {
                        if (this.playingArea.playArea[this.positionX + 1][this.positionY].type == BlockType.Barrier)
                            return false;
                        this.positionX++;
                        return true;
                    }
                    return false;
                case Rotation.LEFT:
                    if (this.positionX > 0)
                    {
                        if (this.playingArea.playArea[this.positionX - 1][this.positionY].type == BlockType.Barrier)
                            return false;
                        this.positionX--;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }

        public bool MoveBack()
        {
            switch (this.rotation)
            {
                case Rotation.UP:
                    if (this.positionY < this.playingArea.sizeY - 1)
                    {
                        if (this.playingArea.playArea[this.positionX][this.positionY + 1].type == BlockType.Barrier)
                            return false;
                        this.positionY++;                        
                        return true;
                    }
                    return false;
                case Rotation.DOWN:
                    if (this.positionY > 0)
                    {
                        if (this.playingArea.playArea[this.positionX][this.positionY - 1].type == BlockType.Barrier)
                            return false;
                        this.positionY--;
                        return true;
                    }
                    return false;
                case Rotation.RIGHT:
                    if (this.positionX > 0)
                    {
                        if (this.playingArea.playArea[this.positionX - 1][this.positionY ].type == BlockType.Barrier)
                            return false;
                        this.positionX--;
                        return true;
                    }
                    return false;
                case Rotation.LEFT:
                    if (this.positionX < this.playingArea.sizeX - 1)
                    {
                        if (this.playingArea.playArea[this.positionX + 1][this.positionY].type == BlockType.Barrier)
                            return false;
                        this.positionX++;
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        public void RotateLeft()
        {
            if (this.rotation == 0)
            {
                this.rotation = (Rotation)270;
            }
            else
            {
                this.rotation = (Rotation)((int)this.rotation - 90);
            }
        }
        public void RotateRight()
        {
            if (this.rotation == (Rotation)270)
            {
                this.rotation = (Rotation)0;
            }
            else
            {
                this.rotation = (Rotation)((int)this.rotation + 90);
            }
        }
        public void collectReward()
        {
            if(this.playingArea.playArea[this.positionX][this.positionY].type == BlockType.Reward)
            {
                this.playingArea.uncollectedReward.Remove(this.playingArea.playArea[this.positionX][this.positionY]);
                this.collectedBlocks.Add(this.playingArea.playArea[this.positionX][this.positionY]);
                this.playingArea.playArea[this.positionX][this.positionY].type = BlockType.None;
            }
        }
        private bool isHome()
        {
            return this.playingArea.playArea[this.positionX][this.positionY].type == BlockType.Finish ? true : false;
        }
        public bool stateOfGame()
        {
            if(this.isHome() && this.playingArea.uncollectedReward.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
