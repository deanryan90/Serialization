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

[System.Serializable]
public class objectPlacement : MonoBehaviour
{
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

    #region Class Variables

    public TileSelected tileSelected;

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

    public SaveLoad save = new SaveLoad();
    public SaveLoad load = new SaveLoad();

    public Tile tile;
    public Tile tileMovement;
    private int tileCount = 0;

    private int tileSelectionCount = 13;

    public List<ExtendedObject> sceneObjects = new List<ExtendedObject>();

    //Ints
    public int arrayStarter = 10;

    public int tileMovementChecker = 0;
    public int tileCounter = 0;

    public int tileAdder = 0;
    public int tilesDeleted = 0;

    private int moveTileSelected = 0;
    private bool alreadyHasMoveTileClicked = false;

    private RaycastHit2D hit;
    private RaycastHit2D hitDelete;
    private RaycastHit2D hitChangeTileRotation;

    [XmlElement("newPosition")]
    private RaycastHit2D hitMoveNew;

    //new Movement Tile function
    private bool isSelectedForMove = false;

    private CameraMovement cameraMovementClass;
    public bool isMoveNew = false;
    //public bool isCollider = true;

    #endregion Class Variables

    //string parse to int
    private int tileNumber;

    private string tileName = "";
    private Vector2 tempVec;

    private static string stringToEdit = "";
    private static string path = "";

    private string saveField = "";
    private string loadField = "";

    public bool isLoadedAlready = false;

    private float X;
    private float Y;
    private float Z = 0;
    private float testZ = 0;
    public GameObject testSpriteMousePos;
    public Sprite[] tileGuides;

    public float xTest = -1000;
    public float yTest = 10;

    private Vector3 mouselocation;// = (Camera.main.ScreenToWorldPoint(Input.mousePosition));

    private Vector3 mousePos;

    public GameObject testSnowyEdge;
    public Sprite[] testSnowyEdgeAll;

    public Sprite iceTest1;
    private Sprite iceTest2;
    public Sprite iceTest3;
    public Sprite iceTest4;

    public Sprite[] snowEdgeGrassTest;

    public Sprite[] testLoadSprites;

    private SpriteRenderer spriteRenderer;
    private int rotateCounter = 0;

    private void Awake()
    {
        testLoadSprites = Resources.LoadAll<Sprite>("sprites/snowEdge");// as Sprite [];
                                                                        //iceTest = UnityEngine.Sprite.Create(Resources.Load<Sprite>("ice"),);
                                                                        //iceTest1 = Resources.Load("ice.png",typeof(Sprite))as Sprite;
        iceTest2 = Resources.Load("sprites/ice", typeof(Sprite)) as Sprite;
        //testLoadSprites = Resources.Load("sprites/snowEdge",typeof(Sprite)) as Sprite [];
        //objMatToCompTo = Resources.LoadAll("(1)BlockPreFab", typeof(GameObject)).Cast<GameObject>().ToArray();
        //iceTest3 = Resources.Load("ice.png")as Sprite;
        //iceTest4 = Resources.Load("ice")as Sprite;
        //testSnowyEdgeAll[0] = null;
        //testSnowyEdge.GetComponent<SpriteRenderer>().sprite = testSnowyEdgeAll[0];
        //testSnowyEdgeAll[0] = Resources.Load("sprites/snowEdgeGrass",typeof(Sprite))as Sprite;
        //testSnowyEdgeAll[1] = Resources.Load("sprites/snowEdgeGrass",typeof(Sprite))as Sprite;
        //testSnowyEdgeAll[2] = Resources.Load("sprites/snowEdgeGrass2",typeof(Sprite))as Sprite;
        //testSnowyEdgeAll[3] = Resources.Load("sprites/snowEdgeGrass3",typeof(Sprite))as Sprite;
    }

    #region Start

    private void Start()
    {
        cameraMovementClass = GetComponent<CameraMovement>();
        tileMovement = new Tile();
        path = @"C:\Users\Dean\Desktop\Project Vanilla\ProjectVanilla V5\IsometricCameraTestRPG\Assets\Resources\Levels\" + saveField;
        print("Path " + stringToEdit);
    }

    #endregion Start

    #region Create Tile

    public void CreateTile(Vector2 screenPos, Vector2 worldPos, Tile tile, List<Tile> tiles, int tileCount, string type)
    {
        screenPos = Input.mousePosition;
        worldPos = GetComponent<Camera>().ScreenToWorldPoint(screenPos);
        tile = new Tile(screenPos, worldPos, "" + tileCount, type);
        tiles.Add(tile);
        tileCount++;
    }

    #endregion Create Tile

    #region GUI

    private void OnGUI()
    {
        //M turns off + N turns on Camera //R resets camera // Scrollbar zoom in/out // up,down,right,left
        if (CameraMovement.cameraSwitch == true)
        {
            GUI.Label(new Rect(10, 100, 200, 200), "M is Camera Switch");
            GUI.Label(new Rect(10, 120, 200, 200), "R resets camera position");
            GUI.Label(new Rect(10, 140, 200, 200), "Scrollbar zoom in/out");
        }

        GUI.Label(new Rect(500, 0, 300, 200), "Right Control Button Selects Tile to Move");
        GUI.Label(new Rect(500, 20, 200, 200), "Right Mouse Button to Place Tile");
        GUI.Label(new Rect(500, 40, 300, 200), "Left Mouse Button to Select Tile for Deletion");
        GUI.Label(new Rect(500, 60, 200, 200), "Delete Button Tile from Level");
        GUI.Label(new Rect(500, 80, 200, 200), "Tile Type " + tileSelected);
        saveField = "Level";
        saveField = GUI.TextField(new Rect(70, 5, 100, 20), saveField, 10);

        if (GUI.Button(new Rect(5, 5, 60, 20), "Save"))
        {
            path = path + saveField + ".xml";
            print(path);
            save.SerializeXML(tiles, path);
        }
        if (GUI.Button(new Rect(5, 30, 60, 20), "Load"))
        {
            if (isLoadedAlready == false)
            {
                load.LoadingNewLevel(saveField, tile, tileMovement, tileCount, tiles);
            }
            else
            {
                print("Level Already Loaded");
            }
        }
    }

    #endregion GUI

    // Update is called once per frame

    #region Update

    private void Update()
    {
        X = Input.mousePosition.x;
        Y = Input.mousePosition.y;
        Z = Input.mousePosition.z;

        testSpriteMousePos.transform.position = (Camera.main.ScreenToWorldPoint(new Vector3(X - 2, Y - 2, Z + 10)));

        //Switch Camera on/off
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraMovement.cameraSwitch = !CameraMovement.cameraSwitch;
        }
        //Reset Camera Position
        if (Input.GetKeyDown(KeyCode.R))
        {
            Camera.main.orthographicSize = 5;
            Camera.main.transform.position = new Vector3(258.32f, 275.03f, -10);
        }
        //Turn Camera Mode on/off
        if (Input.GetKeyDown(KeyCode.M))
        {
            cameraMovementClass.enabled = !cameraMovementClass.enabled;
        }

        #region Camera Button Movement

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraMovementClass.enabled == false) // forward
        {
            Camera.main.orthographicSize += 0.2f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraMovementClass.enabled == false) // forward
        {
            if (Camera.main.orthographicSize <= 1)
            {
                Camera.main.orthographicSize = 1;
            }
            else
            {
                Camera.main.orthographicSize -= 0.2f;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Camera.main.transform.position += Vector3.up * CameraMovement.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Camera.main.transform.position -= Vector3.up * CameraMovement.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Camera.main.transform.position += Vector3.right * CameraMovement.speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Camera.main.transform.position -= Vector3.right * CameraMovement.speed * Time.deltaTime;
        }

        #endregion Camera Button Movement

        #region Move Tile

        Tile.CheckTileMovement(tiles);
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            //Tile.SelectTileMovement(hitMoveNew,isMoveNew);
            hitMoveNew = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hitMoveNew.collider != null)
            {
                if (Tile.selected == false)
                {
                    hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.5f);
                    Tile.selected = true;
                    isMoveNew = true;
                }
                else if (Tile.selected == true)
                {
                    hitMoveNew.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    Tile.selected = false;
                    isMoveNew = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            hitChangeTileRotation = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            //testSnowyEdge.GetComponent<SpriteRenderer>().sprite = testLoadSprites[rotateCounter];
            if (hitChangeTileRotation.collider != null)
            {
                if (hitChangeTileRotation.collider.tag == "snowEdge")
                {
                    print("snowEdge Detected");
                    //hitChangeTileRotation.collider.GetComponent<SpriteRenderer>().sprite = iceTest2;
                }
                //hitChangeTileRotation.collider.gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);
                //hitChangeTileRotation.collider.GetComponent<SpriteRenderer>().sprite =
                //rotateCounter++;
                //if(rotateCounter == 4)
                //{
                //rotateCounter = 0;
                //}
            }
        }
        if (isMoveNew == true)
        {
            if (hitMoveNew.collider != null)
            {
                tileName = hitMoveNew.collider.gameObject.name;
                print("Tile Selected " + tileName);
                tileNumber = int.Parse(tileName);

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    hitMoveNew.collider.gameObject.transform.position += Vector3.up * 0.1f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    hitMoveNew.collider.gameObject.transform.position -= Vector3.up * 0.1f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    hitMoveNew.collider.gameObject.transform.position -= Vector3.right * 0.1f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    hitMoveNew.collider.gameObject.transform.position += Vector3.right * 0.1f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift))
                {
                    hitMoveNew.collider.gameObject.transform.position -= Vector3.right * 0.5f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift))
                {
                    hitMoveNew.collider.gameObject.transform.position += Vector3.right * 0.5f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift))
                {
                    hitMoveNew.collider.gameObject.transform.position += Vector3.up * 0.5f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
                if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift))
                {
                    hitMoveNew.collider.gameObject.transform.position -= Vector3.up * 0.5f;
                    tiles[tileNumber].position = hitMoveNew.collider.gameObject.transform.position;
                }
            }
        }

        #endregion Move Tile

        #region Select Tile

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition));
            Tile.SelectTileForDeletion(hitDelete);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Tile.DeleteTile(tiles);
        }

        #endregion Select Tile

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

        #region Tile Type Selection

        if (Input.GetKeyDown(KeyCode.T))
        {
            if ((int)tileSelected == tileSelectionCount)
            {
                tileSelected = 0;
            }
            else
            {
                tileSelected++;
            }
        }

        #endregion Tile Type Selection

        #region Tile Placement

        if (Input.GetMouseButtonDown(1))
        {
            switch (tileSelected)
            {
                case TileSelected.Grass:
                    print("grass");
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "grassTile");
                    tileCount++;
                    break;

                case TileSelected.Snow:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "snowTile");
                    tileCount++;
                    break;

                case TileSelected.SandyGrass:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "sandyGrassTile");
                    tileCount++;
                    break;

                case TileSelected.Rock:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "rockTile");
                    tileCount++;
                    break;

                case TileSelected.DeepWater:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "deepWaterTile");
                    tileCount++;
                    break;

                case TileSelected.ShallowWater:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "shallowWaterTile");
                    tileCount++;
                    break;

                case TileSelected.Sand:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "SandTile");
                    tileCount++;
                    break;

                case TileSelected.Dirt:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "dirtTile");
                    break;

                case TileSelected.RockyWater:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "rockyWaterTile");
                    tileCount++;
                    break;

                case TileSelected.SnowEdgeGrass:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "snowEdgeGrassTile");
                    tileCount++;
                    break;

                case TileSelected.SnowGrass:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "snowGrassTile");
                    tileCount++;
                    break;

                case TileSelected.Ice:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "iceTile");
                    tileCount++;
                    break;

                case TileSelected.RockyGrass:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "rockyGrassTile");
                    tileCount++;
                    break;

                case TileSelected.Swamp:
                    CreateTile(screenPos, worldPos, tile, tiles, tileCount, "swampTile");
                    tileCount++;
                    break;
            }
        }

        #endregion Tile Placement
    }

    #endregion Update

    public void tileSelectedGuide()
    {
    }
}