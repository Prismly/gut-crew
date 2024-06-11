using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ModuleController : MonoBehaviour
{
    [SerializeField, Tooltip("the level grid to set visibility")] private LevelGrid owningLevel;
    [SerializeField, Tooltip("the module to place")] private GameObject[] ModulesToPlace;
    private ObjectGrid grid;

    [SerializeField] private TMP_Text ModuleName;
    [SerializeField] private TMP_Text ModuleDescription;

    private List<GameObject> Modules;
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
        Modules = new List<GameObject>();
        foreach (GameObject obj in ModulesToPlace)
        {
            Modules.Add(obj);
        }
        InitializeObjectAtIndex();
    }

    private void InitializeObjectAtIndex()
    {
        GameObject moduleGrid = Instantiate(Modules[moduleIndex], transform);
        grid = moduleGrid.GetComponent<ObjectGrid>();
        grid.SetParent(transform);
        grid.StartDragging();

        ModuleBehavior behavior = moduleGrid.GetComponent<ModuleBehavior>();
        ModuleName.SetText(behavior.GetDisplayName());
        ModuleDescription.SetText(behavior.GetDescription());
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

                    // Reset the module index and remove this element from the list unless this is the first of a pair
                    if (secondInPair || !behavior.HasPairModule())
                    {
                        Modules.RemoveAt(moduleIndex);
                        moduleIndex = 0;
                    }
                    // If this is the first of a pair, store the reference to the previous module
                    else
                        previousModule = grid.gameObject;

                    // If this is a pair module, flip the boolean flag
                    if (behavior.HasPairModule())
                    {
                        secondInPair = !secondInPair;
                    }

                    // If there are no more modules to place, delete the controller and end module placement
                    if (moduleIndex >= Modules.Count)
                    {
                        Debug.Log("Out of modules");
                        GameObject.Find("LevelGrid").GetComponent<Global>().EnemyActionTimer *= 0.75f;
                        Destroy(gameObject);
                        SceneManager.LoadScene(1);
                        return;
                    }

                    // Update the controller with the next module
                    InitializeObjectAtIndex();
                    droppedObject = false;
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

    public void SelectNextModule(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && previousModule == null)
        {
            Destroy(grid.gameObject);
            moduleIndex = (moduleIndex + 1) % Modules.Count;
            InitializeObjectAtIndex();
        }
    }

    public void SelectPreviousModule(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && previousModule == null)
        {
            Destroy(grid.gameObject);
            moduleIndex -= 1;
            if (moduleIndex < 0)
                moduleIndex = Modules.Count - 1;
            InitializeObjectAtIndex();
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
