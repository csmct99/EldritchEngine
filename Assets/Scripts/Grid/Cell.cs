using EldritchEngine.Particles;

using UnityEngine;

namespace EldritchEngine.Rendering
{
	public class Cell
	{
		public Vector2Int GridPosition { get; private set; }
		public Particle Particle { get; private set; }
		public Color Color { get; private set; }
		
		public Grid Grid { get; private set; }
		
		public bool IsEmpty => Particle == null;
		

		public Cell(int x, int y, Grid grid)
		{
			GridPosition = new Vector2Int(x, y);
			Particle = null;
			Color = Color.black; // Color of air
			Grid = grid;
		}
		
		public void SetParticle(Particle p)
		{
			Particle = p;
			
			bool CanRender = p is IParticleRender;
			if (CanRender)
			{
				IParticleRender render = (IParticleRender) p;
				Color = render.GetColor();
			}
			else
			{
				Color = Color.black;
			}
		}
	}
}
