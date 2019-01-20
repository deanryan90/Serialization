using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot("Root")]
public class ObjectComponent : MonoBehaviour
{
    [XmlElement("name")]
    public string name
    { get; set; }

    [XmlElement("position")]
    public Vector2 position
    { get; set; }

    [XmlElement("Scale")]
    public Vector2 scale
    { get; set; }

    [XmlElement("Rotation")]
    public Quaternion rotation
    { get; set; }

    [XmlIgnore]
    public GameObject tileGameObject;

    [XmlIgnore]
    public static bool selected;

    [XmlIgnore]
    public static bool isMoveNew;

    [XmlElement("Type")]
    public string tileType
    { get; set; }

    public List<Tile> tiles = new List<Tile>();

    public ObjectComponent()
    { }

    static public void Move()
    { }

    static public void Deletion()
    { }

    static public void CollisionDetection()
    { }

    static public void Select()
    { }

    static public void Placement()
    { }
}