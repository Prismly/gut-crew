using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Bot : MonoBehaviour
{
    private float trackDistance = 0f;

    public float IncrementDistance(float increment)
    {
        trackDistance = Mathf.Clamp(trackDistance + increment, 0, float.MaxValue);
        return trackDistance;
    }
}
