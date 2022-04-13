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
			this.BlockEditor = BlockType.RECTANGLE;
		}

		public void IncreaseX()
		{
			this.map.SizeX++;
			this.playingArea.ChangeSize(this.map.SizeX, this.map.SizeY);
			this.OnChange?.Invoke();
		}
		public void IncreaseY()
		{
			this.map.SizeY++;
			this.playingArea.ChangeSize(this.map.SizeX , this.map.SizeY);
			this.OnChange?.Invoke();
		}
		public void DecreaseX()
		{
			if (this.map.SizeX > 2)
            {
				this.map.SizeX--;
				this.playingArea.ChangeSize(this.map.SizeX, this.map.SizeY);
				this.OnChange?.Invoke();
			}
		}
		public void DecreaseY()
		{
			if (this.map.SizeY > 2)
			{
				this.map.SizeY--;
				this.playingArea.ChangeSize(this.map.SizeX, this.map.SizeY);
				this.OnChange?.Invoke();
			}					
		}
		public void ChangeRectangleType(int x,int y)
        {
			if (this.BlockEditor == BlockType.REWARD && this.playingArea.PlayArea[x][y].Type != BlockType.REWARD)
            {
				this.playingArea.PlayArea[x][y].Type = BlockType.REWARD;
				this.playingArea.UncollectedReward.Add(this.playingArea.PlayArea[x][y]);
			}else if (this.BlockEditor != BlockType.REWARD && this.playingArea.PlayArea[x][y].Type == BlockType.REWARD)
			{
				this.playingArea.UncollectedReward.Remove(this.playingArea.PlayArea[x][y]);
				this.playingArea.PlayArea[x][y].Type = this.BlockEditor;
			} else
            {
				this.playingArea.PlayArea[x][y].Type = this.BlockEditor;
			}
			this.OnChange?.Invoke();
        }

		public Map SaveMap()
        {
			this.map.Blocks.Clear();
            for (int i = 0; i < this.map.SizeX; i++)
            {
                for (int j = 0; j < this.map.SizeY; j++)
                {
					if (this.playingArea.PlayArea[i][j].Type != BlockType.RECTANGLE)
                    {
						this.map.Blocks.Add(this.playingArea.PlayArea[i][j]);
                    }
                }
            }
			return this.map;
        }
		public void ChangeMap(Map map)
        {
			this.map.Name = map.Name;
			this.map.Description = map.Description;
			this.map.Files = map.Files;
			this.map.SizeX = map.SizeX;
			this.map.SizeY = map.SizeY;
			this.map.Blocks = map.Blocks;
			this.playingArea.ChangeMap(map);
			this.OnChange?.Invoke();
		}
		public void NewMap()
        {
			this.ChangeMap(new Map());
			this.OnChange?.Invoke();
		}
		public void DeletePlayArea()
        {
			this.map.Blocks.Clear();
			this.ChangeMap(this.map);
        }
		public void ChangeFile(BlockType type, string fileName)
		{
			this.map.Files[type] = fileName;
			this.OnChange?.Invoke();
		}
		public void changeBLockEditor(BlockType type)
		{
			this.BlockEditor = type;
			this.OnChange?.Invoke();
		}

		public event Action? OnChange;
	}
}
