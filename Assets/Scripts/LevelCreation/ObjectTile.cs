using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTile : Tile
{
    [SerializeField]private bool dragging = false;
    [SerializeField, Tooltip("the Tile that was most recently hovered over")]
    private Collider prevHoverTile;
    [Space]
    [SerializeField, Tooltip("if this tile is part of a destruction object")] private bool isDestructionTile = false;

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            CheckRayCast();
        }
    }

    public void StartDragging()
    {
        SetHighlightColor();
        dragging = true;
    }

    public void StopDragging(bool validRelease)
    {
        SetDefaultColor();
        dragging = false;
        if (prevHoverTile)
        {
            Tile prevTile = prevHoverTile.GetComponent<Tile>();
            if (validRelease)
            {
                AttachTile(prevTile);
                prevTile.AttachTile(this);
            }
            //turn off highlight
            prevTile.SetDefaultColor();
            prevHoverTile = null;
        }
    }

    public bool IsValid(bool destructible = false)
    {
        if (destructible)
        {
            ObjectTile attachedTile = (ObjectTile)prevHoverTile.GetComponent<Tile>().attached;
            return prevHoverTile && attachedTile && attachedTile.IsDestructible();
        }
        return prevHoverTile && !prevHoverTile.GetComponent<Tile>().attached;
    }

    public bool CheckMoveUp()
    {
        return prevHoverTile && prevHoverTile.GetComponent<Tile>().up;
    }
    public bool CheckMoveDown()
    {
        return prevHoverTile && prevHoverTile.GetComponent<Tile>().down;
    }
    public bool CheckMoveLeft()
    {
        return prevHoverTile && prevHoverTile.GetComponent<Tile>().left;
    }
    public bool CheckMoveRight()
    {
        return prevHoverTile && prevHoverTile.GetComponent<Tile>().right;
    }


    public void DestroyObject()
    {
        if (prevHoverTile)
        {
            Tile hoverTile = prevHoverTile.GetComponent<Tile>();
            hoverTile.SetDefaultColor();
            if (!hoverTile.attached)
            {
                return;
            }

            ObjectGrid hoverObject = (ObjectGrid)(prevHoverTile.GetComponent<Tile>().attached.grid);
            if (!hoverObject)
            {
                return;
            }
            ((LevelGrid)prevHoverTile.GetComponent<Tile>().grid).RemoveObject(hoverObject);
            hoverObject.DestroyObject();
        }
    }


    public bool CheckRayCast()
    {
        RaycastHit raycastHit;
        bool success = Physics.Raycast(transform.position, new Vector3(0, 0, 1), out raycastHit, 10, ~LayerMask.NameToLayer("LevelTile"));
        Collider hit = raycastHit.collider;
        if (hit)
        {
            //Debug.Log("hit something");
            if (hit != prevHoverTile)
            {
                if (prevHoverTile)
                {
                    prevHoverTile.GetComponent<Tile>().SetDefaultColor();
                }
                prevHoverTile = hit;
            }


            Tile hitTile = hit.GetComponent<Tile>();
            if (hitTile.attached)
            {
                if (isDestructionTile && !((ObjectTile)hitTile.attached).IsDestructible())
                {
                    hitTile.SetHighlightColor();
                    SetHighlightColor();
                }
                else
                {
                    hitTile.SetErrorColor();
                    SetErrorColor();
                }
            }
            else
            {
                hitTile.SetHighlightColor();
                SetHighlightColor();
            }
            //}

        }
        else
        {
            if (prevHoverTile)
            {
                //turn off highlight
                Tile prevTile = prevHoverTile.GetComponent<Tile>();
                prevTile.SetDefaultColor();
                if (prevTile.attached)
                {
                    SetHighlightColor();
                }
                prevHoverTile = null;

            }
        }

        return success;
    }

    public Vector3 GetHoverOffset()
    {
        if (!prevHoverTile)
        {
            return Vector3.zero;
        }
        return prevHoverTile.transform.position - transform.position;
    }

    public bool IsDestructible()
    {
        return ((ObjectGrid)grid).IsDestructible();
    }
}
