using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
    [SerializeField] private float InteractCooldown = 0.05f;
    private float InteractTimer = 0;

    private void Update()
    {
        if (InteractTimer > 0)
            InteractTimer -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractTimer > 0)
            return;

        GameObject Interactable = collision.gameObject;
        ModuleBehavior module;
        if (module = Interactable.GetComponent<ModuleBehavior>())
        {
            module.ActivateModule(transform.parent.gameObject);
            InteractTimer = InteractCooldown;
        }
        else
        {
            Debug.LogWarning("Interactable has no module behavior");
        }
    }
}
