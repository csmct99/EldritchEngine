
using UnityEngine;

namespace EldritchEngine.Rendering
{
	public class Grid
	{
		private Cell[,] _cells;

		public int Width { get; private set; } = 100;
		public int Height { get; private set; } = 100;

		public Grid(int width = 100, int height = 100)
		{
			Width = width;
			Height = height;
			_cells = new Cell[Height, Width];
			
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					_cells[x, y] = new Cell(x, y, this);
				}
			}
		}

		/// <summary>
		/// Clamped version of <see cref="GetCellUnsafe"/> is slower but safer.
		/// </summary>
		public Cell GetCell(int x, int y)
		{
			x = Mathf.Clamp(x, 0, Width - 1);
			y = Mathf.Clamp(y, 0, Height - 1);
			return _cells[x, y];
		}
		
		public Cell GetCellUnsafe(int x, int y)
		{
			return _cells[x, y];
		}

		public void UpdateGrid()
		{
			foreach (Cell cell in _cells)
			{
				cell.Particle?.Update();
			}
		}
	}
}
