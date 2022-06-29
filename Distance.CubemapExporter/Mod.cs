using System.Collections;
using Distance.CubemapExporter.Scripts;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Distance.CubemapExporter
{
	[ModEntryPoint("com.github.reherc/CubemapExporter")]
	public sealed class Mod : MonoBehaviour
	{
		public static Mod Instance { get; private set; }

		public IManager Manager { get; private set; }

		public Log Logger { get; private set; }

		public Exporter Exporter { get; set; }

		public void Initialize(IManager manager)
		{
			DontDestroyOnLoad(this);

			Instance = this;
			Manager = manager;
			Logger = LogManager.GetForCurrentAssembly();

			Exporter = gameObject.AddComponent<Exporter>();

			RuntimePatcher.AutoPatch();
		}
	}
}