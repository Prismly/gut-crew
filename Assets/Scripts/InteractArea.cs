using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Interactable = collision.gameObject;
        ModuleBehavior module;
        if (module = Interactable.GetComponent<ModuleBehavior>())
        {
            module.Execute(transform.parent.gameObject);
        }
        else
        {
            Debug.LogWarning("Interactable has no module behavior");
        }
    }
}
