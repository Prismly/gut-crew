using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleBehavior : MonoBehaviour
{
    [SerializeField] private bool HasPair = false;
    protected GameObject OtherModule;

    [SerializeField] private float ModuleCooldown = 1f;
    private float TimeUntilUsable;
    private bool IsUsable = true;

    private Module ModuleSelf;

    private void Start()
    {
        ModuleSelf = GetComponent<Module>();
        if (ModuleSelf == null)
        {
            Debug.LogError("ModuleBehavior attached without Module script!!");
        }
    }

    private void Update()
    {
        if (TimeUntilUsable > 0)
            TimeUntilUsable -= Time.deltaTime;
        else if (!IsUsable)
        {
            IsUsable = true;
            ModuleSelf.SetActive(true);
        }
    }

    public bool HasPairModule()
    {
        return HasPair;
    }

    public void SetOtherModule(GameObject other)
    {
        OtherModule = other;
    }

    public void ActivateModule(GameObject Executor)
    {
        if (IsUsable)
        {
            Execute(Executor);
            TimeUntilUsable = ModuleCooldown;
            IsUsable = false;
            ModuleSelf.SetActive(false);
        }
    }

    /**
     * Generic function for executing a module, inherited by all modules.
     * @param Executor the character executing this module
     */
    protected abstract void Execute(GameObject Executor);
}
