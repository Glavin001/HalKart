using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour 
{
	//Static reference to the GameController class
	public static GameController controller;

	public int[] playerTagTime;
	public int targetTime;
	public int winner = 0;

	
	#region Properties
	private bool gameWon = false;
	public bool GameWon
	{
		get{return gameWon;}
		set{gameWon = value;}
	}

	private int playerIt = 0;
	public int PlayerIt
	{
		get{return playerIt;}
		set{playerIt = value;}
	}
	#endregion
	
	void Awake () 
	{
		//if controller is not set, set it to this one and allow it to persist
		if (controller == null)
		{
			DontDestroyOnLoad(gameObject);
			controller = this;
		}
		//else if control exists and it isn't this instance, destroy this instance
		else if(controller != this)
		{
			Debug.Log ("Game control already exists, deleting this new one");
			Destroy (gameObject);
		}
	}
	
	void Start()
	{
		playerTagTime = new int[4];
		for(int i=0; i<playerTagTime.Length; i++)
		{
			playerTagTime[i] = 0;
		}
	}
	
	void Update()
	{
		if(!gameWon)
		{
			if (playerTagTime[0] >= targetTime)
			{
				winner = 1;
				gameWon = true;
			}
			if (playerTagTime[1] >= targetTime)
			{
				winner = 2;
				gameWon = true;
			}
			
			if(playerTagTime[2] >= targetTime)
			{
				winner = 3;
				gameWon = true;
			}
			
			if(playerTagTime[3] >= targetTime)
			{
				winner = 4;
				gameWon = true;
			}
		}
	}
	
	void OnGUI()
	{
		if (gameWon)
		{
			//Set the Size of the centered popoup here
			int groupWidth = 250;
			int groupHeight = 100;
			int groupX = (Screen.width - groupWidth) / 2;
			int groupY = (Screen.height - groupWidth) /2;
			// Make a group on the center of the screen
			GUI.BeginGroup (new Rect (groupX, groupY, groupWidth, groupHeight));
			GUI.Box (new Rect (0, 0, groupWidth, groupHeight), "\n\nPlayer " + winner + " Wins!\nPress O to play again!");

			GUI.EndGroup ();
		}
	}
	
	#region SaveLoad
	public void Save()
	{
		//Create binary formatter
		BinaryFormatter bf = new BinaryFormatter();
		
		//Create a file
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		//Create a PlayerData class and set values
		PlayerData data = new PlayerData();
		//data. = ;
		//data. = ;
		
		//Take the seriazable PlayerData class that data is, and write it to file
		bf.Serialize(file, data);
		file.Close ();
	}
	
	public void Load()
	{
		//If the file exists
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			//Create a binary formatter
			BinaryFormatter bf = new BinaryFormatter();
			//Open the file
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			//Create a PlayerData file by typecasting what is deserialized from the binary formatter reading from the file
			PlayerData data = (PlayerData)bf.Deserialize(file);
			//Close the file
			file.Close();
			//Set the loaded data
			 //= data.;
			 //= data.;
		}
	}
	#endregion
}

[Serializable] //This tells unity that this is a data container that can be written to a file
class PlayerData
{
	//Use private and get/set, this is quick
	public int someSavedValue;
	public int someSavedValue2;
}