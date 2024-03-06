using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementScheme : ScriptableObject
{
    public enum Responses { NONE, MOVE, STAGGER }

    public GameObject environment;

    public abstract Responses ProcessLimbData(LimbData inputData);
}
