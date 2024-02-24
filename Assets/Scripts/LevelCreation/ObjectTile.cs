using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTile : Tile
{
    [SerializeField]private bool dragging = false;
    [SerializeField, Tooltip("the Tile that was most recently hovered over")]
    private Collider prevHoverTile;

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

    public bool IsValid()
    {
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
                hitTile.SetErrorColor();
                SetErrorColor();
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
}
