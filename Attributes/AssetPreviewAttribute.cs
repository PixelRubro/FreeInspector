using System;

namespace YoukaiFox.Inspector
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class AssetPreviewAttribute : YoukaiAttribute
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
