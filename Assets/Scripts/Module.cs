using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module : MonoBehaviour
{
    private Vector2 GridPosition = Vector2.zero;

    public Vector2 GetGridPosition()
    {
        return GridPosition;
    }

    public void SetGridPosition(Vector2 NewPosition)
    {
        GridPosition = NewPosition;
    }

    /**
     * Generic function for executing a module, inherited by all modules.
     * @param Executor the character executing this module
     */
    public abstract void Execute(GameObject Executor);
}
