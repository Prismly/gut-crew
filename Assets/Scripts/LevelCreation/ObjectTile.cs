using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTile : Tile
{
    private bool dragging = false;
    [SerializeField, Tooltip("the Tile that was most recently hovered over")]
    private Collider prevHoverTile;

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            RaycastHit raycastHit;
            bool success = Physics.Raycast(transform.position,new Vector3(0,0,1), out raycastHit, 10,~LayerMask.NameToLayer("LevelTile"));
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
                }

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
                prevTile.AttachTile(null);
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
        return prevHoverTile.GetComponent<Tile>().up;
    }
    public bool CheckMoveDown()
    {
        return prevHoverTile.GetComponent<Tile>().down;
    }
    public bool CheckMoveLeft()
    {
        return prevHoverTile.GetComponent<Tile>().left;
    }
    public bool CheckMoveRight()
    {
        return prevHoverTile.GetComponent<Tile>().right;
    }
}
