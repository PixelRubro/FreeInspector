using System;

namespace PixelRouge.Inspector
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class AssetPreviewAttribute : BaseAttribute
	{
		public int Width;
		public int Height;

		public AssetPreviewAttribute(int width = 48, int height = 48)
		{
			Width = width;
			Height = height;
		}
	}
}
