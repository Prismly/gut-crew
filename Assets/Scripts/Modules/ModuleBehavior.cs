using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehavior : MonoBehaviour
{
    /**
     * Generic function for executing a module, inherited by all modules.
     * @param Executor the character executing this module
     */
    public abstract void Execute(GameObject Executor);
}
