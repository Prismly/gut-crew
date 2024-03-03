using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment
{
    public MovementScheme MoveScheme = null;
    
    // NOTE: Start Point is implicitly equal to the GameObject's position in world space.
    // NOTE: End Point is implicitly equal to Start Point, shifted right by Length.
    public Vector2 StartPoint = Vector2.zero;
    public Vector2 EndPoint = Vector2.zero;

    public float StartDist = 0;

    // The length of this segment in world space.
    public float Length = 0;

    public TrackSegment(Vector2 prevSegmentEnd, float startDist, float length, MovementScheme moveScheme)
    {
        Length = length;
        StartDist = startDist;
        StartPoint = prevSegmentEnd;
        EndPoint = StartPoint + (Vector2.right * Length);
        MoveScheme = moveScheme;
    }
}
