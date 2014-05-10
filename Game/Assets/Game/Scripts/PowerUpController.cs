using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour 
{
	public int type;
	
	void Start () 
	{
		Random.Range(1, 4);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerController>().GetPowerUp(type);
			Destroy(this.gameObject); //Bad, use object pool
		}
	}
}
