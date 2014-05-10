using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour 
{
	public Color[] colors;

	private int targetPlayer = 0;
	private Transform target = null;

	public Transform thisPlayer;
	public Transform player1;
	public Transform player2;
	public Transform player3;
	public Transform player4;

	void Start()
	{
		renderer.material.color = colors [0];
	}

	void Update () 
	{
		//Update target
		if (targetPlayer != GameController.controller.PlayerIt)
		{
			targetPlayer = GameController.controller.PlayerIt;

			switch(targetPlayer)
			{
			case 1:
				target = player1;
				break;
			case 2:
				target = player2;
				break;
			case 3:
				target = player3;
				break;
			case 4:
				target = player4;
				break;
			default:
				target = null;
				break;
			}
		}

		//Look at target
		if (target != null) 
		{
			transform.LookAt (target, worldUp: Vector3.left);
		}

		// 
		//gameObject.transform.parent
		
		if (target == thisPlayer) {
			renderer.material.color = colors [1];
		} else {
			renderer.material.color = colors[0];
		}
	}
}
