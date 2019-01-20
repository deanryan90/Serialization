using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text;
using System.Linq;

[XmlRoot("Root")]
public class Tile
{
    //public static TileSelected tileSelected;
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

    public Vector2 test
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

    public static bool isMoveNewFromTileClass = false;

    public static int rotateCounter;

    public List<Tile> tiles = new List<Tile>();

    public Tile()
    {
        selected = false;
        isMoveNew = false;
        rotateCounter = 0;
    }

    public Tile(Vector2 screenPos, Vector2 _position, string _name, string _tileType)
    {
        test = this.test;
        tileType = _tileType;
        position = _position;
        rotation = this.rotation;
        tileGameObject = GameObject.Instantiate(Resources.Load("GameObjects/" + _tileType), _position, rotation) as GameObject;
        tileGameObject.name = _name;
        //tileGameObject.GetComponent<Renderer>().sortingLayerName =  "Midground";
    }

    public void MoveTile(Vector2 _newPosition, RaycastHit2D hitMoveNew, List<Tile> tiles)
    {
        _newPosition = hitMoveNew.collider.gameObject.transform.position;
        _newPosition += Vector2.up * 0.1f;
        for (int i = 0; i < 1; i++)
        {
            tiles[i].position = hitMoveNew.collider.gameObject.transform.position;
        }
    }

    static public void CheckTileMovement(List<Tile> tiles)
    {
        for (int j = tiles.Count - 1; j >= 0; j--)
        {
            if (tiles[j].tileGameObject.GetComponent<Renderer>().material.color == new Color(0, 0, 0, 0.5f))
            {
                selected = true;
                MonoBehaviour.print("Tile is Already Selected");
            }
        }
    }

    static public void SelectTileMovement(RaycastHit2D hitMoveNew, bool isMoveNew, bool selected, string tileName, int tileNumber, List<Tile> tiles)
    {
        hitMoveNew = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hitMoveNew.collider != null)
        {
            if (selected == false)
            {
                hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.5f);
                selected = true;
                isMoveNew = true;
                MonoBehaviour.print("Selected " + hitMoveNew.collider.name);
                MonoBehaviour.print("Selected " + selected);
            }
            else if (selected == true)
            {
                hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                selected = false;
                isMoveNew = false;
                MonoBehaviour.print("Unselected " + hitMoveNew.collider.name);
                MonoBehaviour.print("Selected " + selected);
            }
        }
    }

    static public void DeleteTile(List<Tile> tiles)
    {
        for (int j = tiles.Count - 1; j >= 0; j--)
        {
            if (tiles[j].tileGameObject.GetComponent<Renderer>().material.color == Color.red)
            {
                MonoBehaviour.DestroyObject(tiles[j].tileGameObject);
                tiles.Remove(tiles[j]);
                MonoBehaviour.print("" + tiles.Count);
            }
        }
    }

    static public void RotateTile(RaycastHit2D hit, Sprite[] sprites)
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (rotateCounter == 4)
            {
                rotateCounter = 0;
            }
            if (hit.collider.tag == "snowEdge")
            {
                MonoBehaviour.print("snowEdge Detected");
                hit.collider.GetComponent<SpriteRenderer>().sprite = sprites[rotateCounter];
                rotateCounter++;
                MonoBehaviour.print(rotateCounter);
            }
        }
    }

    static public void SelectTileForDeletion(RaycastHit2D hitDelete)
    {
        //make sure you have a camera in the scene tagged as 'MainCamera'
        hitDelete = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hitDelete.collider != null)
        {
            if (selected == false)
            {
                MonoBehaviour.print(hitDelete.collider.gameObject.name + " Clicked");
                hitDelete.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                selected = true;
                MonoBehaviour.print(hitDelete.collider.gameObject);
            }
            else if (selected == true)
            {
                hitDelete.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                selected = false;
            }
        }
    }

    static public void TileGuideSelecter(GameObject testSpriteMousePos, Sprite[] tileGuides, int tileSelected)
    {
        if ((int)tileSelected == 0)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[0];
        }
        else if ((int)tileSelected == 1)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[1];
        }
        else if ((int)tileSelected == 2)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[2];
        }
        else if ((int)tileSelected == 3)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[3];
        }
        else if ((int)tileSelected == 4)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[4];
        }
        else if ((int)tileSelected == 5)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[5];
        }
        else if ((int)tileSelected == 6)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[6];
        }
        else if ((int)tileSelected == 7)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[7];
        }
        else if ((int)tileSelected == 8)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[8];
        }
        else if ((int)tileSelected == 9)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[9];
        }
        else if ((int)tileSelected == 10)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[10];
        }
        else if ((int)tileSelected == 11)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[11];
        }
        else if ((int)tileSelected == 12)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[12];
        }
        else if ((int)tileSelected == 13)
        {
            testSpriteMousePos.GetComponent<SpriteRenderer>().sprite = tileGuides[13];
        }
    }

    static public void CreateTile(Vector2 screenPos, Vector2 worldPos, Tile tile, List<Tile> tiles, int tileCount, string type)
    {
        screenPos = Input.mousePosition;
        worldPos = Camera.main.gameObject.GetComponent<Camera>().ScreenToWorldPoint(screenPos);
        tile = new Tile(screenPos, worldPos, "" + tileCount, type);
        tiles.Add(tile);
    }
}