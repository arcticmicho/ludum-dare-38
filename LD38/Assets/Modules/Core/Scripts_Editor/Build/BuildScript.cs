using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class BuildScript
{
	private static string[] EnabledLevels()
	{
		List<string> scenes = new List<string>();

		foreach (var scene in EditorBuildSettings.scenes) 
		{
			if(scene.enabled)
				scenes.Add (scene.path);
		}
			
		return scenes.ToArray();
	}

	[MenuItem("Build/Build iOS")]
	public static void BuildIOS() 
	{
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = EnabledLevels ();
		buildPlayerOptions.locationPathName = "Build/ios";
		buildPlayerOptions.target = BuildTarget.iOS;
		buildPlayerOptions.options = BuildOptions.None;

		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

}