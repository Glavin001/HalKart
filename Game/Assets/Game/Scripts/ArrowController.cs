using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour 
{
	private int targetPlayer = 0;
	private Transform target = null;

	public Transform player1;
	public Transform player2;
	public Transform player3;
	public Transform player4;

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
			transform.LookAt(target);
	}
}
