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
		Screen.SetResolution (1280, 720, true, 60);
	}

	void Update () 
	{
		if(!GameController.controller.GameWon && isTagged)
		{
			if(Time.time - lastUpdate >= 1f)
			{
				GameController.controller.playerTagTime[player-1]++;
				lastUpdate = Time.time;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (isTagged && other.tag == "Player")
		{
			GameController.controller.PlayerIt = player;
			other.GetComponent<PlayerController>().Tag();
			isTagged = false;
		}
	}

	public void Tag()
	{
		isTagged = true;
		lastUpdate = Time.time;
	}
}
