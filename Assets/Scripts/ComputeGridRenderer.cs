using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace EldritchEngine.Rendering
{
	public class ComputeGridRenderer : MonoBehaviour, IGridRenderer
	{
		public Grid Grid { get; set; }

		public RawImage rawImage;
		
		public ComputeShader computeShader;
		public RenderTexture renderTexture;
		private ComputeBuffer pixelBuffer;
		private int kernelHandle;
		
		public int GridSize = 16;
		
		void Awake()
		{
			// Initialize the RenderTexture
			renderTexture = new RenderTexture(GridSize, GridSize, 24);
			renderTexture.enableRandomWrite = true; // This is required to be able to write to the texture from a compute shader
			renderTexture.filterMode = FilterMode.Point;
			renderTexture.format = RenderTextureFormat.ARGBFloat;
			renderTexture.Create();

			rawImage.texture = renderTexture;

			// Initialize the ComputeBuffer
			pixelBuffer = new ComputeBuffer(GridSize * GridSize, sizeof(float) * 4); // Assuming RGBA32 format

			// Get the kernel ID
			kernelHandle = computeShader.FindKernel("CSMain"); 

			// Set the buffer and texture on the compute shader
			computeShader.SetBuffer(kernelHandle, "InputPixels", pixelBuffer);
			computeShader.SetTexture(kernelHandle, "Result", renderTexture);
			computeShader.SetInt("Width",  GridSize);
			
			Grid = new Grid(GridSize, GridSize);
		}

		void Update()
		{
			Render();
			Grid.UpdateGrid();
		}
		
		void Render()
		{
			RenderGrid(Grid);
		}

		void OnDestroy()
		{
			// Release the buffer to prevent memory leaks
			pixelBuffer.Release();
		}

		// Utility function to get the render texture
		public RenderTexture GetRenderTexture()
		{
			return renderTexture;
		}

		public void RenderGrid(Grid grid)
		{
			// Update your pixel data here
			Color[] pixels = new Color[grid.Width * grid.Height]; // Replace with your actual pixel data
			
			for (var i = 0; i < pixels.Length; i++)
			{
				Cell cell = grid.GetCellUnsafe(i % grid.Width, i / grid.Width);
				pixels[i] = cell.Color;
			}

			// Set data to buffer
			pixelBuffer.SetData(pixels);

			// Dispatch the compute shader
			computeShader.Dispatch(kernelHandle, GridSize / 16, GridSize / 16, 1);
		}
	}
}
