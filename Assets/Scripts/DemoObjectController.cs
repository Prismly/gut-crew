using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoObjectController : MonoBehaviour
{
    [SerializeField, Tooltip("the level grid to set visibility")] private LevelGrid owningLevel;
    [SerializeField, Tooltip("the object being controlled")] private ObjectGrid grid;
    [SerializeField, Tooltip("object prefab")] private GameObject gridPrefab;
    private bool movedRight = false;
    private bool movedLeft = false;
    private bool movedUp = false;
    private bool movedDown = false;

    private bool droppedObject = false;
    private bool clickedDrop = false;

    private bool visible = true;
    private bool clickedVisible = false;
    private void Start()
    {
        grid.SetParent(transform);
        grid.StartDragging();
    }

    public void TryDrop(InputAction.CallbackContext ctx)
    {
        float val = ctx.ReadValue<float>();
        if(val > 0)
        {
            if (!clickedDrop && !droppedObject)
            {
                clickedDrop = true;
                if (grid.StopDragging())
                {
                    droppedObject = true;
                    grid.transform.parent = transform.parent;
                    GameObject newObject = Instantiate(gridPrefab, transform);
                    grid = newObject.GetComponent<ObjectGrid>();
                    grid.SetParent(transform);
                    droppedObject = false;
                    grid.StartDragging();
                }
            }
        }
        else
        {
            clickedDrop = false;
        }
    }

    public void ToggleVisibility(InputAction.CallbackContext ctx)
    {
        float val = ctx.ReadValue<float>();
        if (val > 0)
        {
            if (!clickedVisible)
            {
                clickedVisible = true;
                visible = !visible;
                owningLevel.SetVisible(visible);
            }
        }
        else
        {
            clickedVisible = false;
        }
    }

    public void HandleInput(InputAction.CallbackContext ctx)
    {
        Vector2 inputs = ctx.ReadValue<Vector2>();
        

        if(inputs.x > 0)
        {
            movedLeft = false;
            if (!movedRight)
            {
                movedRight = true;
                grid.TryMoveRight();
            }
        }
        else if(inputs.x < 0)
        {
            movedRight = false;
            if (!movedLeft)
            {
                movedLeft = true;
                grid.TryMoveLeft();
            }
        }
        else
        {
            movedRight = false;
            movedLeft = false;
        }

        if (inputs.y > 0)
        {
            movedDown = false;
            if (!movedUp)
            {
                movedUp = true;
                grid.TryMoveUp();
            }
        }
        else if (inputs.y < 0)
        {
            movedUp = false;
            if (!movedDown)
            {
                movedDown = true;
                grid.TryMoveDown();
            }
        }
        else
        {
            movedUp = false;
            movedDown = false;
        }

    }
}
