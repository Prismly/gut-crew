using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField, Tooltip("box collider component")]
    protected BoxCollider colliderComponent;
    public BoxCollider collider
    {
        get {
            if (!colliderComponent)
            {
                colliderComponent = GetComponent<BoxCollider>();
            }
            return colliderComponent; 
        }
    }

    [Header("Sprite rendering")]

    [SerializeField, Tooltip("sprite renderer component")]
    protected SpriteRenderer rendererComponent;
    public SpriteRenderer renderer
    {
        get
        {
            if (!rendererComponent)
            {
                rendererComponent = GetComponent<SpriteRenderer>();
            }
            return rendererComponent;
        }
    }

    [SerializeField, Tooltip("default color")]
    protected Color defaultColor = Color.white;

    [SerializeField, Tooltip("the highlighted color")]
    protected Color highlightColor = Color.cyan;

    [SerializeField, Tooltip("the error color")]
    protected Color errorColor = Color.red;

    [Header("Adjacent tiles")]

    [SerializeField, Tooltip("The tile above this tile")]
    protected Tile upTile;

    public Tile up 
    { 
        get { return upTile; }
        set { upTile = value; }
    }

    [SerializeField, Tooltip("The tile below this tile")]
    protected Tile downTile;

    public Tile down
    {
        get { return downTile; }
        set { downTile = value; }
    }

    [SerializeField, Tooltip("The tile left of this tile")]
    protected Tile leftTile;

    public Tile left
    {
        get { return leftTile; }
        set { leftTile = value; }
    }

    [SerializeField, Tooltip("The tile right of this tile")]
    protected Tile rightTile;

    public Tile right
    {
        get { return rightTile; }
        set { rightTile = value; }
    }

    [SerializeField, Tooltip("The tile this is attached to")]
    protected Tile attachedTile;
    public Tile attached 
    { 
        get { return attachedTile; }
    }
    [Header("Owning objects")]
    [SerializeField, Tooltip("the owning grid")] protected Grid owningGrid;
    public Grid grid
    {
        get { return owningGrid; }
        set { owningGrid = value; }
    }

    public void AddNewTileToList(Tile newTile)
    {
        grid.AddTile(newTile);
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(newTile);
#endif
    }

    public void SpawnAbove()
    {
        if (up)
        {
            Debug.LogError("TILE ALREADY EXISTS ABOVE");
            return;
        }
        if (!grid)
        {
            Debug.LogError("TILE CANNOT SPAWN WITHOUT BEING CONNECTED TO A GRID");
        }
        GameObject newTile = Instantiate(grid.baseTile, transform.parent);
        Tile newTileScript = newTile.GetComponent<Tile>();
        if (!newTileScript)
        {
            Debug.LogError("BASE TILE PREFAB DOES NOT HAVE TILE SCRIPT");
        }
        newTile.transform.position = transform.position + new Vector3(0, newTileScript.collider.size.y, 0);
        newTileScript.down = this;
        up = newTileScript;

        if (right && right.up)
        {
            newTileScript.right = right.up;
        }
        if(left && left.up)
        {
            newTileScript.left = left.up;
        }
        if (left && left.up && left.up.up && left.up.up.right)
        {
            newTileScript.up = left.up.up.right;
        }
        else if (right && right.up && right.up.up && right.up.up.left)
        {
            newTileScript.up = right.up.up.left;
        }

        newTileScript.grid = grid;
        AddNewTileToList(newTileScript);
    }

    public void SpawnBelow()
    {
        if (down)
        {
            Debug.LogError("TILE ALREADY EXISTS BELOW");
            return;
        }
        if (!grid)
        {
            Debug.LogError("TILE CANNOT SPAWN WITHOUT BEING CONNECTED TO A GRID");
        }
        GameObject newTile = Instantiate(grid.baseTile, transform.parent);
        Tile newTileScript = newTile.GetComponent<Tile>();
        if (!newTileScript)
        {
            Debug.LogError("BASE TILE PREFAB DOES NOT HAVE TILE SCRIPT");
        }
        newTile.transform.position = transform.position + new Vector3(0, -newTileScript.collider.size.y, 0);
        newTileScript.up = this;
        down = newTileScript;

        if (right && right.down)
        {
            newTileScript.right = right.down;
        }
        if (left && left.down)
        {
            newTileScript.left = left.down;
        }
        if (left && left.down && left.down.down && left.down.down.right)
        {
            newTileScript.down = left.down.down.right;
        }
        else if (right && right.down && right.down.down && right.down.down.left)
        {
            newTileScript.down = right.down.down.left;
        }

        newTileScript.grid = grid;
        AddNewTileToList(newTileScript);
    }

    public void SpawnRight()
    {
        if (right)
        {
            Debug.LogError("TILE ALREADY EXISTS TO THE RIGHT");
            return;
        }
        if (!grid)
        {
            Debug.LogError("TILE CANNOT SPAWN WITHOUT BEING CONNECTED TO A GRID");
        }
        GameObject newTile = Instantiate(grid.baseTile, transform.parent);
        Tile newTileScript = newTile.GetComponent<Tile>();
        if (!newTileScript)
        {
            Debug.LogError("BASE TILE PREFAB DOES NOT HAVE TILE SCRIPT");
        }
        newTile.transform.position = transform.position + new Vector3(newTileScript.collider.size.x,0,0);
        newTileScript.left = this;
        right = newTileScript;

        if (up && up.right)
        {
            newTileScript.up = up.right;
        }
        if (down && down.right)
        {
            newTileScript.down = down.right;
        }
        if (up && up.right && up.right.right && up.right.right.down)
        {
            newTileScript.right = up.right.right.down;
        }
        else if (down && down.right && down.right.right && down.right.right.up)
        {
            newTileScript.right = down.right.right.up;
        }

        newTileScript.grid = grid;
        AddNewTileToList(newTileScript);
    }

    public void SpawnLeft()
    {
        if (left)
        {
            Debug.LogError("TILE ALREADY EXISTS TO THE LEFT");
            return;
        }
        if (!grid)
        {
            Debug.LogError("TILE CANNOT SPAWN WITHOUT BEING CONNECTED TO A GRID");
        }
        GameObject newTile = Instantiate(grid.baseTile, transform.parent);
        Tile newTileScript = newTile.GetComponent<Tile>();
        if (!newTileScript)
        {
            Debug.LogError("BASE TILE PREFAB DOES NOT HAVE TILE SCRIPT");
        }
        newTile.transform.position = transform.position + new Vector3(-newTileScript.collider.size.x,0,0);
        newTileScript.right = this;
        left = newTileScript;

        if (up && up.left)
        {
            newTileScript.up = up.left;
        }
        if (down && down.left)
        {
            newTileScript.down = down.left;
        }
        if (up && up.left && up.left.left && up.left.left.down)
        {
            newTileScript.left = up.left.left.down;
        }
        else if (down && down.left && down.left.left && down.left.left.up)
        {
            newTileScript.left = down.left.left.right;
        }

        newTileScript.grid = grid;
        AddNewTileToList(newTileScript);
    }

    public void DeleteTile(bool editMode = false)
    {
        grid.RemoveTile(this);
        if (editMode)
        {
            DestroyImmediate(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public void SetDefaultColor()
    {
        renderer.color = defaultColor;
    }

    public void SetHighlightColor()
    {
        renderer.color = highlightColor;
    }

    public void SetErrorColor()
    {
        renderer.color = errorColor;
    }

    public void AttachTile(Tile newTile)
    {
        if (newTile)
        {
            attachedTile = newTile;
        }
    }
}
