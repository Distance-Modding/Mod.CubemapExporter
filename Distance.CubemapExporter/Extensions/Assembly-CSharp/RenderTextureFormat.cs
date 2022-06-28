#pragma warning disable RCS1110, RCS1136

using UnityEngine;

public static class RenderTextureFormatExtensions
{
	public static TextureFormat ToTextureFormat(this RenderTextureFormat rtFormat)
	{
		switch (rtFormat)
		{
			case RenderTextureFormat.ARGB1555: return TextureFormat.ARGB4444;
			case RenderTextureFormat.ARGB2101010: return TextureFormat.ARGB32;
			case RenderTextureFormat.ARGB32: return TextureFormat.ARGB32;
			case RenderTextureFormat.ARGB4444: return TextureFormat.ARGB4444;
			case RenderTextureFormat.ARGBFloat: return TextureFormat.RGBAFloat;
			case RenderTextureFormat.ARGBHalf: return TextureFormat.RGBAFloat;
			case RenderTextureFormat.ARGBInt: return TextureFormat.ARGB32;
			case RenderTextureFormat.BGRA32: return TextureFormat.BGRA32;
			case RenderTextureFormat.R8: return TextureFormat.R16;
			case RenderTextureFormat.RFloat: return TextureFormat.RFloat;
			case RenderTextureFormat.RHalf: return TextureFormat.RFloat;
			case RenderTextureFormat.RGB111110Float: return TextureFormat.RGBAFloat;
			case RenderTextureFormat.RGB565: return TextureFormat.RGB565;
			case RenderTextureFormat.RGFloat: return TextureFormat.RGFloat;
			case RenderTextureFormat.RGHalf: return TextureFormat.RGFloat;
			case RenderTextureFormat.RGInt: return TextureFormat.EAC_RG;
			case RenderTextureFormat.RInt: return TextureFormat.R16;
			case RenderTextureFormat.Shadowmap: return TextureFormat.ARGB32;
			case RenderTextureFormat.Depth: return TextureFormat.ARGB32;
			case RenderTextureFormat.Default: return TextureFormat.ARGB32;
			case RenderTextureFormat.DefaultHDR: return TextureFormat.ARGB32;
			default: return TextureFormat.ARGB32;
		}
	}
}
