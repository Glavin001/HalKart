using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public bool isTagged = false;		//is the player it
	private float lastUpdate = 0f; 		//for keeping track of how long the player has been it for

	[Range(1, 4)]
	public int player = 1; 				//what player # is this script

	private bool hasPowerUp; 			//does the player have a power up
	private int powerUpType; 			//if so what kind of power up
	public float powerUpLength; 		//How long to power the player up for

	public float jumpForce; 			//The force to apply when the player jumps

	private float speedUpEndTime; 		//When the player will go back to normal speed
	private bool spedUp; 				//Is the player currently sped up
	public float normalSpeed; 			//The normal speed the player will travel at
	public float boostSpeed; 			//The boost speed the player will travel at

	public GameObject bomb; 			//A reference to the prefab of the bomb to explode

	private bool invincible; 			//is the player invincible
	private float invincibleEndTime; 	//When invincibility ends

	private CarController carController;// A reference to the car controller script


	void Start ()
	{
		carController = this.gameObject.GetComponent<CarController>();
		hasPowerUp = false;
		invincible = false;
		spedUp = false;
	}

	void Update () 
	{
		//If tagged, update Game Controller
		if(!GameController.controller.GameWon && isTagged)
		{
			if(Time.time - lastUpdate >= 1f)
			{
				GameController.controller.playerTagTime[player-1]++;
				lastUpdate = Time.time;
			}
		}

		//If sped up, check to see if time to go back to normal speed
		if (spedUp)
		{
			if (Time.time > speedUpEndTime)
			{
				carController.MaxSpeed = normalSpeed;
				spedUp = false;
			}
		}

		//If invincible, check to see if time to go back to normal
		if (invincible)
		{
			if (Time.time > invincibleEndTime)
			{
				invincible = false;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (isTagged && other.tag == "Player" && !invincible)
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

	#region powerUps

	public void GetPowerUp(int type)
	{
		if (!hasPowerUp)
		{
			powerUpType = type;
			hasPowerUp = true;
		}
	}

	public void UsePowerUp()
	{
		if(hasPowerUp)
		{
			switch(powerUpType)
			{
			case 1:
				SpeedUp();
				break;
			case 2:
				GoInvincible();
				break;
			case 3:
				Jump();
				break;
			case 4:
				DropBomb();
				break;
			}
			hasPowerUp = false;
		}
	}

	private void SpeedUp()
	{
		speedUpEndTime = Time.time + powerUpLength;
		carController.MaxSpeed = boostSpeed;
		spedUp = true;
	}

	private void GoInvincible()
	{
		invincibleEndTime = Time.time + powerUpLength;
		invincible = true;
	}

	private void Jump()
	{
		rigidbody.AddForce(Vector3.up * jumpForce);
	}

	private void DropBomb()
	{
		Instantiate(bomb, transform.position, transform.rotation);
	}

	#endregion
}
