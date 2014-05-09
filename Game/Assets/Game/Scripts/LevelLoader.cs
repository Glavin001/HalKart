using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
	[Range(0, 60)]
	public int delayTime = 0;
	public string nextScene = "SceneNameAsText";

	void Update () 
	{
		if (Time.timeSinceLevelLoad > delayTime) 
			Application.LoadLevel(nextScene);
	}
}
