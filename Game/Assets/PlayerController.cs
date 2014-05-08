using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public bool isTagged = false;
	[Range(1, 4)]
	public int player = 1;
	private float lastUpdate = 0f;
	
	void Start () 
	{
	
	}

	void Update () 
	{
		if(isTagged)
		{
			if(Time.time - lastUpdate >= 1f)
			{
				GameController.controller.playerTagTime[player-1]++;
				lastUpdate = Time.time;
			}
		}
	}

	public void Tag()
	{
		isTagged = true;
		lastUpdate = Time.time;
	}
}
