using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectGrid : Grid
{
    [SerializeField, Tooltip("The overarching parent transform for this object that contains both the grid and the object's functionality")]private Transform parentTransform;
    private Vector3 tileSize;
    public void StartDragging()
    {
        foreach(ObjectTile tile in tiles )
        {
            tile.StartDragging();
        }
        ObjectTile firstTile = (ObjectTile)tiles[0];
        if(firstTile.CheckRayCast())
        {
            Vector3 offset = firstTile.GetHoverOffset();
            parentTransform.position += new Vector3(offset.x, offset.y, 0);
        }
    }

    public void Start()
    {
        tileSize = tilePrefab.GetComponent<Tile>().collider.size;
    }

    public bool TryMoveUp()
    {
        bool success = true;
        foreach (ObjectTile tile in tiles)
        {
            if (!tile.CheckMoveUp())
            {
                success = false;
                break;
            }
        }
        if (!success)
        {
            Debug.Log("Can't move up");
            return false;
        }
        parentTransform.position += new Vector3(0, tileSize.y, 0);
        return true;
    }

    public bool TryMoveDown()
    {
        bool success = true;
        foreach (ObjectTile tile in tiles)
        {
            if (!tile.CheckMoveDown())
            {
                success = false;
                break;
            }
        }
        if (!success)
        {
            Debug.Log("Can't move down");
            return false;
        }
        parentTransform.position += new Vector3(0, -tileSize.y, 0);
        return true;
    }

    public bool TryMoveRight()
    {
        bool success = true;
        foreach (ObjectTile tile in tiles)
        {
            if (!tile.CheckMoveRight())
            {
                success = false;
                break;
            }
        }
        if (!success)
        {
            Debug.Log("Can't move right");
            return false;
        }
        parentTransform.position += new Vector3(tileSize.x, 0, 0);
        return true;
    }

    public bool TryMoveLeft()
    {
        bool success = true;
        foreach (ObjectTile tile in tiles)
        {
            if (!tile.CheckMoveLeft())
            {
                success = false;
                break;
            }
        }
        if (!success)
        {
            Debug.Log("Can't move left");
            return false;
        }
        parentTransform.position += new Vector3(-tileSize.x, 0, 0);
        return true;
    }


    public bool StopDragging()
    {
        foreach (ObjectTile tile in tiles)
        {
            if (!tile.IsValid())
            {
                Debug.LogWarning("Cannot place here");
                return false;
            }
        }
        foreach (ObjectTile tile in tiles)
        {
            tile.StopDragging(true);
        }
        Vector3 diff = tiles[0].attached.transform.position - tiles[0].transform.position;
        transform.position += new Vector3(diff.x, diff.y, 0);
        LevelGrid owningLevel = (LevelGrid)tiles[0].attached.grid;
        parentTransform.parent = owningLevel.transform;
        owningLevel.AddNewObject(this);
        return true;
    }

    public void CancelDrag()
    {
        foreach (ObjectTile tile in tiles)
        {
            tile.StopDragging(false);
        }
    }

    public void Highlight(bool highlight)
    {
        foreach (Tile tile in tiles)
        {
            if (highlight)
            {
                tile.SetHighlightColor();
            }
            else
            {
                tile.SetDefaultColor();
            }
        }
    }

    public void SetParent(Transform parent)
    {
        parentTransform = parent;
    }

}
