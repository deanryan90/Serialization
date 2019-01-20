using UnityEngine;
using System.Collections;

public class TileManagement : MonoBehaviour {

	#region Tile Selected Enums
	public enum TileSelected
	{
		Grass,
		Snow,
		SandyGrass,
		Rock,
		DeepWater,
		ShallowWater,
		Sand,
		Dirt,
		RockyWater,
		SnowEdgeGrass,
		SnowGrass,
		Ice,
		RockyGrass,
		Swamp
	}
	#endregion

	public TileSelected tileSelected;
	public Sprite [] tileGuides;
	public Sprite [] testLoadSprites;

	public bool isMoveNew = false;

	//RaycastHit2D hit;
	//RaycastHit2D hitDelete;
	//RaycastHit2D hitChangeTileRotation;
	//RaycastHit2D hitMoveNew;
	RaycastHit2D hitMoveNew ;//= Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
	//public bool isMoveNew = false;

	void Awake()
	{
		testLoadSprites = Resources.LoadAll<Sprite>("sprites/snowEdge");
	}
	// Use this for initialization
	void Start () 
	{
		hitMoveNew = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TileMovement(RaycastHit2D hitMoveNew,bool isMoveNew)
	{
		hitMoveNew = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		
		if(hitMoveNew.collider != null)
		{
			if (Tile.selected == false)
			{
				hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0,0,0,0.5f);	
				Tile.selected = true;
				isMoveNew = true;
			}
			else if (Tile.selected == true)
			{
				hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
				Tile.selected = false;
				isMoveNew=false;
			}
		}
	}
}
