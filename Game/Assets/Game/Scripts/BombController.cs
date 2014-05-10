using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour 
{
	public GameObject explosion;
	public float fuseLength = 3f;
	private float explodeTime;

	void Start () 
	{
		explodeTime = Time.time + fuseLength;
	}

	void Update () 
	{
		if (Time.time > explodeTime)
		{
			Explode();
		}
	}

	void Explode() 
	{
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy(this.gameObject); //Bad, use object pool
	}
}