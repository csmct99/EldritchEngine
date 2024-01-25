using EldritchEngine.Rendering;

using Unity.VisualScripting;

using UnityEngine;

namespace EldritchEngine.Particles
{
	public class SandParticle : Particle, IParticleRender, IParticleGravity
	{
		public SandParticle(Cell cell) : base(cell) 
		{
			
		}
		
		public Color GetColor()
		{
			return Color.yellow;
		}
		
		public override void Update()
		{
			ApplyGravity();
		}

		public void ApplyGravity()
		{
			if(IsAtBottom) return;
		
			if (CellBelow.IsEmpty)
			{
				MoveToCell(CellBelow);
			}
			else
			{
				int random = Random.Range(0, 2);
				if (random == 0)
				{
					if (CellBottomLeft.IsEmpty || CellBottomLeft.Particle.Mass < Mass)
					{
						MoveToCell(CellBottomLeft);
					}
					else if (CellBottomRight.IsEmpty || CellBottomRight.Particle.Mass < Mass)
					{
						MoveToCell(CellBottomRight);
					}
				}
				else
				{
					if (CellBottomRight.IsEmpty || CellBottomRight.Particle.Mass < Mass)
					{
						MoveToCell(CellBottomRight);
					}
					else if (CellBottomLeft.IsEmpty || CellBottomLeft.Particle.Mass < Mass)
					{
						MoveToCell(CellBottomLeft);
					}
				}
			}
		}


	}
}
