using HarmonyLib;

namespace Distance.CubemapExporter.Harmony
{
	[HarmonyPatch(typeof(LevelCubeMapRenderer), "Visit")]
	internal static class LevelCubeMapRenderer__Visit
	{
		[HarmonyPostfix]
		internal static void Postfix(LevelCubeMapRenderer __instance, IVisitor visitor)
		{
			visitor.VisitAction("Export Cubemap To File", () => __instance.StartCoroutine(Mod.Instance.Exporter.Export(__instance)), true);
		}
	}
}
