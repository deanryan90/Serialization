using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

[XmlRoot("Root")]
public class ExtendedObject 
{
	[XmlElement("name")]
	public string name
	{get;set;}
	[XmlElement("position")]
	public Vector2 position
	{  get;
		set;
	}
	[XmlElement("Scale")]
	public Vector2 scale
	{get;set;}
	[XmlElement("Rotation")]
	public Quaternion rotation
	{get;set;}
	public Vector2 test
	{get;set;}
	//[XmlAttribute("GameObject")]
	[XmlIgnore]
	public GameObject tileGameObject;
	[XmlIgnore]
	public static bool selected;
	[XmlElement("Type")]
	public string tileType
	{get;set;}
	
	
	
	public ExtendedObject()
	{
		selected = false;
	}
	public ExtendedObject(Vector2 screenPos,Vector2 _position,string _name,string _tileType)
	{
		test = this.test;
		tileType = _tileType;
		position = _position;
		rotation = this.rotation;
		tileGameObject  = GameObject.Instantiate(Resources.Load("GameObjects/" + _tileType),_position,rotation) as GameObject;
		tileGameObject.name = _name;
		tileGameObject.GetComponent<Renderer>().sortingLayerName =  "Midground";
	}
	public void MoveTile(Vector2 _newPosition,RaycastHit2D hitMoveNew,List<Tile> tiles)
	{
		_newPosition = hitMoveNew.collider.gameObject.transform.position;
		_newPosition += Vector2.up * 0.1f;
		for(int i = 0;i < 1;i++)
		{
			tiles[i].position = hitMoveNew.collider.gameObject.transform.position;
		}
	}
	
	
}