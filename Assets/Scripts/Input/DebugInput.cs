using System;

using EldritchEngine.Rendering;

using UnityEngine;

namespace EldritchEngine.Debugging
{
	public class DebugInput : MonoBehaviour
	{
		private Rendering.Grid _grid;
 
		public MonoBehaviour GridRenderer;
		
		private void Start()
		{
			if (GridRenderer is IGridRenderer gridRenderer)
			{
				_grid = gridRenderer.Grid;
			}
		}

		private void Update()
		{
			DrawSandAtMouse(_grid);
			DrawWaterAtMouse(_grid);
		}

		public void DrawSandAtMouse(Rendering.Grid grid)
		{
			if (Input.GetMouseButton(0))
			{
				Cell cell = grid.GetCell((int) Input.mousePosition.x, (int) Input.mousePosition.y);
				cell.SetParticle(new Particles.SandParticle(cell));
			}
		}
		
		public void DrawWaterAtMouse(Rendering.Grid grid)
		{
			if (Input.GetMouseButton(1))
			{
				Cell cell = grid.GetCell((int) Input.mousePosition.x, (int) Input.mousePosition.y);
				cell.SetParticle(new Particles.WaterParticle(cell));
			}
		}
	}
}
