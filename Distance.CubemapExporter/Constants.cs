using UnityEngine;

namespace Distance.CubemapExporter
{
	internal static class Constants
	{
		public const string LAYER_TRACKNODES = "TrackNodes";
		public const int CULLING_MASK = int.MaxValue;
		public const int RESOLUTION = 4096;
		public const int ANISOTROPIC_LEVEL = 16;
		public const int ANTIALIASING_LEVEL = 8;
		public const int RENDER_TEXTURE_DEPTH = 32;
		public const RenderTextureFormat RENDER_TEXTURE_FORMAT = RenderTextureFormat.ARGB32;
		public const TextureFormat TEXTURE_FORMAT = TextureFormat.ARGB32;
		public const RenderTextureReadWrite RENDER_TEXTURE_IO = RenderTextureReadWrite.sRGB;
		public const bool MIPMAPS = false;
		public const float MIPMAP_BIAS = -1f;
		public const string DIALOG_FILTER = "Portable Network Graphics (*.png)|*.png";
		public const string DIALOG_TITLE = "Export Cubemap as Image";
	}
}