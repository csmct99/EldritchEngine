namespace EldritchEngine.Rendering
{
	public interface IGridRenderer
	{
		public Grid Grid { get; set; }
		public void RenderGrid(Grid grid);
		
	}
}
