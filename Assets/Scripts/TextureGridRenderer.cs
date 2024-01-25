using System;
using System.Diagnostics;

using EldritchEngine.Debugging;

using UnityEngine;

namespace EldritchEngine.Rendering
{
	public class TextureGridRenderer : MonoBehaviour, IGridRenderer
	{
		private Texture2D _texture;

		[SerializeField]
		private int height = 100;

		[SerializeField]
		private int width = 100;
		
		private PerformanceAnalyser _performanceAnalyser = new PerformanceAnalyser();

		public Grid Grid { get; set; }

		public void RenderGrid(Grid grid)
		{
			_performanceAnalyser.Reset("Render Grid");
			UpdateTexture(grid, _texture);
			_performanceAnalyser.Stop();
			_performanceAnalyser.RecordCurrentTime();
		}

		private void Awake()
		{
			Grid = new Grid(width, height);
			_texture = CreateTextureOfGrid(Grid);
			GetComponent<Renderer>().material.mainTexture = _texture;
			
			// Add some particles
			
			
		}

		private void Update()
		{
			Grid.UpdateGrid();
			RenderGrid(Grid);
		}

		private void OnGUI()
		{
			GUI.Label(new Rect(10, 10, 100, 20), $"Average: {_performanceAnalyser.GetAverageTime()}ms");
			GUI.Label(new Rect(10, 30, 100, 20), $"Max: {_performanceAnalyser.GetMaxTime()}ms");
			GUI.Label(new Rect(10, 50, 100, 20), $"Min: {_performanceAnalyser.GetMinTime()}ms");
		}

		/// <summary>
		/// Creates a texture of the grid.
		/// </summary>
		/// <param name="grid"></param>
		private Texture2D CreateTextureOfGrid(Grid grid)
		{
			Texture2D tex = new Texture2D(grid.Width, grid.Height);
			tex.filterMode = FilterMode.Point;

			Color[] colorArray = new Color[grid.Width * grid.Height];

			// Apply changes to the texture
			tex.SetPixels(colorArray);
			tex.Apply();

			return tex;
		}

		private void UpdateTexture(Grid grid, Texture2D texture)
		{
			Color[] colorArray = new Color[grid.Width * grid.Height];

			for (int y = 0; y < grid.Height; y++)
			{
				for (int x = 0; x < grid.Width; x++)
				{
					Cell cell = grid.GetCell(x, y);
					colorArray[y * grid.Width + x] = cell.Color;
				}
			}

			// Apply changes to the texture
			texture.SetPixels(colorArray);
			texture.Apply(false);
		}
	}
}
