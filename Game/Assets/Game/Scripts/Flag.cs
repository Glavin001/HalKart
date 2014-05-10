using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			other.GetComponent<PlayerController>().Tag();
			Debug.Log("Crash into player");
			Destroy(gameObject);
		}
	}
}
