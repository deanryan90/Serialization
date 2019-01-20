using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    #region Class Variables

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

    #endregion Tile Selected Enums

    private Vector2 screenPos;
    private Vector2 worldPos;

    //Bools
    public bool moveSelector = false;

    public static bool isCanvasOn = true;
    public bool isMove = false;
    public static bool isMoveClicked = false;
    public bool isMoveTileSelected = false;
    public bool tileRotate = false;

    //public bool cameraSwitch = true;

    //--New Serialized Item--//
    public List<Tile> tiles = new List<Tile>();

    private static string stringToEdit = "";
    private static string path = "";

    private string saveField = "";
    private string loadField = "";

    public Tile tile;
    public Tile tileMovement;
    private int tileCount = 0;

    public TileSelected tileSelected;

    private int tileSelectionCount = 13;

    public List<ExtendedObject> sceneObjects = new List<ExtendedObject>();

    //Ints
    public int arrayStarter = 10;

    public int tileMovementChecker = 0;
    public int tileCounter = 0;

    public int tileAdder = 0;
    public int tilesDeleted = 0;

    private RaycastHit2D hitDelete;
    private RaycastHit2D hitChangeTileRotation;
    private RaycastHit2D hitMoveNew;

    private bool isSelectedForMove = false;

    private CameraMovement cameraMovementClass;

    //string parse to int
    private int tileNumber;

    private string tileName = "";
    private Vector2 tempVec;

    private float X;
    private float Y;
    private float Z = 0;
    private float testZ = 0;
    private GameObject testSpriteMousePos;
    public Sprite[] tileGuides;

    private float xTest = -1000;
    private float yTest = 10;

    private Vector3 mouselocation;// = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

    public SaveLoad save = new SaveLoad();
    public SaveLoad load = new SaveLoad();
    private Vector3 mousePos;

    public bool isLoadedAlready = false;

    public Sprite[] testLoadSprites;

    private SpriteRenderer spriteRenderer;
    //public int rotateCounter = 0;

    #endregion Class Variables

    private void OnGUI()
    {
        GUI.Label(new Rect(500, 0, 300, 200), "Right Control Button Selects Tile to Move");
        GUI.Label(new Rect(500, 20, 200, 200), "Right Mouse Button to Place Tile");
        GUI.Label(new Rect(500, 40, 300, 200), "Left Mouse Button to Select Tile for Deletion");
        GUI.Label(new Rect(500, 60, 200, 200), "Delete Button Tile from Level");
        //GUI.Label(new Rect(500,80,200,200),"Tile Type "+tileSelected);

        saveField = GUI.TextField(new Rect(70, 5, 100, 20), saveField, 10);

        if (GUI.Button(new Rect(5, 5, 60, 20), "Save"))
        {
            path = path + saveField + ".xml";
            print(path);
            save.SerializeXML(tiles, path);
        }
        if (GUI.Button(new Rect(5, 30, 60, 20), "Load"))
        {
            load.LoadingNewLevel(saveField, tile, tileMovement, tileCount, tiles);
        }
    }

    private void Awake()
    {
        hitMoveNew = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        testSpriteMousePos = GameObject.Instantiate(Resources.Load("GameObjects/tileGuide")) as GameObject;
        testLoadSprites = Resources.LoadAll<Sprite>("sprites/snowEdge");
    }

    // Use this for initialization
    private void Start()
    {
        tileMovement = new Tile();
        tile = new Tile();
        path = @"C:\Users\Dean\Desktop\ProjectVanilla V5\IsometricCameraTestRPG\Assets\Resources\Levels\" + saveField;
        print("Path " + stringToEdit);
    }

    // Update is called once per frame
    private void Update()
    {
        X = Input.mousePosition.x;
        Y = Input.mousePosition.y;
        Z = Input.mousePosition.z;
        testSpriteMousePos.transform.position = (Camera.main.ScreenToWorldPoint(new Vector3(X - 2, Y - 2, Z + 10)));

        if (Input.GetMouseButtonDown(1))
        {
            TilePlacement();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Tile.SelectTileForDeletion(hitDelete);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TileSelecter();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Tile.RotateTile(hitChangeTileRotation, testLoadSprites);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Tile.DeleteTile(tiles);
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            SelectTileForMoving();
        }
        CheckIsMoveNew();
        Tile.TileGuideSelecter(testSpriteMousePos, tileGuides, (int)tileSelected);
    }

    public void CheckIsMoveNew()
    {
        if (Tile.isMoveNew == true)
        {
            if (hitMoveNew.collider != null)
            {
                tileName = hitMoveNew.collider.gameObject.name;
                print("Tile Selected " + tileName);
                tileNumber = int.Parse(tileName);

                MovementKeys(KeyCode.UpArrow, Vector3.up);
                MovementKeys(KeyCode.DownArrow, -Vector3.up);
                MovementKeys(KeyCode.LeftArrow, -Vector3.right);
                MovementKeys(KeyCode.RightArrow, Vector3.right);
            }
        }
    }

    public void MovementKeys(KeyCode key, Vector3 direction)
    {
        if (Input.GetKey(key))
        {
            hitMoveNew.collider.gameObject.transform.position += direction * 0.1f;
            tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
        }
    }

    public void SelectTileForMoving()
    {
        hitMoveNew = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hitMoveNew.collider != null)
        {
            if (Tile.selected == false)
            {
                hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.5f);
                Tile.selected = true;
                Tile.isMoveNew = true;
            }
            else if (Tile.selected == true)
            {
                hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                Tile.selected = false;
                Tile.isMoveNew = false;
            }
        }
    }

    public void TileSelecter()
    {
        MonoBehaviour.print("Tile Selected " + tileSelected);
        if ((int)tileSelected == tileSelectionCount)
        {
            tileSelected = 0;
        }
        else
        {
            tileSelected++;
        }
    }

    public void TilePlacement()
    {
        switch (tileSelected)
        {
            case TileSelected.Grass:
                print("grass");
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "grassTile");
                tileCount++;
                break;

            case TileSelected.Snow:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "snowTile");
                tileCount++;
                break;

            case TileSelected.SandyGrass:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "sandyGrassTile");
                tileCount++;
                break;

            case TileSelected.Rock:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "rockTile");
                tileCount++;
                break;

            case TileSelected.DeepWater:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "deepWaterTile");
                tileCount++;
                break;

            case TileSelected.ShallowWater:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "shallowWaterTile");
                tileCount++;
                break;

            case TileSelected.Sand:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "SandTile");
                tileCount++;
                break;

            case TileSelected.Dirt:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "dirtTile");
                break;

            case TileSelected.RockyWater:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "rockyWaterTile");
                tileCount++;
                break;

            case TileSelected.SnowEdgeGrass:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "snowEdgeGrassTile");
                tileCount++;
                break;

            case TileSelected.SnowGrass:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "snowGrassTile");
                tileCount++;
                break;

            case TileSelected.Ice:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "iceTile");
                tileCount++;
                break;

            case TileSelected.RockyGrass:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "rockyGrassTile");
                tileCount++;
                break;

            case TileSelected.Swamp:
                Tile.CreateTile(screenPos, worldPos, tile, tiles, tileCount, "swampTile");
                tileCount++;
                break;
        }
    }
}