using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

[System.Serializable]
public class UserInterface : MonoBehaviour {


	
	//string saveField = "";
	//string loadField = "";

	//
	public SaveLoad save = new SaveLoad();
	public SaveLoad load = new SaveLoad();

	public List<Tile> tiles = new List<Tile>();

	// Use this for initialization
	void Start () 
	{

	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.S))
		{
			save.SerializeXML(tiles,@"C:\Users\Dean\Desktop\ProjectVanilla V5\IsometricCameraTestRPG\Assets\Resources\Levels\testLevel.xml");
		}
	}

}
