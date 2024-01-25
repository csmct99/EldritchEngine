using EldritchEngine.Rendering;


using UnityEngine;

namespace EldritchEngine.Particles
{
	public class WaterParticle : Particle, IParticleRender, IParticleGravity
	{
		public WaterParticle(Cell cell) : base(cell) 
		{
			Mass = 0.5f;
		}
		
		public Color GetColor()
		{
			return Color.blue;
		}
		
		public override void Update()
		{
			ApplyGravity();
		}

		public void ApplyGravity()
		{
			if(IsAtBottom) return;
			if(IsAtWall) return;
		
			if (CellBelow.IsEmpty)
			{
				MoveToCell(CellBelow);
			}
			else
			{
				int random = Random.Range(0, 2);
				if (random == 0)
				{
					if(CellLeft.IsEmpty)
					{
						MoveToCell(CellLeft);
					}
					else if(CellRight.IsEmpty)
					{
						MoveToCell(CellRight);
					}
					else if (CellBottomLeft.IsEmpty)
					{
						MoveToCell(CellBottomLeft);
					}
					else if (CellBottomRight.IsEmpty)
					{
						MoveToCell(CellBottomRight);
					}
				}
				else
				{
					if(CellRight.IsEmpty)
					{
						MoveToCell(CellRight);
					}
					else if(CellLeft.IsEmpty)
					{
						MoveToCell(CellLeft);
					}
					else if (CellBottomRight.IsEmpty)
					{
						MoveToCell(CellBottomRight);
					}
					else if (CellBottomLeft.IsEmpty)
					{
						MoveToCell(CellBottomLeft);
					}
				}
			}
		}


	}
}
