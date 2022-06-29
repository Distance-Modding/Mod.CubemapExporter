using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.Rendering;
using static Distance.CubemapExporter.Constants;
using static Distance.CubemapExporter.Dialogs;

namespace Distance.CubemapExporter.Scripts
{
	public class Exporter : MonoBehaviour
	{
		public IEnumerator Export(LevelCubeMapRenderer renderer)
		{
			#region Save File Dialog
			FileDialog exportDialog = ExportDirectoryDialog();
			if (exportDialog.ShowDialog() != DialogResult.OK)
			{
				yield break;
			}
			FileInfo destination = new FileInfo(exportDialog.FileName);
			#endregion

			#region Caching current state
			LevelEditor editor = G.Sys.LevelEditor_;
			LevelLayer trackNodes = editor.WorkingLevel_.GetLayer(LAYER_TRACKNODES);
			bool trackNodesVisible = trackNodes.Visible_;

			HelperDisplayMode helperDisplay = editor.CenterPointsMode_;
			Camera camera = renderer.renderCamera_;
			RenderTexture active = RenderTexture.active;
			RenderTexture target = renderer.renderCamera_.targetTexture;
			int mask = camera.cullingMask;
			#endregion

			#region Setup 
			editor.ToggleHelperDisplayMode(HelperDisplayMode.DontDisplay);
			editor.SetCenterPointsActive(false);
			editor.SetLightIconsActive(false);

			trackNodes.Visible_ = false;
			RenderTexture canvas = CreateRenderTexture();
			camera.targetTexture = canvas;

			camera.transform.position = renderer.transform.position;
			editor.currentAxisGizmoLogic_.gameObject.SetActive(false);
			#endregion

			yield return new WaitForEndOfFrame();

			#region Take Snapshots
			Quaternion[] rotations = LevelCubeMapRendererAbstract.camRots_;
			for (int i = 0; i < rotations.Length; ++i)
			{
				camera.cullingMask = CULLING_MASK;
				renderer.gf_.enabled = true;
				camera.transform.rotation = rotations[i];

				camera.Render();
				SaveToFile(canvas, destination, $" face {i:D8}");
			}
			#endregion

			#region Restore State
			editor.currentAxisGizmoLogic_.gameObject.SetActive(true);
			editor.SetCenterPointsActive(false);
			editor.SetLightIconsActive(false);
			camera.cullingMask = mask;
			camera.targetTexture = target;
			canvas.Destroy();
			RenderTexture.active = active;
			trackNodes.Visible_ = trackNodesVisible;
			editor.ToggleHelperDisplayMode(helperDisplay);
			#endregion
		}

		public void SaveToFile(RenderTexture renderTexture, FileInfo destination, string suffix = "")
		{
			Texture2D texture2D = RenderTextureToTexture2D(renderTexture);
			SaveToFile(texture2D, destination, suffix);
			texture2D.Destroy();
		}

		public void SaveToFile(Texture2D texture, FileInfo destination, string suffix = "")
		{
			SaveToFile(texture.EncodeToPNG(), destination, suffix);
		}

		public void SaveToFile(byte[] buffer, FileInfo destination, string suffix = "")
		{
			string dest = destination.FullName;
			string fileName = Path.GetFileNameWithoutExtension(dest) + suffix + Path.GetExtension(dest);
			string filePath = Path.Combine(destination.Directory.FullName, fileName);

			File.WriteAllBytes(filePath, buffer);
		}

		public RenderTexture CreateRenderTexture()
		{
			return new RenderTexture(
				RESOLUTION,
				RESOLUTION,
				RENDER_TEXTURE_DEPTH,
				RENDER_TEXTURE_FORMAT,
				RENDER_TEXTURE_IO
			)
			{
				dimension = TextureDimension.Tex2D,
				useMipMap = MIPMAPS,
				mipMapBias = MIPMAP_BIAS,
				antiAliasing = ANTIALIASING_LEVEL,
				filterMode = FilterMode.Point,
				anisoLevel = ANISOTROPIC_LEVEL
			};
		}

		public Texture2D Create2DTexture(RenderTexture renderTexture)
		{
			return new Texture2D(
				renderTexture.width,
				renderTexture.height,
				renderTexture.format.ToTextureFormat(),
				MIPMAPS
			);
		}

		public Texture2D RenderTextureToTexture2D(RenderTexture renderTexture)
		{
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = renderTexture;
			Texture2D canvas = Create2DTexture(renderTexture);
			canvas.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
			canvas.Apply();
			RenderTexture.active = active;
			return canvas;
		}
	}
}
