using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private bool reliableTriggerStay = false;

    // Layers must be set up such that this and "OnTriggerExit2D" ONLY interact with "Terrain" objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        reliableTriggerStay = true;
    }

    // Layers must be set up such that this and "OnTriggerEnter2D" ONLY interact with "Terrain" objects
    private void OnTriggerExit2D(Collider2D collision)
    {
        reliableTriggerStay = false;
    }

    public bool GroundCheck()
    {
        return reliableTriggerStay;
    }
}
