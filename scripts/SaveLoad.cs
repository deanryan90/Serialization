using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

public class SaveLoad 
{
	#region Save and Load Functions

	public SaveLoad()
	{
	}

	public void SerializeXML(List<Tile> tiles,string path)
	{
		//print(path);
		XmlSerializer serializer = new XmlSerializer(typeof(List<Tile>));
		TextWriter textWriter = new StreamWriter(path);
		serializer.Serialize(textWriter,tiles);
		textWriter.Close();
		//print ("Saved To XML");
	}
	public void LoadingNewLevel(string saveField,Tile tile,Tile tileMovement,int tileCount,List<Tile> tiles)
	{
		//isLoadedAlready = true;
		//print ("Load From XML");
		saveField = saveField + ".xml";
		string filePath = @"C:\Users\Dean\Desktop\ProjectVanilla V5\IsometricCameraTestRPG\Assets\Resources\Levels\"+saveField;
		//string filePath = path +stringToEdit;
		float x  = 0;
		float y = 0;
		
		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);
		XmlNodeList transformList = doc.GetElementsByTagName("Tile");
		
		foreach(XmlNode transformInfo in transformList)
		{
			string type = transformInfo["Type"].InnerText;
			
			XmlNodeList transformContent = transformInfo.ChildNodes;
			foreach(XmlNode transformItems in transformContent)
			{
				if(transformItems.Name == "position")
				{ 
					XmlNodeList newTransformList = transformItems.ChildNodes;
					
					foreach(XmlNode newTransformItems in newTransformList )
					{
						if(newTransformItems.Name == "x")
						{
							x = float.Parse(newTransformItems.InnerText);
							//print ("X Value " + x);
						}
						if(newTransformItems.Name == "y")
						{
							y = float.Parse(newTransformItems.InnerText);
							//print ("Y Value " + y);
						}
					}	
					
					tile = new Tile(Vector2.zero,new Vector2(x,y),"" + tileCount,type);
					tiles.Add(tile);
					tileCount++;
				}
			}
		}
	}
	#endregion

}
