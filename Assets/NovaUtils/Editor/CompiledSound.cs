using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {

	[InitializeOnLoad]
	public static class CompiledSound {

		const string CompileStatePrefsKey = "CompileIndicator.WasCompiling";
		static readonly AudioClip CompiledClip;

		static CompiledSound() {
			EditorApplication.update = OnUpdate;
			CompiledClip = Resources.Load<AudioClip>("Compiled");
		}

		static void OnUpdate() {
			var wasCompiling = EditorPrefs.GetBool(CompileStatePrefsKey);
			var isCompiling = EditorApplication.isCompiling;
			if (wasCompiling && !isCompiling) {
				OnDoneCompiling();
			}
			EditorPrefs.SetBool(CompileStatePrefsKey, isCompiling);
		}

		static void OnDoneCompiling() {
			PlayClip(CompiledClip);
		}

		static void PlayClip(AudioClip clip) {
			Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
			Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
			MethodInfo method = audioUtilClass.GetMethod(
				"PlayClip",
				BindingFlags.Static | BindingFlags.Public,
				null,
				new []{typeof(AudioClip)},
				null
			);
			method.Invoke(null, new object[]{clip});
		}
	}
}