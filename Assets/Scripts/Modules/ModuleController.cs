using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleController : MonoBehaviour
{
    [SerializeField, Tooltip("the level grid to set visibility")] private LevelGrid owningLevel;
    [SerializeField, Tooltip("the module to place")] private GameObject[] ModulesToPlace;
    [SerializeField, Tooltip("the game manager")] private MicroGameManager GameManager;
    private ObjectGrid grid;

    private bool secondInPair = false;
    private GameObject previousModule;

    private int moduleIndex = 0;

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
        GameObject moduleGrid = Instantiate(ModulesToPlace[moduleIndex], transform);
        grid = moduleGrid.GetComponent<ObjectGrid>();
        grid.SetParent(transform);
        grid.StartDragging();
    }

    public void TryDrop(InputAction.CallbackContext ctx)
    {
        float val = ctx.ReadValue<float>();
        if (val > 0)
        {
            if (!clickedDrop && !droppedObject)
            {
                clickedDrop = true;
                if (grid.StopDragging())
                {
                    droppedObject = true;
                    grid.transform.parent = transform.parent;

                    ModuleBehavior behavior = grid.gameObject.GetComponent<ModuleBehavior>();

                    // If this module is the second in a pair, assign pair references
                    if (previousModule)
                    {
                        previousModule.GetComponent<ModuleBehavior>().SetOtherModule(grid.gameObject);
                        behavior.SetOtherModule(previousModule);
                        previousModule = null;
                        Debug.Log("second in pair");
                    }

                    // Increment the module index unless this is the first of a pair
                    if (secondInPair || !behavior.HasPairModule())
                        moduleIndex++;

                    // If this is the first of a pair, store the reference to the previous module
                    else
                        previousModule = grid.gameObject;

                    // If this is a pair module, flip the boolean flag
                    if (behavior.HasPairModule())
                    {
                        secondInPair = !secondInPair;
                    }

                    // If there are no more modules to place, delete the controller and end module placement
                    if (moduleIndex >= ModulesToPlace.Length)
                    {
                        Debug.Log("Out of modules");
                        GameManager.EndModulePlacement();
                        Destroy(gameObject);
                        return;
                    }

                    // Update the controller with the next module
                    GameObject newObject = Instantiate(ModulesToPlace[moduleIndex], transform);
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


        if (inputs.x > 0)
        {
            movedLeft = false;
            if (!movedRight)
            {
                movedRight = true;
                grid.TryMoveRight();
            }
        }
        else if (inputs.x < 0)
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
