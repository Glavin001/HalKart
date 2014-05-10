using UnityEngine;
using System.Collections;

public class Resolution : MonoBehaviour 
{
	void Start () 
	{
		Screen.SetResolution (1280, 720, true, 60);
	}
}
