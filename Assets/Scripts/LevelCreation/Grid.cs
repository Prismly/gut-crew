using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> list = new List<T>();
        public T this[int key]
        {
            get
            {
                return list[key];
            }
            set
            {
                list[key] = value;
            }
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public void Add(T val)
        {
            list.Add(val);
        }
    }

    
    //only used for setup
    private List<ListWrapper<Tile>> setupTilesList = new List<ListWrapper<Tile>>();
    [SerializeField, Tooltip("All tiles in this grid")] protected List<Tile> tiles = new List<Tile>();
    public GameObject baseTile
    {
        get { return tilePrefab; }
    }

    [Header("base grid creation")]
    [SerializeField, Tooltip("The base tile prefab to use for this Grid")] protected GameObject tilePrefab;
    [SerializeField, Tooltip("the position to start spawning the tile grid from")] private Vector3 tileStartPos = Vector3.zero;
    [SerializeField, Tooltip("The width of the grid to create")] private int gridWidth = 1;
    [SerializeField, Tooltip("The height of the grid to create")] private int gridHeight = 1;
    [SerializeField, Tooltip("Mark as true to be able to overwrite an existing grid")] private bool overwriteGrid = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTile(Tile newTile)
    {
        if (newTile)
        {
            tiles.Add(newTile);
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    public void RemoveTile(Tile removeTile)
    {
        if (tiles.Contains(removeTile))
        {
            tiles.Remove(removeTile);
        }
    }

    public void CreateNewGrid()
    {
        SetupGrid(gridWidth, gridHeight, overwriteGrid);
        overwriteGrid = false;
    }

    public void SetupGrid(int xSize, int ySize, bool overridePrevVals)
    {
        Vector2 tileSize = tilePrefab.GetComponent<Tile>().collider.size;
        if (tiles != null)
        {
            if (!overridePrevVals)
            {
                Debug.LogWarning("Trying to override previous values through inspector");
                return;
            }
            else
            {
                ClearLevel();
            }
        }

        setupTilesList = new List<ListWrapper<Tile>>();
        Debug.Log("creating tiles with start at: " + tileStartPos + ", and size of " + tileSize);
        for (int i = 0; i < xSize; i++)
        {
            setupTilesList.Add(new ListWrapper<Tile>());
            for (int j = 0; j < ySize; j++)
            {

                GameObject newTileObj = Instantiate(tilePrefab, transform);
                newTileObj.transform.localPosition = new Vector3(tileStartPos.x + tileSize.x * i, tileStartPos.y + tileSize.y * j, newTileObj.transform.localPosition.z);
                newTileObj.name = "Tile " + i + ", " + j;
                Tile newTile = newTileObj.GetComponent<Tile>();
                if (!newTile)
                {
                    newTile = newTileObj.AddComponent<Tile>();
                }
                newTile.grid = this;
                setupTilesList[i].Add(newTile);
                tiles.Add(newTile);
            }
        }
        Debug.Log("completed list");
        LinkTileList();
    }

    public void ClearLevel()
    {
        foreach(Tile tile in tiles)
        {
            if (tile)
            {
                Destroy(tile.gameObject);
            }
        }
        tiles = new List<Tile>();
    }

    public void LinkTileList()
    {
        // Debug.Log("linking tiles: " + tilesList.Count + ", " + tilesList[0].Count);
        for (int i = 0; i < setupTilesList.Count; i++)
        {
            for (int j = 0; j < setupTilesList[i].Count; j++)
            {

                if (i > 0)
                {
                    //Debug.Log("top: "+ i +", " + j);
                    setupTilesList[i][j].left = setupTilesList[i - 1][j];
                }
                if (i < setupTilesList.Count - 1)
                {
                    //Debug.Log("bottom: "+ i +", " + j);
                    setupTilesList[i][j].right = setupTilesList[i + 1][j];
                }

                if (j > 0)
                {
                    //Debug.Log("left: "+ i +", " + j);
                    setupTilesList[i][j].down = setupTilesList[i][j - 1];
                }
                if (j < setupTilesList[i].Count - 1)
                {
                    //Debug.Log("right: "+ i +", " + j);
                    setupTilesList[i][j].up = setupTilesList[i][j + 1];

                }
                
            }
        }
        setupTilesList.Clear();
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
}
