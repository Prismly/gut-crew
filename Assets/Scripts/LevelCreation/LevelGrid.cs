using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : Grid
{
    [SerializeField, Tooltip("a list of all of the Object Grids that are currently attached to this Grid")]List<ObjectGrid> objects = new List<ObjectGrid>();
    // Start is called before the first frame update
    public void HighlightObjects(bool highlight)
    {
        foreach(ObjectGrid grid in objects)
        {
            grid.Highlight(highlight);
        }
    }

    public void AddNewObject(ObjectGrid newObject)
    {
        objects.Add(newObject);
    }

    public void RemoveObject(ObjectGrid obj)
    {
        objects.Remove(obj);
    }

    public void DisplayGrids(bool visible)
    {
        SetVisible(visible);
        foreach(ObjectGrid grid in objects)
        {
            grid.SetVisible(visible);
        }
    }
}
