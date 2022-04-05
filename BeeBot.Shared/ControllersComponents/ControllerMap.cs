using BeeBot.Shared.BeeComponents;
using BeeBot.Shared.MapComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeBot.Shared.ControllersComponents
{
	public class ControllerMap
	{
		private readonly Map map;
		private readonly PlayingArea playingArea;
		public BlockType BlockEditor { get; set; }
		public ControllerMap(Map map, PlayingArea playingArea)
		{
			this.map = map;
			this.playingArea = playingArea;
			this.BlockEditor = BlockType.None;
		}

		public void increaseX()
		{
			this.map.sizeX++;
			this.playingArea.changeSize(this.map.sizeX, this.map.sizeY);
			this.OnChange?.Invoke();
		}
		public void increaseY()
		{
			this.map.sizeY++;
			this.playingArea.changeSize(this.map.sizeX , this.map.sizeY);
			this.OnChange?.Invoke();
		}
		public void decreaseX()
		{
			if (this.map.sizeX > 2)
            {
				this.map.sizeX--;
				this.playingArea.changeSize(this.map.sizeX, this.map.sizeY);
				this.OnChange?.Invoke();
			}
		}
		public void decreaseY()
		{
			if (this.map.sizeY > 2)
			{
				this.map.sizeY--;
				this.playingArea.changeSize(this.map.sizeX, this.map.sizeY);
				this.OnChange?.Invoke();
			}					
		}
		public void changeRectangleType(int x,int y)
        {
			if (this.BlockEditor == BlockType.Reward && this.playingArea.playArea[x][y].type != BlockType.Reward)
            {
				this.playingArea.playArea[x][y].type = BlockType.Reward;
				this.playingArea.uncollectedReward.Add(this.playingArea.playArea[x][y]);
			}
			this.playingArea.playArea[x][y].type = this.BlockEditor;
			this.OnChange?.Invoke();
        }

		public Map saveMap()
        {
			this.map.Blocks.Clear();
            for (int i = 0; i < this.map.sizeX; i++)
            {
                for (int j = 0; j < this.map.sizeY; j++)
                {
					if (this.playingArea.playArea[i][j].type != BlockType.None)
                    {
						this.map.Blocks.Add(this.playingArea.playArea[i][j]);
                    }
                }
            }
			return this.map;
        }
		public void changeMap(Map map)
        {
			this.map.Name = map.Name;
			this.map.Description = map.Description;
			this.map.RewardFile = map.RewardFile;
			this.map.BarrierFile = map.BarrierFile;
			this.map.FinishFile = map.FinishFile;
			this.map.RectangleFile = map.RectangleFile;
			this.map.sizeX = map.sizeX;
			this.map.sizeY = map.sizeY;
			this.map.Blocks = map.Blocks;
			this.playingArea.changeMap(map);
			this.OnChange?.Invoke();
		}

		public event Action? OnChange;
	}
}
