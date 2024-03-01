using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObstacleData : ScriptableObject
{
    [SerializeField] private bool walkable;
    [SerializeField] private bool climbable;
    [SerializeField] private bool swimmable;
}
