using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private int reliableTriggerStay = 0;

    // Layers must be set up such that this and "OnTriggerExit2D" ONLY interact with "Terrain" objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        reliableTriggerStay++;
        //Debug.Log("TriggerEnter");
    }

    // Layers must be set up such that this and "OnTriggerEnter2D" ONLY interact with "Terrain" objects
    private void OnTriggerExit2D(Collider2D collision)
    {
        reliableTriggerStay--;
        //Debug.Log("TriggerExit");
    }

    public bool GroundCheck()
    {
        //Debug.Log(reliableTriggerStay);
        return reliableTriggerStay > 0;
    }
}
