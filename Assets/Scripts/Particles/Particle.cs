using EldritchEngine.Rendering;

using UnityEngine;

using Grid = EldritchEngine.Rendering.Grid;

namespace EldritchEngine.Particles
{
	public abstract class Particle
	{
		
		public Particle(Cell cell)
		{
			Cell = cell;
		}
		
		public Cell Cell { get; private set; }

		public Vector2Int Position => Cell.GridPosition;
		public Grid Grid => Cell.Grid;
		
		public Cell CellBelow => Grid.GetCell(Position.x, Position.y - 1);
		public Cell CellAbove => Grid.GetCell(Position.x, Position.y + 1);
		public Cell CellLeft => Grid.GetCell(Position.x - 1, Position.y);
		public Cell CellRight => Grid.GetCell(Position.x + 1, Position.y);
		
		public Cell CellBottomLeft => Grid.GetCell(Position.x - 1, Position.y - 1);
		public Cell CellBottomRight => Grid.GetCell(Position.x + 1, Position.y - 1);
		public Cell CellTopLeft => Grid.GetCell(Position.x - 1, Position.y + 1);
		public Cell CellTopRight => Grid.GetCell(Position.x + 1, Position.y + 1);
		
		public bool IsAtBottom => Position.y == 0;
		public bool IsAtWall => Position.x == 0 || Position.x == Grid.Width - 1;
		
		public float Mass { get; protected set; } = 1f;
		
		/// <summary>
		/// Moves the particle to the new cell, swapping with the particle in that cell if there is one.
		/// </summary>
		/// <param name="newCell"></param>
		protected void MoveToCell(Cell newCell)
		{
			Particle other = newCell.Particle;
			if (other != null)
			{
				other.Cell = Cell;
			}

			newCell.SetParticle(this);
			Cell.SetParticle(other);

			Cell = newCell;
		}

		public abstract void Update();

	}
}
